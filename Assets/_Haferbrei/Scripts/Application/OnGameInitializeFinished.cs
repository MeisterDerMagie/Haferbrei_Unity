using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace Haferbrei{
public class OnGameInitializeFinished : MonoBehaviour
{
    [SerializeField, BoxGroup("Scene Loader"), Required] private SO_LoadScenes nextScenesToLoadInBuild;
    
    public void OnInitializeFinished()
    {
        Debug.Log("Successfully initialized game.");
        Timing.RunCoroutine(LoadNextScenes());
    }

    private IEnumerator<float> LoadNextScenes()
    {
        yield return Timing.WaitForOneFrame; //einen Frame warten, damit die Start und Awake Funktionen aller anderen GameObjekte aufgerufen werden
        //Debug.Log("LoadNextScenes");
        nextScenesToLoadInBuild.LoadScenes();
    }
}
}