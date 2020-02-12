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
    [ReadOnly, SerializeField] protected Dictionary<Guid, T> scriptableObjectReferences = new Dictionary<Guid, T>();
    
    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        List<T> scriptableObjects = Wichtel.UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<T>(foldersToSearch);

        scriptableObjectReferences.Clear();

        foreach(T so in scriptableObjects)
        {
            string assetPath = AssetDatabase.GetAssetPath(so);
            bool ignoreThisScriptableObject = false;
            foreach (var folderPath in foldersToIgnore) 
            {
                if(assetPath.Contains(folderPath)) ignoreThisScriptableObject = true;
            }
            if(ignoreThisScriptableObject) continue;

            if(!scriptableObjectReferences.ContainsKey(so.guid)) scriptableObjectReferences.Add(so.guid, so);
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