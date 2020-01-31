﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace DiscoPong{
public class INIT005_ResetAtomVariables : SerializedMonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] private bool resetOnSceneStart = true;
    [SerializeField, BoxGroup("Settings"), Required] private List<AtomBaseVariable> atomVariablesToReset = new List<AtomBaseVariable>();

    private void Awake()
    {
        if (resetOnSceneStart) ResetAtomVariables();
    }

    [Button, DisableInEditorMode]
    public void ResetAtomVariables()
    {
        foreach (var atomVariable in atomVariablesToReset)
        {
            atomVariable.Reset();
        }
    }
}
}