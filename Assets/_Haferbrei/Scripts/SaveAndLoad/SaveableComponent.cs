//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Haferbrei {
[RequireComponent(typeof(GuidComponent))]
public class SaveableComponent : MonoBehaviour, ISaveable
{
    [SerializeField, Required] private SaveLoadController saveLoadController;
    
    public SaveableData SaveData()
    {
        throw new NotImplementedException();
    }

    public void LoadData()
    {
        throw new NotImplementedException();
    }

    public void InitSaveable()
    {
        saveLoadController.RegisterSaveableComponent(this);
    }

    public void OnDestroy()
    {
        saveLoadController.UnregisterSaveableComponent(this);
    }
}


[Serializable]
public class SaveableComponentData : SaveableData
{
    public Guid parentGuid;
    public Transform transform;
    public string prefabName;
}
}