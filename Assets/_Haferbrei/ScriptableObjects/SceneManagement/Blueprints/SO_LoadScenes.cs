//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.Utils.ColorModels;
using MEC;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Wichtel.Extensions;

namespace Haferbrei{
[CreateAssetMenu(fileName = "LoadScenes", menuName = "Scriptable Objects/Scene Management/Load Scenes", order = 0)]
public class SO_LoadScenes : ScriptableObject
{
    [SerializeField, BoxGroup("Settings")] private bool loadAdditively;
    [SerializeField, BoxGroup("Settings")] public GameObject loadingScreen;
    
    [HideIf("loadAdditively")]
    [SerializeField, BoxGroup("Scenes")] private SceneReference newScene;
    [SerializeField, BoxGroup("Scenes")] private List<SceneReference> scenesToLoadAdditively;

    [SerializeField, BoxGroup("SaveGame (optional)")] private SaveLoadController saveLoadController;
    
    
    public void LoadScenes()
    {
        Timing.RunCoroutine(_LoadScenes());
    }

    private IEnumerator<float> _LoadScenes()
    {
        //Show loading screen
        int debugLogCounter = 1; //Kann gelöscht werden, wenn die ganzen ---- 1. Show Loading Screen ---- Debug.Logs entfernt werden
        GameObject loadingScreenInstance = null;
        if (loadingScreen != null)
        {
            Debug.Log($"---- {(debugLogCounter++)}. Show Loading Screen ----");
            loadingScreenInstance = Instantiate(loadingScreen);
            DontDestroyOnLoad(loadingScreenInstance);
        }
        
        //load single scene if not additively
        Debug.Log($"---- {(debugLogCounter++)}. Load Scenes ----");
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
        if (saveLoadController != null && saveLoadController.loadSaveGame)
        {
            Debug.Log($"---- {(debugLogCounter++)}. Load SaveGameData ----");
            saveLoadController.loadSaveGame = false;
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(saveLoadController._LoadGameState()));
        }

        //after all scenes are loaded: search for all initializers and initialize all those who want to be initialized at the scene start
        Debug.Log($"---- {(debugLogCounter++)}. Initialize Scene ----");
        InitializeScenes();

        //hide and clean up loading screen
        Debug.Log($"---- {(debugLogCounter++)}. Hide LoadingScreen ----");
        if(loadingScreenInstance != null) Destroy(loadingScreenInstance);
        loadingScreen = null;
    }

    private void InitializeScenes()
    {
        //get all initializers
        var allInitializers = new List<INIT001_Initialize>(ComponentExtensions.FindAllComponentsOfType<INIT001_Initialize>());
        
        //initialize all initializers
        foreach (var initializer in allInitializers)
        {
            if(initializer.initOnSceneStart) initializer.StartInitialization();
        }
    }
}
}