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
public abstract class ScriptableObjectCollectionGeneric<T> : ScriptableObject, IResettable where T : ScriptableObject
{
    [SerializeField, Delayed] protected string folder;
    [SerializeField, Delayed] protected List<string> foldersToIgnore;
    [ReadOnly, SerializeField] public Dictionary<string, T> scriptableObjects = new Dictionary<string, T>();
    [ReadOnly, SerializeField] private Dictionary<string, T> scriptableObjectsOnDisk = new Dictionary<string, T>(); //just for the inspector / debugging
    [ReadOnly, SerializeField] private Dictionary<string, T> scriptableObjectsInstantiatedAtRuntime = new Dictionary<string, T>(); //just for the inspector / debugging

    public void RegisterScriptableObject(T _scriptableObject)
    {
        if (scriptableObjects.ContainsKey(_scriptableObject.name)) return;
        
        scriptableObjects.Add(_scriptableObject.name, _scriptableObject);
        if(Application.isPlaying) scriptableObjectsInstantiatedAtRuntime.Add(_scriptableObject.name, _scriptableObject);
    }

    public void UnregisterScriptableObject(T _scriptableObject)
    {
        if (!scriptableObjects.ContainsKey(_scriptableObject.name)) return;
        
        scriptableObjects.Remove(_scriptableObject.name);
        if (scriptableObjectsInstantiatedAtRuntime.ContainsKey(_scriptableObject.name)) scriptableObjectsInstantiatedAtRuntime.Remove(_scriptableObject.name);
    }
    
    public void ResetSelf()
    {
        foreach (var so in scriptableObjectsInstantiatedAtRuntime)
        {
            //Debug.Log("Destroy: " + so.Value.name);
            Destroy(so.Value);
            scriptableObjects = new Dictionary<string, T>(scriptableObjectsOnDisk);
        }
        scriptableObjectsInstantiatedAtRuntime.Clear();
    }


    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        List<T> scriptableObjectsFoundInAssets = Wichtel.UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<T>(foldersToSearch);

        scriptableObjects.Clear();
        scriptableObjectsOnDisk.Clear();

        foreach(T so in scriptableObjectsFoundInAssets)
        {
            string assetPath = AssetDatabase.GetAssetPath(so);
            bool ignoreThisScriptableObject = false;
            foreach (var folderPath in foldersToIgnore) 
            {
                if(assetPath.Contains(folderPath)) ignoreThisScriptableObject = true;
            }
            if(ignoreThisScriptableObject) continue;

            if (!scriptableObjects.ContainsKey(so.name))
            {
                scriptableObjects.Add(so.name, so);
                scriptableObjectsOnDisk.Add(so.name, so);
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