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
public class SaveablePrefab : MonoBehaviour, ISaveablePrefab
{
    [SerializeField, ReadOnly, ShowIf("prefabNameIsNotNullOrEmpty")] private string prefabName;
    [SerializeField, ReadOnly] public List<ComponentGuidPair> saveableComponents = new List<ComponentGuidPair>();

    private bool prefabNameIsNotNullOrEmpty => !string.IsNullOrEmpty(prefabName); //für Odin
    
    public SaveablePrefabData SaveData()
    {
        SaveablePrefabData objectData = new SaveablePrefabData();

        //save own data
        objectData.sceneName = gameObject.scene.name;
        objectData.guid = GetComponent<GuidComponent>().GetGuid();
        objectData.prefabName = prefabName;
        objectData.gameObjectName = gameObject.name;
        
        objectData.parentGuid = (transform.parent == null || transform.parent.GetComponent<GuidComponent>() == null) ? Guid.Empty : transform.parent.GetComponent<GuidComponent>().GetGuid();

        //save component guids
        objectData.components = CreateComponentGuidDataPairs();
        
        return objectData;
    }

    public void LoadData(SaveableObjectData _loadedObjectData)
    {
        var data = _loadedObjectData as SaveablePrefabData;
        
        //load own data
        gameObject.name = data.gameObjectName;

        //load component guids
        foreach (var pair in data.components)
        {
            var component = GetComponentById(pair.componentId);
            if(component != null) component.SetComponentGuid(pair.componentGuid);
            else Debug.LogError("Can't set Component Guid!");
        }
    }

    public void InitSaveablePrefab()
    {
        foreach (var pair in saveableComponents)
        {
            if(pair.component.componentGuid == Guid.Empty) pair.component.SetComponentGuid(Guid.NewGuid());
        }
    }

    private ComponentGuidPairData[] CreateComponentGuidDataPairs()
    {
        ComponentGuidPairData[] pairs = new ComponentGuidPairData[saveableComponents.Count];
        
        for (int i = 0; i < saveableComponents.Count; i++)
        {
            pairs[i].componentId = saveableComponents[i].componentId;
            pairs[i].componentGuid = saveableComponents[i].component.componentGuid;
        }

        return pairs;
    }
    
    private void CreatePairs() //on own object and in all children
    {
        LeereEintrageEntfernen();
        
        SaveableComponent[] saveableComponentInChildrenAndOnObjectSelf = GetComponentsInChildren<SaveableComponent>(true);

        foreach (var component in saveableComponentInChildrenAndOnObjectSelf)
        {
            if(SaveableComponentsContains(component)) continue;
            saveableComponents.Add(CreateNewPair(component));
        }
    }

    private ComponentGuidPair CreateNewPair(SaveableComponent _saveableComponent)
    {
        var newPair = new ComponentGuidPair
        {
            component = _saveableComponent,
            componentId = Guid.NewGuid().ToString()
        };
        return newPair;
    }

    private bool SaveableComponentsContains(SaveableComponent _saveableComponent)
    {
        foreach (var pair in saveableComponents) { if (pair.component == _saveableComponent) return true; }
        return false;
    }

    private void LeereEintrageEntfernen()
    {
        for (int i = saveableComponents.Count-1; i >= 0; i--)
        {
            if(saveableComponents[i].component == null) saveableComponents.RemoveAt(i);
        }
    }

    private SaveableComponent GetComponentById(string _componentId)
    {
        foreach (var pair in saveableComponents)
        {
            if(pair.componentId == _componentId) return pair.component;
        }
        return null;
    }

    #if UNITY_EDITOR
    #region SetPrefabNameForReference
    private void OnValidate()
    {
        if (this.IsAssetOnDisk())
        {
            prefabName = gameObject.name;
            
            //Guids in SaveableComponents der Children setzen
            CreatePairs();
        }
        
        this.MoveComponentAtIndex(2);
    }
    #endregion
    #endif
}


[Serializable]
public class SaveablePrefabData : SaveableObjectData
{
    public string sceneName;
    public Guid parentGuid;
    public string prefabName;
    public string gameObjectName;
    public ComponentGuidPairData[] components;
}

[Serializable]
public struct ComponentGuidPair
{
    public SaveableComponent component;
    public string componentId;   //Prefab interne Id, ist bei jeder Instanz des Prefabs gleich. Dient dazu, die Component eindeutig zuordnen zu können.
}

[Serializable]
public struct ComponentGuidPairData
{
    public string componentId;   //Prefab interne Id, ist bei jeder Instanz des Prefabs gleich. Dient dazu, die Component eindeutig zuordnen zu können.
    public Guid componentGuid; //Guid der Component, ist bei jeder Instanz des Prefabs anders und dadurch einzigartig
}
}