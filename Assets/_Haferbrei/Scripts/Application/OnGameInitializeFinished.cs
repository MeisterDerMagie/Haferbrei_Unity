using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace DiscoPong{
public class OnGameInitializeFinished : MonoBehaviour
{
    [SerializeField, BoxGroup("Scene Loader"), Required] private SO_LoadScenes nextScenesToLoadInBuild;
    [SerializeField, BoxGroup("Initialize state"), Required] private BoolVariable gameIsInitialized; 
    
    
    public void OnInitializeFinished()
    {
        #if UNITY_EDITOR
        //if (!gameIsInitialized)
        //{
            //gameIsInitialized.Value = true;
            Debug.Log("Successfully initialized game.");
            Timing.RunCoroutine(LoadLastOpenedScenesInEditor());
            return;
        //}
        #endif
        Debug.Log("Successfully initialized game.");
        Timing.RunCoroutine(LoadNextScenes());
    }

    private IEnumerator<float> LoadNextScenes()
    {
        yield return Timing.WaitForOneFrame; //einen Frame warten, damit die Start und Awake Funktionen aller anderen GameObjekte aufgerufen werden
        //Debug.Log("LoadNextScenes");
        nextScenesToLoadInBuild.LoadScenes();
    }

    #if UNITY_EDITOR
    private IEnumerator<float> LoadLastOpenedScenesInEditor()
    {
        yield return Timing.WaitForOneFrame; //einen Frame warten, damit die Start und Awake Funktionen aller anderen GameObjekte aufgerufen werden
        //Debug.Log("LoadLastOpenedScenes");
        AutoPlayModeSceneSetup.LoadOpenedScenes();
    }
    #endif
}
}