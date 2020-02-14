//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.Utils.ColorModels;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Haferbrei{
[CreateAssetMenu(fileName = "LoadScenes", menuName = "Scriptable Objects/Scene Management/Load Scenes", order = 0)]
public class SO_LoadScenes : ScriptableObject
{
    [SerializeField, BoxGroup("Settings")] private bool loadAdditively;
    [SerializeField, BoxGroup("Settings")] public GameObject loadingScreen;
    
    [HideIf("loadAdditively")]
    [SerializeField, BoxGroup("Scenes")] private SceneReference newScene;
    [SerializeField, BoxGroup("Scenes")] private List<SceneReference> scenesToLoadAdditively;

    //[SerializeField, BoxGroup("SaveGame (optional)")] private SaveLoadController optionalSaveGameToLoad;
    
    
    public void LoadScenes()
    {
        Timing.RunCoroutine(_LoadScenes());
    }

    private IEnumerator<float> _LoadScenes()
    {
        //Show loading screen
        GameObject loadingScreenInstance = null;
        if (loadingScreen != null)
        {
            loadingScreenInstance = Instantiate(loadingScreen);
            DontDestroyOnLoad(loadingScreenInstance);
        }
        
        //load single scene if not additively
        if (!loadAdditively)
        {
            Debug.Log("load Scene: " + newScene.ScenePath);
            yield return Timing.WaitUntilDone(SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Single));
        }
        
        //load additive scenes
        foreach (var scene in scenesToLoadAdditively)
        {
            Debug.Log("load Scene additively: " + scene.ScenePath);
            yield return Timing.WaitUntilDone(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));
        }
        
        //load save game data if there is any
        //if (optionalSaveGameToLoad != null && optionalSaveGameToLoad.loadSaveGame)
        //{
        //    yield return Timing.WaitUntilDone(Timing.RunCoroutine(optionalSaveGameToLoad._LoadGameState()));
        //}

        //after all scenes are loaded: search for all initializers and initialize all those who want to be initialized at the scene start
        InitializeScenes();

        //hide and clean up loading screen
        if(loadingScreenInstance != null) Destroy(loadingScreenInstance);
        loadingScreen = null;
    }

    private void InitializeScenes()
    {
        //get all root objects
        var allRootObjects = new List<GameObject>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            allRootObjects.AddRange(scene.GetRootGameObjects());
        }
        
        //get all initializers
        var allInitializers = new List<INIT001_Initialize>();
        foreach (var rootGameObject in allRootObjects)
        {
            var initializer = rootGameObject.GetComponent<INIT001_Initialize>();
            if(initializer != null) allInitializers.Add(initializer);
        }
        //initialize all initializers
        foreach (var initializer in allInitializers)
        {
            if(initializer.initOnSceneStart) initializer.StartInitialization();
        }
    }
}
}