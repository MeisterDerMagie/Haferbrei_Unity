//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Haferbrei.JsonConverters;
//using Bayat.SaveSystem;
using MEC;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wichtel;

namespace Haferbrei {
[CreateAssetMenu(fileName = "SaveLoadController", menuName = "Scriptable Objects/SaveLoadController", order = 0)]
public class SaveLoadController : SerializedScriptableObject
{
    [ReadOnly] public bool loadSaveGame; //soll beim nächsten Mal, dass das Spiel initialisiert wird, saveGame data geladen werden?
    
    public PrefabCollection allPrefabsCollection;
    public SceneCollection allScenesCollection;
    public SaveableScriptableObjects saveableScriptableObjects;
    public GameObject initializerPrefab;
    public GameObject loadingScreenPrefab;
    
    public List<SaveableObjectData> loadedData = new List<SaveableObjectData>();
    public List<SaveableObjectData> dataToSave = new List<SaveableObjectData>();

    public List<ISaveable> saveableGameObjects = new List<ISaveable>();
    
    [ShowInInspector] private Dictionary<Transform, Guid> parents = new Dictionary<Transform, Guid>();
    private Dictionary<Scene, Guid> initializers = new Dictionary<Scene, Guid>();
    private string saveGameFileName;
    
    [Button, DisableInEditorMode]
    public void SaveGameState(string _saveGameFileName)
    {
        if(dataToSave == null) dataToSave = new List<SaveableObjectData>();
        dataToSave.Clear();
        
        // 1. Collect SaveableScriptableObjects
        saveableScriptableObjects.CollectAllRuntimeInstantiatedSOs();
        
        // 2. Save ScriptableObjects
        var scriptableObjectsData = saveableScriptableObjects.SaveScriptableObjects();
        dataToSave.AddRange(scriptableObjectsData);
        
        // 3. Save GameObjects
        List<ISaveable> emptySaveablesToRemove = new List<ISaveable>(); //anscheinend kann es selten passieren, dass leere Einträge übrig bleiben. Hier wird sich darum gekümmert.
        foreach (var saveableGameObject in saveableGameObjects)
        {
            if (saveableGameObject == null)
            {
                emptySaveablesToRemove.Add(saveableGameObject);
                continue;
            }
            SaveableObjectData objectData = saveableGameObject.SaveData();
            dataToSave.Add(objectData);
        }
        foreach (var emptyEntry in emptySaveablesToRemove) saveableGameObjects.Remove(emptyEntry);
        
        // 4. Write file
        //
        // SAVE FILE HERE
        JsonConverter[] converters = AllJsonConverters.GetAllJsonConverters(); //ToDo: cache converters for improved performance
        var settings = new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore, TypeNameHandling = TypeNameHandling.All, Converters = converters};
        string json = JsonConvert.SerializeObject(dataToSave, Formatting.Indented, settings);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/SaveFile.json", json);
        //
        
        Debug.Log("Game saved!");
    }

