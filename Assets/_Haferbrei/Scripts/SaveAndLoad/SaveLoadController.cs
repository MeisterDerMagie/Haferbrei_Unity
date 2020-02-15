//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Bayat.SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class SaveLoadController : SerializedMonoBehaviour, IInitSingletons
{
    public string saveFileName;
    public int dataEntries;
    [SerializeField] public SaveData saveData = new SaveData();

    public static Action<GameObject> registerSaveableGameObject = delegate(GameObject _saveable) {  };
    public static Action<GameObject> unregisterSaveableGameObject = delegate(GameObject _saveable) {  };
    public static Action<Component> registerSaveableComponent = delegate(Component _saveable) {  };
    public static Action<Component> unregisterSaveableComponent = delegate(Component _saveable) {  };
    
    
    //--- Singleton Behaviour ---
    #region Singleton
    public static SaveLoadController instance;
    public void InitSingleton()
    {
        if (instance == null) instance = this;
        else 
        {
            Destroy(gameObject);
            return;
        }
        
        registerSaveableGameObject   += RegisterSaveableGameObject;
        unregisterSaveableGameObject += UnregisterSaveableGameObject;
        registerSaveableComponent += RegisterSaveableComponent;
        unregisterSaveableComponent += UnregisterSaveableComponent;
    }
    #endregion
    //--- ---

    [Button]
    public void Save()
    {
        SaveSystemAPI.SaveAsync(saveFileName, saveData);
    }

    [Button]
    public async void Load()
    {
        await SaveSystemAPI.LoadIntoAsync(saveFileName, saveData);
    }
    
    private void RegisterSaveableGameObject(GameObject _saveable)
    {
        saveData.gameObjectsToSave.Add(_saveable);
        dataEntries++;
    }
    private void UnregisterSaveableGameObject(GameObject _saveable)
    {
        if (!saveData.gameObjectsToSave.Contains(_saveable)) return;
        saveData.gameObjectsToSave.Remove(_saveable);
        dataEntries--;
    }

    private void RegisterSaveableComponent(Component _saveable)
    {
        saveData.componentsToSave.Add(_saveable);
        dataEntries++;
    }

    private void UnregisterSaveableComponent(Component _saveable)
    {
        if (!saveData.componentsToSave.Contains(_saveable)) return;
        saveData.componentsToSave.Remove(_saveable);
        dataEntries--;
    }

    private void OnDestroy()
    {
        registerSaveableGameObject   -= RegisterSaveableGameObject;
        unregisterSaveableGameObject -= UnregisterSaveableGameObject;
    }
}

public class SaveData
{
    public List<GameObject> gameObjectsToSave = new List<GameObject>();
    public List<Component> componentsToSave = new List<Component>();
}
}