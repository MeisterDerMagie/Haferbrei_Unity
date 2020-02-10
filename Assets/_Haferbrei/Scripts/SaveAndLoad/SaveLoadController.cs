//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGamePro;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wichtel;

namespace Haferbrei {
[CreateAssetMenu(fileName = "SaveLoadController", menuName = "Scriptable Objects/SaveLoadController", order = 0)]
public class SaveLoadController : SerializedScriptableObject
{
    public PrefabCollection allPrefabsCollection;
    public SceneCollection allScenesCollection;
    
    public List<SaveableData> loadedData = new List<SaveableData>();
    public List<SaveableData> dataToSave = new List<SaveableData>();

    public List<ISaveable> saveableGameObjects = new List<ISaveable>();
    
    private Dictionary<Guid, Transform> parents = new Dictionary<Guid, Transform>();
    
    [Button, DisableInEditorMode]
    public void SaveGameState(string _saveGameFileName)
    {
        dataToSave.Clear();
        
        foreach (var saveableGameObject in saveableGameObjects)
        {
            SaveableData data = saveableGameObject.SaveData();
            dataToSave.Add(data);
        }
        
        SaveGame.Save(_saveGameFileName, dataToSave);
        Debug.Log("Game saved!");
    }

    [Button, DisableInEditorMode]
    public void LoadGameState(string _saveGameFileName)
    {
        Debug.Log("Start loading game @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay );
        Timing.RunCoroutine(_LoadGameState(_saveGameFileName));
    }
    
    private IEnumerator<float> _LoadGameState(string _saveGameFileName)
    {
        loadedData = SaveGame.Load<List<SaveableData>>(_saveGameFileName, null);
        parents.Clear();
        
        //load all necessary scenes
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(_LoadNecessaryScenes()));

        Debug.Log("Loaded all scenes @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);
        
        //load data
        foreach (var data in loadedData)
        {
            if      (data.saveableType == "GameObject")       LoadGameObject(data); //yield return Timing.WaitUntilDone(Timing.RunCoroutine(_LoadGameObject(data)));
            else if (data.saveableType == "ScriptableObject") LoadScriptableObject(data);
            else Debug.LogError("Can't load object of type: \"" + data.saveableType + "\"");
        }
        
        //set parents
        SetParents();

        Debug.Log("Game loaded @ frame #" + Time.frameCount + " / time: " + DateTime.Now.TimeOfDay);
    }

    private void SetParents()
    {
        foreach (var pair in parents)
        {
            var parent = GuidManager.ResolveGuid(pair.Key);
            if (parent == null) { Debug.LogError("Parent doesn't exist! Guid: " + pair.Key); continue; }
            pair.Value.SetParent(parent.transform, false);
        }
    }

    private IEnumerator<float> _LoadNecessaryScenes()
    {
        //Welche Szenen müssen geladen werden?
        var necessaryScenes = new List<string>();
        foreach (var data in loadedData)
        {
            var gameObjectData = data as SaveableGameObjectData;
            if(!necessaryScenes.Contains(gameObjectData.sceneName)) necessaryScenes.Add(gameObjectData.sceneName);
        }
        
        //Lade diese Szenen
        foreach (var sceneName in necessaryScenes)
        {
            if (!SceneUtilitiesW.IsSceneLoaded(sceneName))
            {
                var scene = allScenesCollection.GetScene(sceneName);
                yield return Timing.WaitUntilDone(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));
                Debug.Log("loaded Scene: " + scene.ScenePath);
            }
        }
    }

    private void LoadGameObject(SaveableData _loadedData)
    {
        var data = _loadedData as SaveableGameObjectData;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(data.sceneName));
        
        // 1. Existiert das GameObjekt mit der Guid? Falls nicht, muss das Prefab instantiiert werden.
        var gameObjectToLoad = GuidManager.ResolveGuid(data.guid);
        if (gameObjectToLoad == null) gameObjectToLoad = InstantiatePrefab(data);
        
        // 2. Lade Daten in das GameObject
        gameObjectToLoad.GetComponent<SaveableGameObject>().LoadData(data);
        
        // 3. Falls das GameObject einen Parent hatte, wird der hier vermerkt, damit es den später bekommt
        if (data.parentGuid != Guid.Empty) parents.Add(data.parentGuid, gameObjectToLoad.transform);
        
        // 4. Initialisiere die SaveableComponent? Weiß nicht, ob das hier passieren sollte.
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
        //hier wird das scriptableObject geladen
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