//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FullSerializer;
using Haferbrei.JsonConverter;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wichtel;
using Wichtel.Extensions;

namespace Haferbrei {
[CreateAssetMenu(fileName = "SaveLoadController", menuName = "Scriptable Objects/SaveLoadController", order = 0)]
public class SaveLoadController : SerializedScriptableObject
{
    [ReadOnly]
    public bool loadSaveGame; //soll beim nächsten Mal, dass das Spiel initialisiert wird, saveGame data geladen werden?

    [SerializeField, BoxGroup("Settings"), Required] private bool encryptSaveFile = true;
    [SerializeField, BoxGroup("Settings"), Required] private string saveGameFileExtension = ".save";
    public PrefabCollection allPrefabsCollection;
    public SceneCollection allScenesCollection;
    public SaveableScriptableObjects saveableScriptableObjects;
    public GameObject initializerPrefab;
    public GameObject loadingScreenPrefab;

    public SaveFileData loadedData = new SaveFileData();
    public SaveFileData dataToSave = new SaveFileData();

    public List<SaveablePrefab> saveablePrefabs = new List<SaveablePrefab>();
    public List<SaveableComponent> saveableComponents = new List<SaveableComponent>();

    [ShowInInspector] private Dictionary<Transform, Guid> parents = new Dictionary<Transform, Guid>();
    private Dictionary<Scene, Guid> initializers = new Dictionary<Scene, Guid>();
    private List<fsConverter> converters;
    private string encryptionPassword = "Good job, you managed to hack the password! Have fun reading the SaveFile. :)";
    private string saveGameFileName;

    private string saveGameDirectoryPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Application.companyName, Application.productName);
    private string saveGameFilePath => Path.Combine(saveGameDirectoryPath, saveGameFileName + ((encryptSaveFile) ? saveGameFileExtension : ".json"));

    [Button, DisableInEditorMode]
    public void SaveGameState(string _saveGameFileName)
    {
        dataToSave = new SaveFileData();
        saveGameFileName = _saveGameFileName;

        // 1. Collect SaveableScriptableObjects
        saveableScriptableObjects.CollectAllRuntimeInstantiatedSOs();

        // 2. Save ScriptableObjects
        var scriptableObjectsData = saveableScriptableObjects.SaveScriptableObjects();
        dataToSave.scriptableObjectDatas.AddRange(scriptableObjectsData);

        // 3. Save Prefabs
        saveablePrefabs = ComponentExtensions.FindAllComponentsOfType<SaveablePrefab>();
        foreach (var saveablePrefab in saveablePrefabs)
        {
            SaveablePrefabData objectData = saveablePrefab.SaveData();
            dataToSave.prefabDatas.Add(objectData);
        }
        
        // 4. Save Components
        saveableComponents = ComponentExtensions.FindAllComponentsOfType<SaveableComponent>();
        foreach (var saveableComponent in saveableComponents)
        {
            dataToSave.componentDatas.Add(saveableComponent.StoreData());
        }
        
        // 5. Write file
        var serializer = new fsSerializer();
        fsData data;
        GetJsonConverters(ref serializer);

        serializer.TrySerialize(typeof(SaveFileData), dataToSave, out data).AssertSuccessWithoutWarnings();     // 5.1 Serialize data
        string json = (encryptSaveFile) ? fsJsonPrinter.CompressedJson(data) : fsJsonPrinter.PrettyJson(data);  // 5.2 Create Json string
        if(encryptSaveFile) json = StringCipher.Encrypt(json, encryptionPassword);          // 5.3 Encrpyt Json string
        System.IO.Directory.CreateDirectory(saveGameDirectoryPath);                                             // 5.4 Create save game directory path if it doesn't already exists
        System.IO.File.WriteAllText(saveGameFilePath, json);                                           // 5.5 Write file to disk
        
        //6. Finished saving
        Debug.Log("Game saved!");
    }