    [Button, DisableInEditorMode]
    public void PrepareLoading(string _saveGameFileName)
    {
        Debug.Log("Prepare loading game @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay );
        loadSaveGame = true;
        saveGameFileName = _saveGameFileName;
    }
    
    public IEnumerator<float> _LoadGameState()
    {
        Debug.Log("Start loading game @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay );

        var loadingScreen = Instantiate(loadingScreenPrefab);
        DontDestroyOnLoad(loadingScreen);
        
        loadSaveGame = false;
        if(loadedData == null) loadedData = new List<SaveableObjectData>();
        loadedData.Clear();
        //
        // LOAD FILE HERE
        string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/SaveFile.json");
        JsonConverter[] converters = AllJsonConverters.GetAllJsonConverters(); //ToDo: cache converters for improved performance
        var settings = new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore, TypeNameHandling = TypeNameHandling.All, Converters = converters};
        loadedData = JsonConvert.DeserializeObject<List<SaveableObjectData>>(json, settings);
        //
        
        parents.Clear();
        initializers.Clear();
        
        // 1. Load all necessary scenes
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(_LoadNecessaryScenes()));
        Debug.Log("Loaded all scenes @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);
        
        // 2. Create ScriptableObject Instances and load data into them
        foreach (var data in loadedData)
        {
            if (data.saveableType == "ScriptableObject") LoadScriptableObject(data);
        }
        Debug.Log("Loaded all scriptableObjects @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);
        
        // 3 All Guids auf bestehenden Objekten initialisieren
        GuidManager.InitializeGuidComponents();
        Debug.Log("Initialized all GuidComponents @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);

        // 4. Load GameObject data and create GameObjects
        foreach (var data in loadedData)
        {
            if (data.saveableType == "GameObject") LoadGameObject(data); //yield return Timing.WaitUntilDone(Timing.RunCoroutine(_LoadGameObject(data)));
        }
        Debug.Log("Loaded all GameObjects @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);

        
        // 5. Set parents
        SetParents();
        
        Destroy(loadingScreen);
        Debug.Log("Loading complete @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);
    }

    private void SetParents()
    {
        foreach (var pair in parents)
        {
            var parent = GuidManager.ResolveGuid(pair.Value);
            if (parent == null) { Debug.LogError("Parent doesn't exist! Guid: " + pair.Value); continue; }
            pair.Key.SetParent(parent.transform, false);
        }
    }

    private IEnumerator<float> _LoadNecessaryScenes()
    {
        //Welche Szenen müssen geladen werden?
        var necessaryScenes = new List<string>();
        foreach (var data in loadedData)
        {
            if(data is SaveableGameObjectData gameObjectData && !necessaryScenes.Contains(gameObjectData.sceneName)) necessaryScenes.Add(gameObjectData.sceneName);
        }
        
        //Lade diese Szenen
        foreach (var sceneName in necessaryScenes)
        {
            if (!SceneUtilitiesW.IsSceneLoaded(sceneName))
            {
                var scene = allScenesCollection.GetScene(sceneName);
                Debug.LogWarning("Achtung, Szene \"" + sceneName + "\" war nicht geladen. Idealerweise sollten alle nötigen Szenen geladen sein, bevor der Speicherstand geladen wird.");
                yield return Timing.WaitUntilDone(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));
                Debug.Log("loaded Scene: " + scene.ScenePath);
            }
        }
    }

    private void LoadGameObject(SaveableObjectData _loadedObjectData)
    {
        var data = _loadedObjectData as SaveableGameObjectData;
        var targetScene = SceneManager.GetSceneByName(data.sceneName);
        SceneManager.SetActiveScene(targetScene);
        
        // 1. Existiert das GameObject mit der Guid? Falls nicht, muss das Prefab instantiiert werden.
        var gameObjectToLoad = GuidManager.ResolveGuid(data.guid);
        if (gameObjectToLoad == null) gameObjectToLoad = InstantiatePrefab(data);
        
        // 2. Lade Daten in das GameObject
        gameObjectToLoad.GetComponent<SaveableGameObject>().LoadData(data);
        
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
                newInitializer.name = $"Initializer of scene {targetScene.name}"; //um mögliches Debugging zu vereinfachen, damit nicht alle Initializer "Initializer(Clone)" heißen
                var newGuid = Guid.NewGuid();
                var guidComponent = newInitializer.GetComponent<GuidComponent>();
                guidComponent.SetGuid(newGuid);
                initializers.Add(targetScene, guidComponent.GetGuid());
            }
            initializerParent = initializers[targetScene];
            parents.Add(gameObjectToLoad.transform, initializerParent);
        }
    }

    private GameObject InstantiatePrefab(SaveableGameObjectData _objectData)
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
        else Debug.LogError("Konnte Prefab nicht finden, das beim Laden instantiiert werden sollte! (" + _objectData.prefabName + ") Beim GameObject: " + _objectData.gameObjectName);

        return newGameObject;
    }

    private void LoadScriptableObject(SaveableObjectData _loadedObjectData)
    {
        saveableScriptableObjects.LoadScriptableObject(_loadedObjectData as SaveableScriptableObjectData);
    }

    public void RegisterSaveableGameObject(ISaveable _saveable)
    {
        if(!saveableGameObjects.Contains(_saveable))
            saveableGameObjects.Add(_saveable);
    }

    public void UnregisterSaveableGameObject(ISaveable _saveable)
    {
        if(saveableGameObjects.Contains(_saveable))
            saveableGameObjects.Remove(_saveable);
    }
}
}