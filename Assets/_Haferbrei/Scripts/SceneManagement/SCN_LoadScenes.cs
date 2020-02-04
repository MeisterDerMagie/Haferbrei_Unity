//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Haferbrei {
public class SCN_LoadScenes : MonoBehaviour
{
    [SerializeField, BoxGroup("Scene Loader"), Required] private SO_LoadScenes sceneLoader;
    [SerializeField, BoxGroup("Optional loading screen"), AssetsOnly] private GameObject loadingScreen_Prefab;

    [Button]
    public void LoadScenes()
    {
        if (loadingScreen_Prefab != null) sceneLoader.loadingScreen = loadingScreen_Prefab;
        sceneLoader.LoadScenes();
    }
}
}