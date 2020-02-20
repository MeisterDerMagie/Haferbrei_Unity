using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace Haferbrei{
public class INIT005_ResetAllScriptableObjects : SerializedMonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] private bool resetOnSceneStart = true;

    [SerializeField, BoxGroup("Settings"), AssetsOnly] private ResettableScriptableObjects allResettableScriptableObjectsCollection;

    private void Awake()
    {
        if (resetOnSceneStart) ResetScriptableObjects();
    }

    [Button, DisableInEditorMode]
    public void ResetScriptableObjects()
    {
        allResettableScriptableObjectsCollection.ResetAll();
    }
}
}