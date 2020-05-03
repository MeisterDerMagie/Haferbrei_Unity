using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Haferbrei{
public class Playtime : SerializedMonoBehaviour
{
    [ShowInInspector, LabelText("Total Playtime")] private string totalPlaytimeInspector => totalPlaytime.ToString(@"hh\:mm\:ss");
    [ShowInInspector, LabelText("Session Playtime")] private string sessionPlaytimeInspector => sessionPlaytime.ToString(@"hh\:mm\:ss");
    [ShowInInspector, LabelText("Run Playtime")] private string runPlaytimeInspector => runPlaytime.ToString(@"hh\:mm\:ss");

    public static TimeSpan totalPlaytime => totalPlaytimeBeforeSession.Add(sessionPlaytime); //die gesamte Spielzeit, die der Spieler in Haferbrei verbracht hat
    public static TimeSpan sessionPlaytime => TimeSpan.FromSeconds(Time.realtimeSinceStartup); //die Spielzeit, seitdem er das Spiel gestartet hat
    public static TimeSpan runPlaytime => TimeSpan.FromSeconds(RunPlaytimeController.runPlaytime); //die Spielzeit dieses Spieldurchlaufs

    private static TimeSpan totalPlaytimeBeforeSession;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        float totalPlaytimeBeforeSessionInSeconds = PlayerPrefs.GetFloat("totalPlaytime");
        totalPlaytimeBeforeSession = TimeSpan.FromSeconds(totalPlaytimeBeforeSessionInSeconds);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("totalPlaytime", (float)totalPlaytime.TotalSeconds);
    }
}
}