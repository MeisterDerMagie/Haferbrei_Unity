//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Haferbrei {
[CreateAssetMenu(fileName = "SceneCollection", menuName = "Scriptable Objects/Collections/Scene Collection", order = 0)]
public class SceneCollection : SerializedScriptableObject
{
    [SerializeField, Delayed] private string folder;
    [ReadOnly, SerializeField] private Dictionary<string, SceneReference> sceneReferences = new Dictionary<string, SceneReference>();

    public SceneReference GetScene(string _name)
    {
        var scene = (sceneReferences.ContainsKey(_name)) ? sceneReferences[_name] : null;
        return scene;
    }
    
    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        IEnumerable scenes = Wichtel.UT_EditorUtilities_W.GetAssets<SceneAsset>(foldersToSearch, "t:scene");

        sceneReferences.Clear();

        foreach(SceneAsset sceneAsset in scenes)
        {
            var sceneReference = new SceneReference();
            sceneReference.ScenePath = AssetDatabase.GetAssetPath(sceneAsset);
            if(!sceneReferences.ContainsKey(sceneAsset.name)) sceneReferences.Add(sceneAsset.name, sceneReference);
            else
            {
                Debug.LogError("Achtung, es gibt zwei oder mehr Szenen mit demselben Namen! (" + sceneAsset.name + ")");
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