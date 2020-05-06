//(c) copyright by Martin M. Klöckener
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
    [ShowInInspector, ReadOnly] private static float targetTimeScale = -1f;
    
    [ShowInInspector, ReadOnly] public static bool GameIsPaused;

    private IEnumerable<IPauseable> allPauseables;
    
    [Button, DisableInEditorMode]
    public void SetTimeScale(float _newValue) //Does not unpause the game!
    {
        if (GameIsPaused)
        {
            targetTimeScale = _newValue;
            return;
        }
        
        TimeScale = _newValue;
    }

    [Button, DisableInEditorMode]
    public void SetTimeScaleAndUnpause(float _newValue)
    {
        targetTimeScale = _newValue;
        UnpauseGame();
    }
    
    [Button, DisableInEditorMode]
    public void PauseGame()
    {
        GameIsPaused = true;
        targetTimeScale = TimeScale; //cache vorherige TimeScale
        TimeScale = 0f;
        FindAllIPauseables();
        foreach (var iPauseable in allPauseables) { iPauseable.OnPause(); }
    }

    [Button, DisableInEditorMode]
    public void UnpauseGame()
    {
        GameIsPaused = false;
        TimeScale = targetTimeScale; //gecachte oder zwischenzeitlich geänderte TimeScale wieder setzen
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