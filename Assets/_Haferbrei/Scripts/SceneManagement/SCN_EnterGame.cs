using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace Haferbrei{
public class SCN_EnterGame : MonoBehaviour
{
    
    public SO_LoadScenes mainGameScenes;
    
    private void Start()
    {
        Timing.RunCoroutine(LoadScenesDelayed());
    }

    private IEnumerator<float> LoadScenesDelayed()
    {
        yield return Timing.WaitForOneFrame;
        
        mainGameScenes.LoadScenes();
    }
}
}