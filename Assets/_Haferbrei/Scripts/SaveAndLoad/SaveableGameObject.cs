//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using Wichtel.Extensions;

namespace Haferbrei {
[RequireComponent(typeof(GuidComponent)), DisallowMultipleComponent, HideMonoScript]
public class SaveableGameObject : MonoBehaviour, ISaveable
{
    [SerializeField, ReadOnly, ShowIf("prefabNameIsNotNullOrEmpty")] private string prefabName;
    [SerializeField, Required] private SaveLoadController saveLoadController;
    [SerializeField, InlineEditor, ReadOnly] public List<SaveableComponent> saveableComponents = new List<SaveableComponent>();

    private bool prefabNameIsNotNullOrEmpty => !string.IsNullOrEmpty(prefabName); //für Odin
    
    public SaveableObjectData SaveData()
    {
        SaveableGameObjectData objectData = new SaveableGameObjectData();

        //save own data
        objectData.sceneName = gameObject.scene.name;
        objectData.guid = GetComponent<GuidComponent>().GetGuid();
        objectData.saveableType = "GameObject";
        objectData.prefabName = prefabName;
        objectData.gameObjectName = gameObject.name;
        
        //IN DIESER ZEILE IST EIN BUG: beim Laden
        objectData.parentGuid = (transform.parent == null || transform.parent.GetComponent<GuidComponent>() == null) ? Guid.Empty : transform.parent.GetComponent<GuidComponent>().GetGuid();

        //save component data
        var componentDatas = new List<SaveableData>();
        foreach (var component in saveableComponents) componentDatas.Add(component.StoreData());
        objectData.componentDatas = componentDatas;
        
        return objectData;
    }

    public void LoadData(SaveableObjectData _loadedObjectData)
    {
        var data = _loadedObjectData as SaveableGameObjectData;
        
        //load own data
        gameObject.name = data.gameObjectName;

        //load component data
        foreach (var componentData in data.componentDatas)
        {
            foreach (var component in saveableComponents)
            {
                if(component.componentID == componentData.objectId) component.RestoreData(componentData);
            }
        }
    }

    public void InitSaveable()
    {
        saveLoadController.RegisterSaveableGameObject(this);
    }

    public void OnDestroy()
    {
        saveLoadController.UnregisterSaveableGameObject(this);
    }

    public void AddSaveableComponent(SaveableComponent _component)
    {
        if(saveableComponents.Contains(_component)) return;

        //wenn die SaveableComponent bereits eine Guid hat, wird die nicht überschrieben, um bestehende SaveGames nicht kaputt zu machen.
        if (Guid.TryParse(_component.componentID, out Guid guid))
        {
            if (guid == Guid.Empty) guid = Guid.NewGuid();
        }
        else
        {
            guid = Guid.NewGuid();
        }
        
        _component.componentID = guid.ToString();
        saveableComponents.Add(_component);
    }

    public void RemoveSaveableComponent(SaveableComponent _component)
    {
        if (saveableComponents.Contains(_component)) saveableComponents.Remove(_component);
    }


    #if UNITY_EDITOR
    #region SetPrefabNameForReference
    private void OnValidate()
    {
        if (this.IsAssetOnDisk())
        {
            prefabName = gameObject.name;
        }

        CallOnValidateForSaveableComponentsInChildren();
        
        this.MoveComponentAtIndex(2);
    }

    private void CallOnValidateForSaveableComponentsInChildren()
    {
        foreach (var sc in GetComponentsInChildren<SaveableComponent>(true))
        {
            sc.OnValidate();
        }
    }
    #endregion
    #endif
}


[Serializable]
public class SaveableGameObjectData : SaveableObjectData
{
    public string sceneName;
    public Guid parentGuid;
    public string prefabName;
    public string gameObjectName;
    public List<SaveableData> componentDatas;
}
}