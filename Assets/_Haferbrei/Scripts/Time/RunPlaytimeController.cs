using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei{
public class RunPlaytimeController : MonoBehaviour, IInitSelf
{
    [Saveable] public static float runPlaytime
    {
        get => runPlaytimeBeforeLevelLoad + (float)timeSinceSceneLoad.TotalSeconds;
        set => runPlaytimeBeforeLevelLoad = value;
    }

    private static float runPlaytimeBeforeLevelLoad;
    private static TimeSpan timeSinceSceneLoad => DateTime.Now.Subtract(timeAtSceneLoad);
    private static DateTime timeAtSceneLoad;

    public void InitSelf()
    {
        timeAtSceneLoad = DateTime.Now;
    }
}
}