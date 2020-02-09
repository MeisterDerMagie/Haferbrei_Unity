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
public class SaveableGameObject : MonoBehaviour, ISaveable
{
    [SerializeField, ReadOnly] private string prefabName;
    [SerializeField, Required] private AllPrefabs allPrefabsCollection;
    [SerializeField, Required] private SaveLoadController saveLoadController;
    [SerializeField, InlineEditor, ReadOnly] public List<SaveableComponent> saveableComponents = new List<SaveableComponent>();
    
    [Button]
    public SaveableData SaveData()
    {
        SaveableGameObjectData data = new SaveableGameObjectData();

        //save own data
        data.sceneName = gameObject.scene.name;
        data.guid = GetComponent<GuidComponent>().GetGuid();
        data.saveableType = "GameObject";
        data.prefabName = prefabName;
        data.gameObjectName = gameObject.name;
        data.parentGuid = (transform.parent != null) ? transform.parent.GetComponent<GuidComponent>().GetGuid() : Guid.Empty;

        //save component data
        var componentDatas = new List<SaveableComponentData>();
        foreach (var component in saveableComponents) componentDatas.Add(component.StoreData());
        data.componentDatas = componentDatas;
        
        return data;
    }

    public void LoadData(SaveableGameObjectData _loadedData)
    {
        //load own data
        gameObject.name = _loadedData.gameObjectName;

        //load component data
        foreach (var componentData in _loadedData.componentDatas)
        {
            foreach (var component in saveableComponents)
            {
                if(component.componentID == componentData.componentID) component.RestoreData(componentData);
            }
        }
    }

    [Button]
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
        
        _component.componentID = Guid.NewGuid().ToString();
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
        if (IsAssetOnDisk())
        {
            prefabName = gameObject.name;
        }
    }
    
    private bool IsAssetOnDisk()
    {
        return PrefabUtility.IsPartOfPrefabAsset(this) || IsEditingInPrefabMode();
    }
    
    private bool IsEditingInPrefabMode()
    {
        if (EditorUtility.IsPersistent(this))
        {
            // if the game object is stored on disk, it is a prefab of some kind, despite not returning true for IsPartOfPrefabAsset =/
            return true;
        }
        else
        {
            // If the GameObject is not persistent let's determine which stage we are in first because getting Prefab info depends on it
            var mainStage = StageUtility.GetMainStageHandle();
            var currentStage = StageUtility.GetStageHandle(gameObject);
            if (currentStage != mainStage)
            {
                var prefabStage = PrefabStageUtility.GetPrefabStage(gameObject);
                if (prefabStage != null)
                {
                    return true;
                }
            }
        }
        return false;
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