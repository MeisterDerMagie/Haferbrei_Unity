﻿//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using DiscoPong;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace DiscoPong {
[CreateAssetMenu(fileName = "ResetCollection", menuName = "Scriptable Objects/AtomVariables Reset Collection", order = 0)]
public class SO_AtomVariablesResetCollection : ScriptableObject, IResettable
{
    [SerializeField, BoxGroup("Settings"), Required] private List<AtomBaseVariable> atomVariablesToReset = new List<AtomBaseVariable>();
    
    public void ResetSelf()
    {
        foreach (var atomVariable in atomVariablesToReset)
        {
            if(atomVariable != null) atomVariable.Reset();
        }
    }
}
}