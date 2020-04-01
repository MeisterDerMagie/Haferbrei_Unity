using System;
using System.Collections.Generic;
using Haferbrei;
using UnityEditor;
using UnityEngine;

namespace _Haferbrei.Scripts.Editor{
[InitializeOnLoad]
public static class PlayStateNotifier
{
    public static Action onExitPlaymode = delegate {};
    
    static PlayStateNotifier()
    {
        EditorApplication.playmodeStateChanged += ModeChanged;
    }
 
    static void ModeChanged()
    {
        if (!EditorApplication.isPlayingOrWillChangePlaymode && EditorApplication.isPlaying)
        {
            onExitPlaymode?.Invoke();
            NotifyScriptableObjects();
        }
    }

    //Alle ScriptableObjects, die das IOnExitPlaymode interface implementieren, bekommen die Nachricht, dass der Playmode verlassen wird
    private static void NotifyScriptableObjects()
    {
        string[] foldersToSearch = {"Assets/_Haferbrei"};
        List<ScriptableObject> scriptableObjectsFoundInAssets = Wichtel.UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<ScriptableObject>(foldersToSearch);
        
        foreach(ScriptableObject so in scriptableObjectsFoundInAssets)
        {
            (so as IOnExitPlaymode)?.OnExitPlaymode();
        }
    }
}
}