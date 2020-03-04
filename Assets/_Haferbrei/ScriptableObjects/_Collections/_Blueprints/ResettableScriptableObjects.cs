//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Haferbrei {
[CreateAssetMenu(fileName = "ResettableScriptableObjects", menuName = "Scriptable Objects/Collections/Resettables", order = 0)]
public class ResettableScriptableObjects : SerializedScriptableObject, IResettable
{
    [SerializeField, Delayed] protected string folder;
    [SerializeField, Delayed] protected List<string> foldersToIgnore;
    [ReadOnly, SerializeField] public Dictionary<string, ScriptableObject> collection = new Dictionary<string, ScriptableObject>();
    [ReadOnly, SerializeField] private Dictionary<string, ScriptableObject> onDisk = new Dictionary<string, ScriptableObject>(); //just for the inspector / debugging
    [ReadOnly, SerializeField] private Dictionary<string, ScriptableObject> instantiatedAtRuntime = new Dictionary<string, ScriptableObject>(); //just for the inspector / debugging

    public void ResetAll()
    {
        foreach (var resettable in collection)
        {
            (resettable.Value as IResettable).ResetSelf();
        }
    }

    public void ResetSelf()
    {
        foreach (var so in instantiatedAtRuntime)
        {
            //Debug.Log("Destroy: " + so.Value.name);
            Destroy(so.Value);
            collection = new Dictionary<string, ScriptableObject>(onDisk);
        }
        instantiatedAtRuntime.Clear();
    }
    
    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        List<ScriptableObject> scriptableObjectsFoundInAssets = Wichtel.UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<ScriptableObject>(foldersToSearch);

        collection.Clear();
        onDisk.Clear();

        foreach(ScriptableObject so in scriptableObjectsFoundInAssets)
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
                if (!(so is IResettable)) continue;
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