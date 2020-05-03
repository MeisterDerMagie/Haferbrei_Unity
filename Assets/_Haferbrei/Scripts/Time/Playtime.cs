using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Haferbrei{
public class Playtime : SerializedMonoBehaviour
{
    [ShowInInspector, LabelText("Total Playtime")] private string TotalPlaytimeInspector => Total.ToString(@"hh\:mm\:ss");
    [ShowInInspector, LabelText("Session Playtime")] private string SessionPlaytimeInspector => Session.ToString(@"hh\:mm\:ss");
    [ShowInInspector, LabelText("Run Playtime")] private string RunPlaytimeInspector => Run.ToString(@"hh\:mm\:ss");

    public static TimeSpan Total => totalBeforeSession.Add(Session); //die gesamte Spielzeit, die der Spieler in Haferbrei verbracht hat
    public static TimeSpan Session => TimeSpan.FromSeconds(Time.realtimeSinceStartup); //die Spielzeit, seitdem er das Spiel gestartet hat
    public static TimeSpan Run => TimeSpan.FromSeconds(RunPlaytimeController.runPlaytime); //die Spielzeit dieses Spieldurchlaufs

    private static TimeSpan totalBeforeSession;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        float totalPlaytimeBeforeSessionInSeconds = PlayerPrefs.GetFloat("totalPlaytime");
        totalBeforeSession = TimeSpan.FromSeconds(totalPlaytimeBeforeSessionInSeconds);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("totalPlaytime", (float)Total.TotalSeconds);
    }
}
}