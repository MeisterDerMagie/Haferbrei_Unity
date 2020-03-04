﻿//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Bayat.SaveSystem;
using MEC;
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
    public GameObject initializerPrefab;
    
    public List<SaveableData> loadedData = new List<SaveableData>();
    public List<SaveableData> dataToSave = new List<SaveableData>();

    public List<ISaveable> saveableGameObjects = new List<ISaveable>();
    
    private Dictionary<Transform, Guid> parents = new Dictionary<Transform, Guid>();
    private Dictionary<Scene, Guid> initializers = new Dictionary<Scene, Guid>();
    private string saveGameFileName;
    
    [Button, DisableInEditorMode]
    public void SaveGameState(string _saveGameFileName)
    {
        dataToSave.Clear();
        
        //Save GameObjects
        List<ISaveable> emptySaveablesToRemove = new List<ISaveable>(); //anscheinend kann es selten passieren, dass leere Einträge übrig bleiben. Hier wird sich darum gekümmert.
        foreach (var saveableGameObject in saveableGameObjects)
        {
            if (saveableGameObject == null)
            {
                emptySaveablesToRemove.Add(saveableGameObject);
                continue;
            }
            SaveableData data = saveableGameObject.SaveData();
            dataToSave.Add(data);
        }
        foreach (var emptyEntry in emptySaveablesToRemove) saveableGameObjects.Remove(emptyEntry);

        //Save ScriptableObjects
        
        
        //Write file
        SaveSystemAPI.SaveAsync(_saveGameFileName, dataToSave);
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

        loadSaveGame = false;
        loadedData.Clear();
        SaveSystemAPI.LoadIntoAsync<List<SaveableData>>(saveGameFileName, loadedData);
        parents.Clear();
        initializers.Clear();
        
        //load all necessary scenes
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(_LoadNecessaryScenes()));

        Debug.Log("Loaded all scenes @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);
        
        //load data
        foreach (var data in loadedData)
        {
            if      (data.saveableType == "ScriptableObject") LoadScriptableObject(data);
            else if (data.saveableType == "GameObject")       LoadGameObject(data); //yield return Timing.WaitUntilDone(Timing.RunCoroutine(_LoadGameObject(data)));
            else Debug.LogError("Can't load object of type: \"" + data.saveableType + "\"");
        }
        
        //set parents
        SetParents();
        
        Debug.Log("Loading complete @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);
    }

    private void SetParents()
    {
        foreach (var pair in parents)
        {
            var parent = GuidManager.ResolveGuid(pair.Value);
            if (parent == null) { Debug.LogError("Parent doesn't exist! Guid: " + pair.Key); continue; }
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

    private void LoadGameObject(SaveableData _loadedData)
    {
        var data = _loadedData as SaveableGameObjectData;
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
            Guid initializerParent = new Guid();
            if (!initializers.ContainsKey(targetScene)) initializers.Add(targetScene, Instantiate(initializerPrefab).GetComponent<GuidComponent>().GetGuid()); //if this scene doesn't already have an initializer, create one
            initializerParent = initializers[targetScene];
            parents.Add(gameObjectToLoad.transform, initializerParent);
        }
    }

    private GameObject InstantiatePrefab(SaveableGameObjectData _data)
    {
        GameObject newGameObject = null;
        var prefabToInstantiate = allPrefabsCollection.GetPrefab(_data.prefabName);
        if (prefabToInstantiate != null)
        {
            //instantiiere Prefab
            newGameObject = Instantiate(prefabToInstantiate);
            //Setze die Guid auf dem neu erstellten GameObjekt.
            newGameObject.GetComponent<GuidComponent>().SetGuid(_data.guid);
        }
        else Debug.LogError("Konnte Prefab nicht finden, das beim Laden instantiiert werden sollte! (" + _data.prefabName + ")");

        return newGameObject;
    }

    private void LoadScriptableObject(SaveableData _loadedData)
    {
        // 1. Nicht existierende SOs erstellen
        
        
        // 2. Daten laden
        
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