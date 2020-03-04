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
[CreateAssetMenu(fileName = "SaveableScriptableObjects", menuName = "Scriptable Objects/Save & Load/SaveableScriptableObjectsCollection", order = 0)]
public class SaveableScriptableObjects : SerializedScriptableObject, IResettable
{
    [SerializeField, Delayed] protected string folder;
    [SerializeField, Delayed] protected List<string> foldersToIgnore;
    [ShowInInspector, ReadOnly] public static List<Guid> guids = new List<Guid>();
    [ShowInInspector, ReadOnly] public static List<ScriptableObject> scriptableObjects = new List<ScriptableObject>();

    [ShowInInspector, ReadOnly] private static List<Guid> guidsOnDisk = new List<Guid>();
    [ShowInInspector, ReadOnly] private static List<ScriptableObject> scriptableObjectsOnDisk = new List<ScriptableObject>();
    [ShowInInspector, ReadOnly] private static List<Guid> guidsInstantiatedAtRuntime = new List<Guid>();
    [ShowInInspector, ReadOnly] private static List<ScriptableObject> scriptableInstantiatedAtRuntime = new List<ScriptableObject>();

    /*
    public void LoadAllScriptableObjects()
    {
        foreach (var resettable in collection)
        {
            (resettable.Value as ISaveableScriptableObject).LoadData();
        }
    }*/
    
    public SaveableData SaveDiskSOs()
    {
        throw new NotImplementedException();
    }

    public void ResetSelf()
    {
        foreach (var so in scriptableInstantiatedAtRuntime)
        {
            //Debug.Log("Destroy: " + so.Value.name);
            Destroy(so);
            guids = new List<Guid>(guidsOnDisk);
            scriptableObjects = new List<ScriptableObject>(scriptableObjectsOnDisk);
        }
        guidsInstantiatedAtRuntime.Clear();
        scriptableInstantiatedAtRuntime.Clear();
    }

    public static ScriptableObject ResolveGuid(Guid _guid)
    {
        if (guids.Contains(_guid)) return scriptableObjects[guids.IndexOf(_guid)];
        else
        {
            Debug.LogError("Can't resolve Guid: " + _guid);
            return null;
        }
    }

    public static Guid ResolveReference(ScriptableObject _scriptableObject)
    {
        if (scriptableObjects.Contains(_scriptableObject)) return guids[scriptableObjects.IndexOf(_scriptableObject)];
        else
        {
            Debug.LogError("Can't resolve ScriptableObjectReference: " + _scriptableObject);
            return Guid.Empty;
        }
    }
    
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

            if (!scriptableObjectsOnDisk.Contains(so))
            {
                if (!(so is ISaveableScriptableObject)) continue;
                guidsOnDisk.Add(Guid.NewGuid());
                scriptableObjectsOnDisk.Add(so);
            }
        }
        
        //Remove empty occurences
        for (int i = scriptableObjectsOnDisk.Count -1; i >= 0; i--)
        {
            if(scriptableObjectsOnDisk[i] != null) continue;
            scriptableObjectsOnDisk.RemoveAt(i);
            guidsOnDisk.RemoveAt(i);
        }
    }
    
    private void OnValidate()
    {
        RefreshDictionary();
    }
    #endif
}
}