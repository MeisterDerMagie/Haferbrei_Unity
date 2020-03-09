//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "ScriptableObjectCollection", menuName = "Scriptable Objects/Collections/ScriptableObject Collection", order = 0)]
public class ScriptableObjectCollection : SerializedScriptableObject
{
    [Delayed, SerializeField] private string folder;
    [Delayed, SerializeField] private List<string> foldersToIgnore;
    [ReadOnly, SerializeField] public Dictionary<string, ScriptableObject> scriptableObjects = new Dictionary<string, ScriptableObject>();
    
    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        List<ScriptableObject> scriptableObjectsFoundInAssets = Wichtel.UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<ScriptableObject>(foldersToSearch);

        scriptableObjects.Clear();

        foreach(ScriptableObject so in scriptableObjectsFoundInAssets)
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
            }
            else
            {
                Debug.LogError("Achtung, es gibt zwei oder mehr ScriptableObjects mit demselben Namen! (" + so.name + ")", this);
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