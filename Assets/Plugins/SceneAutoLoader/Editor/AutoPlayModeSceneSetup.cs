using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

//This script was modified from https://forum.unity.com/threads/executing-first-scene-in-build-settings-when-pressing-play-button-in-editor.157502/#post-4152451
[InitializeOnLoad]
public class AutoPlayModeSceneSetup
{
    [MenuItem("Tools/Set As First Scene", false, 0)]
    public static void SetAsFirstScene()
    {
        List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
        List<string> scenePaths = editorBuildSettingsScenes.Select(i => i.path).ToList();
 
        //Add the scene to build settings if not already there; place as the first scene
        if (!scenePaths.Contains(EditorSceneManager.GetActiveScene().path))
        {
            editorBuildSettingsScenes.Insert(0, new EditorBuildSettingsScene(EditorSceneManager.GetActiveScene().path, true));
        }
        else
        {
            //Reference the current scene
            EditorBuildSettingsScene scene = new EditorBuildSettingsScene(EditorSceneManager.GetActiveScene().path, true);
 
            int index = -1;
 
            //Loop and find index from scene; we are doing this way cause IndexOf returns a -1 index for some reason
            for(int i = 0; i < editorBuildSettingsScenes.Count; i++)
            {
                if(editorBuildSettingsScenes[i].path == scene.path)
                {
                    index = i;
                }
            }
 
            if (index != 0)
            {
                //Remove from current index
                editorBuildSettingsScenes.RemoveAt(index);
 
                //Then place as first scene in build settings
                editorBuildSettingsScenes.Insert(0, scene);
            }
        }
 
        //copy arrays back to build setting scenes
        EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
    }
 
    static AutoPlayModeSceneSetup()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
        EditorBuildSettings.sceneListChanged += SceneListChanged;
    }
    
    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
        {
            // User pressed play
            CacheOpenedScenes();
        }
        
        if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
        {
            // User pressed stop
        }
    }

    public static void LoadOpenedScenes()
    {
        //load cached scenes from playerPrefs
        string openedScenesAsString = PlayerPrefs.GetString("openedScenes");
        string[] openedScenesSplit = openedScenesAsString.Split('-');
        
        //convert from string to int (buildIndex)
        int[] openedScenesBuildIndex = new int[openedScenesSplit.Length];
        for (int i = 0; i < openedScenesSplit.Length; i++)
        {
            int.TryParse(openedScenesSplit[i], out int buildIndex);
            openedScenesBuildIndex[i] = buildIndex;
        }
        
        //don't load the initialization scene again
        List<int> openedScenesBuildIndexList = new List<int>();
        openedScenesBuildIndexList.AddRange(openedScenesBuildIndex);
        if (openedScenesBuildIndexList.Contains(0)) openedScenesBuildIndexList.Remove(0); //don't load the init scene, as it already is loaded

        //load scenes
        bool isFirstScene = true;
        foreach (int buildIndex in openedScenesBuildIndexList)
        {
            //Debug.Log("Load scene with build index " + buildIndex);
            SceneManager.LoadSceneAsync(buildIndex, isFirstScene ? LoadSceneMode.Single : LoadSceneMode.Additive);
            isFirstScene = false;
        }
    }

    private static void CacheOpenedScenes()
    {
        string openedScenesAsString = "";
        int countLoaded = SceneManager.sceneCount;
 
        for (int i = 0; i < countLoaded; i++)
        {
            var loadedScene = SceneManager.GetSceneAt(i);
            int buildIndex = loadedScene.buildIndex;
            if (i > 0) openedScenesAsString += "-";
            openedScenesAsString += buildIndex.ToString();
                
            //Debug.Log("Scene " + loadedScene.name + " is opened.");
        }
        
        PlayerPrefs.SetString("openedScenes", openedScenesAsString);
        //Debug.Log("Saved to playerPrefs: " + openedScenesAsString);
    }
 
    static void SceneListChanged()
    {
        // Ensure at least one build scene exist.
        if (EditorBuildSettings.scenes.Length == 0) return;
 
        //Reference the first scene
        SceneAsset theScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
 
        // Set Play Mode scene to first scene defined in build settings.
        EditorSceneManager.playModeStartScene = theScene;
    }
}