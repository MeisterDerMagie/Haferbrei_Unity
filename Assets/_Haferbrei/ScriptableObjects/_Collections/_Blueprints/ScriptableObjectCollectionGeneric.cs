﻿//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Haferbrei {
public abstract class ScriptableObjectCollectionGeneric<T> : SerializedScriptableObject, IResettable where T : ScriptableObject
{
    [SerializeField, Delayed] protected string folder;
    [SerializeField, Delayed] protected List<string> foldersToIgnore;
    [ReadOnly, SerializeField] public Dictionary<string, T> collection = new Dictionary<string, T>();
    [ReadOnly, SerializeField] private Dictionary<string, T> onDisk = new Dictionary<string, T>(); //just for the inspector / debugging
    [ReadOnly, SerializeField] private Dictionary<string, T> instantiatedAtRuntime = new Dictionary<string, T>(); //just for the inspector / debugging

    public void RegisterScriptableObject(T _scriptableObject)
    {
        if (collection.ContainsKey(_scriptableObject.name)) return;
        
        collection.Add(_scriptableObject.name, _scriptableObject);
        if(Application.isPlaying) instantiatedAtRuntime.Add(_scriptableObject.name, _scriptableObject);
    }

    public void UnregisterScriptableObject(T _scriptableObject)
    {
        if (!collection.ContainsKey(_scriptableObject.name)) return;
        
        collection.Remove(_scriptableObject.name);
        if (instantiatedAtRuntime.ContainsKey(_scriptableObject.name)) instantiatedAtRuntime.Remove(_scriptableObject.name);
    }
    
    public void ResetSelf()
    {
        foreach (var so in instantiatedAtRuntime)
        {
            //Debug.Log("Destroy: " + so.Value.name);
            Destroy(so.Value);
            collection = new Dictionary<string, T>(onDisk);
        }
        instantiatedAtRuntime.Clear();
    }


    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        List<T> scriptableObjectsFoundInAssets = Wichtel.UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<T>(foldersToSearch);

        collection.Clear();
        onDisk.Clear();

        foreach(T so in scriptableObjectsFoundInAssets)
        {
            string assetPath = AssetDatabase.GetAssetPath(so);
            bool ignoreThisScriptableObject = false;
            foreach (var folderPath in foldersToIgnore) 
            {
                if(assetPath.Contains(folderPath)) ignoreThisScriptableObject = true;
            }
            if(ignoreThisScriptableObject) continue;

            if (!collection.ContainsKey(so.name))
            {
                collection.Add(so.name, so);
                onDisk.Add(so.name, so);
            }
            else
            {
                Debug.LogError("Achtung, es gibt zwei oder mehr ScriptableObjects mit demselben Namen! (" + so.name + ")");
            }
        }
    }
    
    private void OnValidate()
    {
        RefreshDictionary();
    }
    #endif
}
}