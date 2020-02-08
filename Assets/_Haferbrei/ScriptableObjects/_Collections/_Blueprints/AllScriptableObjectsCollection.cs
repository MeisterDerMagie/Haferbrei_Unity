//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "AllScriptableObjects", menuName = "Scriptable Objects/Collections/AllScriptableObjects", order = 0)]
public class AllScriptableObjectsCollection : SerializedScriptableObject
{
    [SerializeField, Delayed] private string folder;
    [ReadOnly, SerializeField] private Dictionary<string, ScriptableObject> allScriptableObjectReferences = new Dictionary<string, ScriptableObject>();
    
    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        IEnumerable allScriptableObjects = Wichtel.UT_ScriptableObjectsUtilities_W.GetAllScriptableObjectInstances<ScriptableObject>(foldersToSearch);

        allScriptableObjectReferences.Clear();

        foreach(ScriptableObject so in allScriptableObjects)
        {
            if(!allScriptableObjectReferences.ContainsKey(so.name)) allScriptableObjectReferences.Add(so.name, so);
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