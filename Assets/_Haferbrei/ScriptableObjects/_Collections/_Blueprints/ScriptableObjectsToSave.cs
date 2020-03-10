//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "ScriptableObjectCollection", menuName = "Scriptable Objects/Collections/ScriptableObjectsToSave", order = 0)]
public class ScriptableObjectsToSave : SerializedScriptableObject
{
    [Delayed, SerializeField] private string folder;
    [Delayed, SerializeField] private List<string> foldersToIgnore;
    [ReadOnly, SerializeField] public Dictionary<string, ScriptableObject> scriptableObjects = new Dictionary<string, ScriptableObject>();
    
    #region Singleton
    private static ScriptableObjectsToSave current;
    public static ScriptableObjectsToSave Current
        {
            get
            {
                if (current == null)
                {
                    ScriptableObjectsToSave instance = null;
                    #if UNITY_EDITOR
                    string[] instanceGuids = AssetDatabase.FindAssets("t: ScriptableObjectsToSave");
                    if (instanceGuids.Length > 1)
                    {
                        Debug.LogError("There is more than one ScriptableObjectsToSave in this project, but there must only be one.");
                        Debug.Log("Deleting other instances of ScriptableObjectsToSave and keeping only one instance.");
                        Debug.LogFormat("The selected instance is: {0}", AssetDatabase.GUIDToAssetPath(instanceGuids[0]));
                        instance = AssetDatabase.LoadAssetAtPath<ScriptableObjectsToSave>(AssetDatabase.GUIDToAssetPath(instanceGuids[0]));

                        // Delete other instances
                        for (int i = 1; i < instanceGuids.Length; i++)
                        {
                            string instanceGuid = instanceGuids[i];
                            AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(instanceGuid));
                        }
                    }
                    else if (instanceGuids.Length == 0)
                    {
                        Debug.Log("No Asset Reference Resolver instance found, creating a new one at 'Assets/Resources/Bayat/Core'.");
                        instance = ScriptableObject.CreateInstance<ScriptableObjectsToSave>();
                        System.IO.Directory.CreateDirectory("Assets/Resources/Bayat/Core");
                        AssetDatabase.CreateAsset(instance, "Assets/Resources/Bayat/Core/ScriptableObjectsToSave.asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                    else
                    {
                        instance = AssetDatabase.LoadAssetAtPath<ScriptableObjectsToSave>(AssetDatabase.GUIDToAssetPath(instanceGuids[0]));
                    }
                    #else
                    instance = Resources.Load<ScriptableObjectsToSave>("Bayat/Core/ScriptableObjectsToSave");
                    #endif
                    current = instance;
                }
                return current;
            }
        }
    #endregion
    
    #if UNITY_EDITOR
    [Button]
    private void RefreshDictionary()
    {
        string[] foldersToSearch = {folder};
        
        List<ScriptableObject> scriptableObjectsFoundInAssets = Wichtel.UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<ScriptableObject>(foldersToSearch);

        scriptableObjects.Clear();

        foreach(ScriptableObject so in scriptableObjectsFoundInAssets)
        {
            string assetPath = AssetDatabase.GetAssetPath(so);
            bool ignoreThisScriptableObject = false;
            foreach (var folderPath in foldersToIgnore) 
            {
                if(assetPath.Contains(folderPath)) ignoreThisScriptableObject = true;
            }
            if(ignoreThisScriptableObject) continue;

            if (!scriptableObjects.ContainsKey(so.name))
            {
                scriptableObjects.Add(so.name, so);
            }
            else
            {
                Debug.LogError($"Achtung, es gibt zwei oder mehr ScriptableObjects mit demselben Namen! ({so.name})\nPath: {assetPath}", this);
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