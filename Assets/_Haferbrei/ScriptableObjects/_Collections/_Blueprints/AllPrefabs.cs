//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {

[CreateAssetMenu(fileName = "AllPrefabs", menuName = "Scriptable Objects/Collections/All Prefabs", order = 0)]
public class AllPrefabs : SerializedScriptableObject
{
    [SerializeField, Delayed] private string folder;
    [ReadOnly, SerializeField] private Dictionary<string, GameObject> allPrefabReferences = new Dictionary<string, GameObject>();
    
    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        IEnumerable allPrefabs = Wichtel.UT_EditorUtilities_W.GetAssets<GameObject>(foldersToSearch, "t:prefab");

        allPrefabReferences.Clear();

        foreach(GameObject so in allPrefabs)
        {
            if(!allPrefabReferences.ContainsKey(so.name)) allPrefabReferences.Add(so.name, so);
            else
            {
                Debug.LogError("Achtung, es gibt zwei oder mehr Prefabs mit demselben Namen! (" + so.name + ")");
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