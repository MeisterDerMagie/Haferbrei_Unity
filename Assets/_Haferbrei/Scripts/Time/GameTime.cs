﻿//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _Haferbrei.Scripts.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Haferbrei {
public class GameTime : SerializedMonoBehaviour
{
    [ShowInInspector] public static float TimeScale = 1f;
    [ShowInInspector, ReadOnly] private static bool gameIsPaused;

    private IEnumerable<IPauseable> allPauseables;
    
    [Button, DisableInEditorMode]
    public void PauseGame()
    {
        gameIsPaused = true;
        FindAllIPauseables();
        foreach (var iPauseable in allPauseables) { iPauseable.OnPause(); }
    }

    [Button, DisableInEditorMode]
    public void UnpauseGame()
    {
        gameIsPaused = false;
        foreach (var iPauseable in allPauseables) { iPauseable.OnUnpause(); }
    }

    private void FindAllIPauseables()
    {
        allPauseables = FindObjectsOfType<MonoBehaviour>().OfType<IPauseable>();
    }
}

public interface IPauseable
{
    void OnPause();
    void OnUnpause();
}
}