    [Button, DisableInEditorMode]
    public void PrepareLoading(string _saveGameFileName)
    {
        Debug.Log("Prepare loading game @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);
        loadSaveGame = true;
        saveGameFileName = _saveGameFileName;
    }

    public IEnumerator<float> _LoadGameState()
    {
        Debug.Log("Start loading game @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);

        // Loading Screen
        var loadingScreen = Instantiate(loadingScreenPrefab);
        DontDestroyOnLoad(loadingScreen);

        // Clean up & prepare
        loadSaveGame = false;
        loadedData = new SaveFileData();
        parents.Clear();
        initializers.Clear();
        
        // 0. Read file and convert Json
        var serializer = new fsSerializer();
        GetJsonConverters(ref serializer);
        
        string json = System.IO.File.ReadAllText(saveGameFilePath);                                         // 0.1 Read file from disk
        if (encryptSaveFile) json = StringCipher.Decrypt(json, encryptionPassword);    // 0.2 Decrypt json string
        fsData parsedData = fsJsonParser.Parse(json);                                                       // 0.3 Create data from json string
        serializer.TryDeserialize(parsedData, ref loadedData);                                              // 0.4 Deserialize json
        

        // 1. Load all necessary scenes
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(_LoadNecessaryScenes()));
        Debug.Log("Loaded all scenes @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);

        // 2. Create ScriptableObject Instances and load data into them
        foreach (var data in loadedData.scriptableObjectDatas)
        {
            LoadScriptableObject(data);
        }

        Debug.Log("Loaded all scriptableObjects @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);

        // 3. All Guids auf bestehenden Objekten initialisieren
        GuidManager.InitializeGuidComponents();
        Debug.Log("Initialized all GuidComponents @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);

        // 4. Load GameObject data and create GameObjects
        foreach (var data in loadedData.prefabDatas)
        {
            LoadPrefab(data); //yield return Timing.WaitUntilDone(Timing.RunCoroutine(_LoadGameObject(data)));
        }

        Debug.Log("Loaded all GameObjects @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);

        // 5. Load Components
        List<SaveableComponent> components = ComponentExtensions.FindAllComponentsOfType<SaveableComponent>();
        foreach (var componentData in loadedData.componentDatas)
        {
            var component = FindSaveableComponentByGuid(componentData.Id, components);
            if(component != null) component.RestoreData(componentData);
        }
        
        // 6. Set parents
        SetParents();

        // 7. Loading complete
        Destroy(loadingScreen);
        Debug.Log("Loading complete @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);
    }

    private void SetParents()
    {
        foreach (var pair in parents)
        {
            var parent = GuidManager.ResolveGuid(pair.Value);
            if (parent == null)
            {
                Debug.LogError("Parent doesn't exist! Guid: " + pair.Value);
                continue;
            }

            pair.Key.SetParent(parent.transform, false);
        }
    }

    private IEnumerator<float> _LoadNecessaryScenes()
    {
        //Welche Szenen müssen geladen werden?
        var necessaryScenes = new List<string>();
        foreach (var data in loadedData.prefabDatas)
        {
            if (data is SaveablePrefabData gameObjectData && !necessaryScenes.Contains(gameObjectData.sceneName))
                necessaryScenes.Add(gameObjectData.sceneName);
        }

        //Lade diese Szenen
        foreach (var sceneName in necessaryScenes)
        {
            if (!SceneUtilitiesW.IsSceneLoaded(sceneName))
            {
                var scene = allScenesCollection.GetScene(sceneName);
                Debug.LogWarning("Achtung, Szene \"" + sceneName +
                                 "\" war nicht geladen. Idealerweise sollten alle nötigen Szenen geladen sein, bevor der Speicherstand geladen wird.");
                yield return Timing.WaitUntilDone(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));
                Debug.Log("loaded Scene: " + scene.ScenePath);
            }
        }
    }

    private void LoadPrefab(SaveableObjectData _loadedObjectData)
    {
        var data = _loadedObjectData as SaveablePrefabData;
        var targetScene = SceneManager.GetSceneByName(data.sceneName);
        SceneManager.SetActiveScene(targetScene);

        // 1. Existiert das GameObject mit der Guid? Falls nicht, muss das Prefab instantiiert werden.
        var gameObjectToLoad = GuidManager.ResolveGuid(data.guid);
        if (gameObjectToLoad == null) gameObjectToLoad = InstantiatePrefab(data);

        // 2. Lade Daten in das GameObject
        gameObjectToLoad.GetComponent<SaveablePrefab>().LoadData(data);

        // 3. Falls das GameObject einen Parent hatte, wird der hier vermerkt, damit es den später bekommt
        // Falls es keinen Parent hatte, wird es einem Initializer zugeordnet, damit der es dann korrekt initialisieren kann
        if (data.parentGuid != Guid.Empty) parents.Add(gameObjectToLoad.transform, data.parentGuid);
        else
        {
            Guid initializerParent;
            if (!initializers.ContainsKey(targetScene)) //if this scene doesn't already have an initializer, create one
            {
                var newInitializer = Instantiate(initializerPrefab);
                newInitializer.SetActive(false);
                newInitializer.name =
                    $"Initializer of scene {targetScene.name}"; //um mögliches Debugging zu vereinfachen, damit nicht alle Initializer "Initializer(Clone)" heißen
                var newGuid = Guid.NewGuid();
                var guidComponent = newInitializer.GetComponent<GuidComponent>();
                guidComponent.SetGuid(newGuid);
                initializers.Add(targetScene, guidComponent.GetGuid());
            }

            initializerParent = initializers[targetScene];
            parents.Add(gameObjectToLoad.transform, initializerParent);
        }
    }

    private GameObject InstantiatePrefab(SaveablePrefabData _objectData)
    {
        GameObject newGameObject = null;
        var prefabToInstantiate = allPrefabsCollection.GetPrefab(_objectData.prefabName);
        if (prefabToInstantiate != null)
        {
            //instantiiere Prefab
            newGameObject = Instantiate(prefabToInstantiate);
            //Setze die Guid auf dem neu erstellten GameObjekt.
            newGameObject.GetComponent<GuidComponent>().SetGuid(_objectData.guid);
        }
        else
            Debug.LogError("Konnte Prefab nicht finden, das beim Laden instantiiert werden sollte! (" +
                           _objectData.prefabName + ") Beim GameObject: " + _objectData.gameObjectName);

        return newGameObject;
    }

    private void LoadScriptableObject(SaveableObjectData _loadedObjectData)
    {
        saveableScriptableObjects.LoadScriptableObject(_loadedObjectData as SaveableScriptableObjectData);
    }

    private SaveableComponent FindSaveableComponentByGuid(string _guid, List<SaveableComponent> _saveableComponents)
    {
        foreach (var saveableComponent in _saveableComponents)
        {
            if (saveableComponent.componentGuid.ToString() == _guid) return saveableComponent;
        }
        return null;
    }

    private void GetJsonConverters(ref fsSerializer _serializer)
    {
        converters = new List<fsConverter>(AllJsonConverters.GetAllJsonConverters());
        foreach (var converter in converters)
        {
            _serializer.AddConverter(converter);
        }
    }
}
}