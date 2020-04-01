//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {

[CreateAssetMenu(fileName = "PrefabCollection", menuName = "Scriptable Objects/Collections/Prefab Collection", order = 0)]
public class PrefabCollection : SerializedScriptableObject
{
    [SerializeField, Delayed] private string folder;
    [ReadOnly, SerializeField] private Dictionary<string, GameObject> prefabReferences = new Dictionary<string, GameObject>();

    public GameObject GetPrefab(string _name)
    {
        var prefab = (prefabReferences.ContainsKey(_name)) ? prefabReferences[_name] : null;
        return prefab;
    }
    
    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        IEnumerable prefabs = Wichtel.UT_EditorUtilities_W.GetAssets<GameObject>(foldersToSearch, "t:prefab");

        prefabReferences.Clear();

        foreach(GameObject prefab in prefabs)
        {
            if(!prefabReferences.ContainsKey(prefab.name)) prefabReferences.Add(prefab.name, prefab);
            else
            {
                Debug.LogError("Achtung, es gibt zwei oder mehr Prefabs mit demselben Namen! (" + prefab.name + ")");
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