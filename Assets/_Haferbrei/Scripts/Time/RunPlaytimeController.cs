using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei{
public class RunPlaytimeController : MonoBehaviour
{
    [Saveable] public static float runPlaytime
    {
        get => runPlaytimeBeforeLevelLoad + Time.timeSinceLevelLoad;
        set => runPlaytimeBeforeLevelLoad = value;
    }

    private static float runPlaytimeBeforeLevelLoad;
}
}