//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Haferbrei {
[CreateAssetMenu(fileName = "SaveableScriptableObjectReferences", menuName = "Scriptable Objects/Save & Load/SaveableScriptableObjectReferencesCollection", order = 0)]
public class SaveableScriptableObjectReferences : SerializedScriptableObject
{
    //
    //Todo: diese Klasse mit den SaveableScriptableObjects zusammenlegen, um einen zentralen Ansprechpunkt für ScriptableObjects zu haben.
    //
    
    [SerializeField, Delayed] protected string folder;
    [SerializeField, Delayed] protected List<string> foldersToIgnore;

    [SerializeField, ReadOnly] private List<Guid> guids = new List<Guid>();
    [SerializeField, ReadOnly] private List<ScriptableObject> scriptableObjects = new List<ScriptableObject>();

    
    public ScriptableObject ResolveGuid(Guid _guid)
    {
        if (guids.Contains(_guid)) return scriptableObjects[guids.IndexOf(_guid)];
        else
        {
            Debug.LogError("Can't resolve Guid: " + _guid);
            return null;
        }
    }

    public Guid ResolveReference(ScriptableObject _scriptableObject)
    {
        if (scriptableObjects.Contains(_scriptableObject)) return guids[scriptableObjects.IndexOf(_scriptableObject)];
        else
        {
            Debug.LogError("Can't resolve ScriptableObjectReference: " + _scriptableObject);
            return Guid.Empty;
        }
    }
    
    
    //Collect all ScriptableObjects on disk
    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        List<ScriptableObject> scriptableObjectsFoundInAssets = Wichtel.UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<ScriptableObject>(foldersToSearch);
        
        foreach(ScriptableObject so in scriptableObjectsFoundInAssets)
        {
            string assetPath = AssetDatabase.GetAssetPath(so);
            bool ignoreThisScriptableObject = false;
            foreach (var folderPath in foldersToIgnore) 
            {
                if(assetPath.Contains(folderPath)) ignoreThisScriptableObject = true;
            }
            if(ignoreThisScriptableObject) continue;

            if (!scriptableObjects.Contains(so))
            {
                guids.Add(Guid.NewGuid());
                scriptableObjects.Add(so);
            }
        }
        
        //Remove empty occurences
        for (int i = scriptableObjects.Count -1; i >= 0; i--)
        {
            if(scriptableObjects[i] != null) continue;
            scriptableObjects.RemoveAt(i);
            guids.RemoveAt(i);
        }
    }
    
    private void OnValidate()
    {
        RefreshDictionary();
    }
    #endif
}
}