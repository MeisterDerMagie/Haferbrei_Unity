//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGamePro;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wichtel;

namespace Haferbrei {
[CreateAssetMenu(fileName = "SaveLoadController", menuName = "Scriptable Objects/SaveLoadController", order = 0)]
public class SaveLoadController : SerializedScriptableObject
{
    public AllPrefabs allPrefabsCollection;
    
    public List<SaveableData> loadedData = new List<SaveableData>();
    public List<SaveableData> dataToSave = new List<SaveableData>();

    public List<ISaveable> saveableGameObjects = new List<ISaveable>();
    
    [Button]
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

    [Button]
    public void LoadGameState(string _saveGameFileName)
    {
        loadedData = SaveGame.Load<List<SaveableData>>(_saveGameFileName, null);

        foreach (var data in loadedData)
        {
            if      (data.saveableType == "GameObject")       LoadGameObject(data);
            else if (data.saveableType == "ScriptableObject") LoadScriptableObject(data);
            else Debug.LogError("Can't load object of type: \"" + data.saveableType + "\"");
        }

        Debug.Log("Game loaded!");
    }

    private void LoadGameObject(SaveableData _loadedData)
    {
        var data = _loadedData as SaveableGameObjectData;
        
        // 1. Ist die Szene geladen?
        if(!SceneUtilitiesW.IsSceneLoaded(data.sceneName)) { /*Hier muss die richtige Szene geladen werden. So lange warten, bis das passiert ist!!*/ }

        // 2. Existiert das ParentObjekt? Falls nicht, lade das zuerst.
        GameObject parent = null;
        if (data.parentGuid != Guid.Empty)
        {
            parent = GuidManager.ResolveGuid(data.parentGuid);
            if(parent == null) { Debug.LogWarning("Hier wird das ParentObjekt instantiiert"); /* Hier muss das ParentObjekt instantiiert werden. Also quasi diese Methode muss unterbrochen werden und für das ParentObjekt aufgerufen werden */ }
        }
        // 3. Existiert das GameObjekt mit der Guid? Falls nicht, muss das Prefab instantiiert werden.
        var gameObjectToLoad = GuidManager.ResolveGuid(data.guid);

        if (gameObjectToLoad == null)
        {
            var parentTransform = (parent != null) ? parent.transform : null;
            
            var prefabToInstantiate = allPrefabsCollection.GetPrefab(data.prefabName);
            if (prefabToInstantiate != null)
            {
                //instantiiere Prefab
                gameObjectToLoad = Instantiate(prefabToInstantiate, Vector3.zero, Quaternion.identity, parentTransform);
                //Setze die Guid auf dem neu erstellten GameObjekt.
                gameObjectToLoad.GetComponent<GuidComponent>().SetGuid(data.guid);
            }
            else Debug.LogError("Konnte Prefab nicht finden, das beim Laden instantiiert werden sollte! (" + data.prefabName + ")");
        }
        
        // 5. Lade Daten in das GameObject
        gameObjectToLoad.GetComponent<SaveableGameObject>().LoadData(data);

        //6. Initialisiere die SaveableComponent? Weiß nicht, ob das hier passieren sollte.
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