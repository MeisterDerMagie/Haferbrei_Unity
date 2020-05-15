using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel.Extensions;

namespace Haferbrei{
public class IngameTimer_RunningTimers : MonoBehaviour, IInitSelf
{
    [Saveable][ShowInInspector] private static List<IngameTimer> runningTimers = new List<IngameTimer>();

    //start timers after loading a savegame
    public void InitSelf()
    {
        runningTimers.RemoveEmptyEntries();
        for (int i = 0; i < runningTimers.Count; i++)
        {
            runningTimers[i].Start();
        }
    }
    
    public static void RegisterTimer(IngameTimer _timer)
    {
        if(!runningTimers.Contains(_timer)) runningTimers.Add(_timer);
    }

    public static void UnregisterTimer(IngameTimer _timer)
    {
        if (runningTimers.Contains(_timer)) runningTimers.Remove(_timer);
    }
}
}