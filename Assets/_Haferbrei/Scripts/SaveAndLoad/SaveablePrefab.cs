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
public class SaveablePrefab : MonoBehaviour, ISaveable
{
    [SerializeField, ReadOnly] private string prefabName;
    [SerializeField, Required] private AllPrefabs allPrefabsCollection;
    [SerializeField, Required] private SaveLoadController saveLoadController;
    
    [Button]
    public SaveableData SaveData()
    {
        SaveablePrefabData data = new SaveablePrefabData();

        data.guid = GetComponent<GuidComponent>().GetGuid();
        data.prefabName = prefabName;
        data.type = "prefab";
        
        data.parentGuid = transform.parent.GetComponent<GuidComponent>().GetGuid();
        data.transform = transform;
        
        return data;
    }

    public void LoadData()
    {
        /*
        SaveablePrefabData _data = new SaveablePrefabData();

        if(SAL001_SaveLoadManager.instance.loadedData.ContainsKey(ID))
        {
            _data = SAL001_SaveLoadManager.instance.loadedData[ID] as SaveablePrefabData;

            
        }*/
    }

    [Button]
    public void InitSaveable()
    {
        saveLoadController.RegisterSaveablePrefab(this);
    }

    public void OnDestroy()
    {
        saveLoadController.UnregisterSaveablePrefab(this);
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
public class SaveablePrefabData : SaveableData
{
    public Guid parentGuid;
    public Transform transform;
    public string prefabName;
}
}