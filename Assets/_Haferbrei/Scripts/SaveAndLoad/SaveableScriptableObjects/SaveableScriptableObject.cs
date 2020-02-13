//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public abstract class SaveableScriptableObject : ScriptableObjectWithGuid
{
    private new void Awake()
    {
        base.Awake();
        RegisterSaveableScriptableObject();
    }
    
    private void OnDestroy()
    {
        UnregisterSaveableScriptableObject();
    }

    public void SetGuid(Guid _guid)
    {
        UnregisterSaveableScriptableObject();
        guid = _guid;
        RegisterSaveableScriptableObject();
    }

    private void RegisterSaveableScriptableObject()
    {
        if (SaveableScriptableObjectCollection.Instance != null) SaveableScriptableObjectCollection.Instance.RegisterScriptableObject(this);
    }

    private void UnregisterSaveableScriptableObject()
    {
        if(SaveableScriptableObjectCollection.Instance != null) SaveableScriptableObjectCollection.Instance.UnregisterScriptableObject(this);
    }

    public abstract SaveableScriptableObjectData Save();

    public abstract void Load(SaveableScriptableObjectData _loadedData);
}

[Serializable]
public abstract class SaveableScriptableObjectData : SaveableData
{
    public string scriptableObjectType;
    public string scriptableObjectName;
}
}