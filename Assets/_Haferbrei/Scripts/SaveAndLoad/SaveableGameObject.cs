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
    
    public SaveableData SaveData()
    {
        SaveableGameObjectData data = new SaveableGameObjectData();

        //save own data
        data.sceneName = gameObject.scene.name;
        data.guid = GetComponent<GuidComponent>().GetGuid();
        data.saveableType = "GameObject";
        data.prefabName = prefabName;
        data.gameObjectName = gameObject.name;
        
        //IN DIESER ZEILE IST EIN BUG: beim Laden
        data.parentGuid = (transform.parent == null || transform.parent.GetComponent<GuidComponent>() == null) ? Guid.Empty : transform.parent.GetComponent<GuidComponent>().GetGuid();

        //save component data
        var componentDatas = new List<SaveableComponentData>();
        foreach (var component in saveableComponents) componentDatas.Add(component.StoreData());
        data.componentDatas = componentDatas;
        
        return data;
    }

    public void LoadData(SaveableData _loadedData)
    {
        var data = _loadedData as SaveableGameObjectData;
        
        //load own data
        gameObject.name = data.gameObjectName;

        //load component data
        foreach (var componentData in data.componentDatas)
        {
            foreach (var component in saveableComponents)
            {
                if(component.componentID == componentData.componentID) component.RestoreData(componentData);
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
public class SaveableGameObjectData : SaveableData
{
    public string sceneName;
    public Guid parentGuid;
    public string prefabName;
    public string gameObjectName;
    public List<SaveableComponentData> componentDatas;
}
}