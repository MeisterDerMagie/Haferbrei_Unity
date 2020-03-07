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

    [SerializeField, ReadOnly] private List<Guid> guidsOnDisk = new List<Guid>();
    [SerializeField, ReadOnly] private List<ScriptableObject> SOsOnDisk = new List<ScriptableObject>();
    [SerializeField, ReadOnly] private List<Guid> guidsInstantiatedAtRuntime = new List<Guid>();
    [SerializeField, ReadOnly] private List<ScriptableObject> SOsInstantiatedAtRuntime = new List<ScriptableObject>();
    
    //Muss aufgerufen werden, bevor GameObjects gespeichert werden!
    public void CollectAllRuntimeInstantiatedSOs()
    {
        var allSaveableScriptableObjects = FindObjectsOfType<ScriptableObject>();

        foreach (var so in allSaveableScriptableObjects)
        {
            if(!(so is ISaveableScriptableObject) || SOsOnDisk.Contains(so) || SOsInstantiatedAtRuntime.Contains(so)) continue;
            RegisterNewSoCreatedAtRuntime(so);
        }
    }
    
    //--- Save Data ---
    public List<SaveableData> SaveScriptableObjects()
    {
        var data = new List<SaveableData>();
        //Disc
        for (int i = 0; i < SOsOnDisk.Count; i++)
        {
            var soData = GetSoSaveableData(i, true);
            data.Add(soData);
        }
        //Runtime
        for (int i = 0; i < SOsInstantiatedAtRuntime.Count; i++)
        {
            var soData = GetSoSaveableData(i, false);
            data.Add(soData);
        }

        return data;
    }

    private SaveableSOData GetSoSaveableData(int i, bool isDiskSO)
    {
        var so = isDiskSO ? SOsOnDisk[i] : SOsInstantiatedAtRuntime[i];
        var soAsSaveable = so as ISaveableScriptableObject;
        Debug.Log(so.name);
        var soData = soAsSaveable.SaveData();
        soData.guid = isDiskSO ? guidsOnDisk[i] : guidsInstantiatedAtRuntime[i];
        soData.saveableType = "ScriptableObject";
        soData.scriptableObjectName = so.name;
        soData.scriptableObjectType = so.GetType().FullName;
        return soData;
    }
    //--- ---
    
    //--- Load Data ---
    public void LoadScriptableObject(SaveableSOData _data)
    {
        if (guidsOnDisk.Contains(_data.guid))
        {
            int index = guidsOnDisk.IndexOf(_data.guid);
            var so = SOsOnDisk[index];
            (so as ISaveableScriptableObject).LoadData(_data);
            return;
        }
        
        Type soType = Type.GetType(_data.scriptableObjectType);
        var newSo = ScriptableObject.CreateInstance(soType);
        
        SOsInstantiatedAtRuntime.Add(newSo);
        guidsInstantiatedAtRuntime.Add(_data.guid);
        
        (newSo as ISaveableScriptableObject).LoadData(_data);
    }
    //--- ---
    
    private Guid RegisterNewSoCreatedAtRuntime(ScriptableObject _scriptableObject)
    {
        SOsInstantiatedAtRuntime.Add(_scriptableObject);
        var newGuid = Guid.NewGuid();
        guidsInstantiatedAtRuntime.Add(newGuid);
        return newGuid;
    }
    
    public ScriptableObject ResolveGuid(Guid _guid)
    {
        if (guidsOnDisk.Contains(_guid)) return SOsOnDisk[guidsOnDisk.IndexOf(_guid)];
        if (guidsInstantiatedAtRuntime.Contains(_guid)) return SOsInstantiatedAtRuntime[guidsInstantiatedAtRuntime.IndexOf(_guid)];
        else
        {
            Debug.LogError("Can't resolve Guid: " + _guid);
            return null;
        }
    }

    public Guid ResolveReference(ScriptableObject _scriptableObject)
    {
        if (SOsOnDisk.Contains(_scriptableObject)) return guidsOnDisk[SOsOnDisk.IndexOf(_scriptableObject)];
        if (SOsInstantiatedAtRuntime.Contains(_scriptableObject)) return guidsInstantiatedAtRuntime[SOsInstantiatedAtRuntime.IndexOf(_scriptableObject)];
        else
        {
            Debug.LogError("Can't resolve ScriptableObjectReference: " + _scriptableObject);
            return Guid.Empty;
        }
    }
    
    //IResettable
    public void ResetSelf()
    {
        foreach (var so in SOsInstantiatedAtRuntime)
        {
            //Debug.Log("Destroy: " + so.Value.name);
            Destroy(so);
        }
        guidsInstantiatedAtRuntime.Clear();
        SOsInstantiatedAtRuntime.Clear();
    }
    
    //Collect all prebuilt SaveableScriptableObjects on disk 
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

            if (!SOsOnDisk.Contains(so))
            {
                if (!(so is ISaveableScriptableObject)) continue;
                guidsOnDisk.Add(Guid.NewGuid());
                SOsOnDisk.Add(so);
            }
        }
        
        //Remove empty occurences
        for (int i = SOsOnDisk.Count -1; i >= 0; i--)
        {
            if(SOsOnDisk[i] != null) continue;
            SOsOnDisk.RemoveAt(i);
            guidsOnDisk.RemoveAt(i);
        }
    }
    
    private void OnValidate()
    {
        RefreshDictionary();
        
        //clear null entries
        for (int i = SOsInstantiatedAtRuntime.Count-1; i >= 0; i--)
        {
            if(SOsInstantiatedAtRuntime[i] != null) continue;
            SOsInstantiatedAtRuntime.RemoveAt(i);
            guidsInstantiatedAtRuntime.RemoveAt(i);
        }
    }
    #endif
}
}