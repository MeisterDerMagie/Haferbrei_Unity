//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Haferbrei {
public abstract class ScriptableObjectCollectionGeneric<T> : ScriptableObjectWithGuid where T : ScriptableObjectWithGuid
{
    [SerializeField, Delayed] protected string folder;
    [SerializeField, Delayed] protected List<string> foldersToIgnore;
    [ReadOnly, SerializeField] protected Dictionary<Guid, T> scriptableObjects = new Dictionary<Guid, T>();
    
    #if UNITY_EDITOR
    [ReadOnly, SerializeField, BoxGroup("Just for Info")] private Dictionary<Guid, T> scriptableObjectsOnDisk = new Dictionary<Guid, T>(); //just for the inspector / debugging
    [ReadOnly, SerializeField, BoxGroup("Just for Info")] private Dictionary<Guid, T> scriptableObjectsInstantiatedAtRuntime = new Dictionary<Guid, T>(); //just for the inspector / debugging
    #endif
    
    public void RegisterScriptableObject(T _scriptableObject)
    {
        if (scriptableObjects.ContainsKey(_scriptableObject.guid)) return;
        
        scriptableObjects.Add(_scriptableObject.guid, _scriptableObject);
        #if UNITY_EDITOR
        if(Application.isPlaying) scriptableObjectsInstantiatedAtRuntime.Add(_scriptableObject.guid, _scriptableObject);
        #endif
    }

    public void UnregisterScriptableObject(T _scriptableObject)
    {
        if (!scriptableObjects.ContainsKey(_scriptableObject.guid)) return;
        
        scriptableObjects.Remove(_scriptableObject.guid);
        #if UNITY_EDITOR
        if (scriptableObjectsInstantiatedAtRuntime.ContainsKey(_scriptableObject.guid)) scriptableObjectsInstantiatedAtRuntime.Remove(_scriptableObject.guid);
        #endif
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

            if (!scriptableObjects.ContainsKey(so.guid))
            {
                scriptableObjects.Add(so.guid, so);
                scriptableObjectsOnDisk.Add(so.guid, so);
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