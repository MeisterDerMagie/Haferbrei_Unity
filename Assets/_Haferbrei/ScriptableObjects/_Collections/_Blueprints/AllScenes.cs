//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Haferbrei {
[CreateAssetMenu(fileName = "AllScenes", menuName = "Scriptable Objects/Collections/All Scenes", order = 0)]
public class AllScenes : SerializedScriptableObject
{
    [SerializeField, Delayed] private string folder;
    [ReadOnly, SerializeField] private Dictionary<string, SceneReference> allSceneReferences = new Dictionary<string, SceneReference>();

    public SceneReference GetScene(string _name)
    {
        var scene = (allSceneReferences.ContainsKey(_name)) ? allSceneReferences[_name] : null;
        return scene;
    }
    
    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        IEnumerable allScenes = Wichtel.UT_EditorUtilities_W.GetAssets<SceneAsset>(foldersToSearch, "t:scene");

        allSceneReferences.Clear();

        foreach(SceneAsset sceneAsset in allScenes)
        {
            var sceneReference = new SceneReference();
            sceneReference.ScenePath = AssetDatabase.GetAssetPath(sceneAsset);
            if(!allSceneReferences.ContainsKey(sceneAsset.name)) allSceneReferences.Add(sceneAsset.name, sceneReference);
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