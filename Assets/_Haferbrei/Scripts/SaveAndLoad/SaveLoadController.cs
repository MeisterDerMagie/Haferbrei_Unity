//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Bayat.SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class SaveLoadController : MonoBehaviour, IInitSingletons
{
    public string saveFileName;
    private List<object> dataToSave = new List<object>();

    public static Action<object> registerSaveable = delegate(object _saveable) {  };
    public static Action<object> unregisterSaveable = delegate(object _saveable) {  };
    
    
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
        
        registerSaveable   += RegisterSaveable;
        unregisterSaveable += UnregisterSaveable;
    }
    #endregion
    //--- ---

    [Button]
    public void Save()
    {
        SaveSystemAPI.SaveAsync(saveFileName, dataToSave);
    }

    [Button]
    public async void Load()
    {
        await SaveSystemAPI.LoadIntoAsync(saveFileName, dataToSave);
    }
    
    private void RegisterSaveable(object _saveable)
    {
        dataToSave.Add(_saveable);
    }
    private void UnregisterSaveable(object _saveable)
    {
        if (dataToSave.Contains(_saveable)) dataToSave.Remove(_saveable);
    }

    private void OnDestroy()
    {
        registerSaveable   -= RegisterSaveable;
        unregisterSaveable -= UnregisterSaveable;
    }
}
}