//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGamePro;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "SaveLoadController", menuName = "Scriptable Objects/SaveLoadController", order = 0)]
public class SaveLoadController : SerializedScriptableObject
{
    public Dictionary<Guid, SaveableData> loadedData = new Dictionary<Guid, SaveableData>();
    public Dictionary<Guid, SaveableData> dataToSave = new Dictionary<Guid, SaveableData>();

    public List<ISaveable> saveablePrefabs = new List<ISaveable>();
    public List<ISaveable> saveableComponents = new List<ISaveable>();
    
    [Button]
    public void SaveGameState(string _saveGameFileName)
    {
        dataToSave.Clear();

        foreach (var saveablePrefab in saveablePrefabs)
        {
            SaveableData data = saveablePrefab.SaveData();
            dataToSave.Add(data.guid, data);
        }
        
        
        /*
        
        //Save Managers
        foreach(ISaveable _saveableData in saveableManagers)
        {
            SaveableData _data = _saveableData.SaveData();
            dataToSave.Add( _data.ID, _data );

            //Debug.Log("Save Manager: " + _saveableData.ID);
        }

        //Save Objects
        foreach(ISaveable _saveableData in saveableObjects)
        {
            SaveableData _data = _saveableData.SaveData();
            dataToSave.Add( _data.ID, _data );

            //Debug.Log("Save Object: " + _saveableData.ID);
        }*/

        SaveGame.Save(_saveGameFileName, dataToSave);

        Debug.Log("Game saved!");
    }

    [Button]
    public void LoadGameState(string _saveGameFileName)
    {
        loadedData = SaveGame.Load<Dictionary<Guid, SaveableData>>(_saveGameFileName, null);
    }

    public void RegisterSaveablePrefab(ISaveable _saveable)
    {
        if(!saveablePrefabs.Contains(_saveable))
            saveablePrefabs.Add(_saveable);
    }

    public void UnregisterSaveablePrefab(ISaveable _saveable)
    {
        if(saveablePrefabs.Contains(_saveable))
            saveablePrefabs.Remove(_saveable);
    }
    
    public void RegisterSaveableComponent(ISaveable _saveable)
    {
        if(!saveableComponents.Contains(_saveable))
            saveableComponents.Add(_saveable);
    }
    
    public void UnregisterSaveableComponent(ISaveable _saveable)
    {
        if(saveableComponents.Contains(_saveable))
            saveableComponents.Remove(_saveable);
    }
}
}