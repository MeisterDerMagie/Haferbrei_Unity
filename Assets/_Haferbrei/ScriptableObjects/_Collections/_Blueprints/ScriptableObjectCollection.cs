//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "ScriptableObjectCollection", menuName = "Scriptable Objects/Collections/ScriptableObject Collection", order = 0)]
public class ScriptableObjectCollection : SerializedScriptableObject
{
    [SerializeField, Delayed] private string folder;
    [ReadOnly, SerializeField] private Dictionary<string, ScriptableObject> scriptableObjectReferences = new Dictionary<string, ScriptableObject>();
    
    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        IEnumerable scriptableObjects = Wichtel.UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<ScriptableObject>(foldersToSearch);

        scriptableObjectReferences.Clear();

        foreach(ScriptableObject so in scriptableObjects)
        {
            if(!scriptableObjectReferences.ContainsKey(so.name)) scriptableObjectReferences.Add(so.name, so);
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