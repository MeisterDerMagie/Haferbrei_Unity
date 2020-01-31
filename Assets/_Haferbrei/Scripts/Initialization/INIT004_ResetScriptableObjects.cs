using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace DiscoPong{
public class INIT004_ResetScriptableObjects : SerializedMonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] private bool resetOnSceneStart = true;
    [SerializeField, BoxGroup("Settings"), AssetsOnly] private List<IResettable> scriptableObjectsToReset = new List<IResettable>();

    private void Awake()
    {
        if (resetOnSceneStart) ResetScriptableObjects();
    }

    [Button, DisableInEditorMode]
    public void ResetScriptableObjects()
    {
        foreach (var resettable in scriptableObjectsToReset)
        {
            resettable.ResetSelf();
        }
    }
}
}