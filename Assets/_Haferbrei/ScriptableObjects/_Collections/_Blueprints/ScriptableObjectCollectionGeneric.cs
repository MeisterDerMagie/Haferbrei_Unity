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
public abstract class ScriptableObjectCollectionGeneric<T> : ScriptableObjectWithGuid, IResettable where T : ScriptableObjectWithGuid
{
    [SerializeField, Delayed] protected string folder;
    [SerializeField, Delayed] protected List<string> foldersToIgnore;
    [ReadOnly, SerializeField] public Dictionary<Guid, T> scriptableObjects = new Dictionary<Guid, T>();
    [ReadOnly, SerializeField] private Dictionary<Guid, T> scriptableObjectsOnDisk = new Dictionary<Guid, T>(); //just for the inspector / debugging
    [ReadOnly, SerializeField] private Dictionary<Guid, T> scriptableObjectsInstantiatedAtRuntime = new Dictionary<Guid, T>(); //just for the inspector / debugging

    public void RegisterScriptableObject(T _scriptableObject)
    {
        if (scriptableObjects.ContainsKey(_scriptableObject.guid)) return;
        
        scriptableObjects.Add(_scriptableObject.guid, _scriptableObject);
        if(Application.isPlaying) scriptableObjectsInstantiatedAtRuntime.Add(_scriptableObject.guid, _scriptableObject);
    }

    public void UnregisterScriptableObject(T _scriptableObject)
    {
        if (!scriptableObjects.ContainsKey(_scriptableObject.guid)) return;
        
        scriptableObjects.Remove(_scriptableObject.guid);
        if (scriptableObjectsInstantiatedAtRuntime.ContainsKey(_scriptableObject.guid)) scriptableObjectsInstantiatedAtRuntime.Remove(_scriptableObject.guid);
    }
    
    public void ResetSelf()
    {
        foreach (var so in scriptableObjectsInstantiatedAtRuntime)
        {
            //Debug.Log("Destroy: " + so.Value.name);
            Destroy(so.Value);
            scriptableObjects = new Dictionary<Guid, T>(scriptableObjectsOnDisk);
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