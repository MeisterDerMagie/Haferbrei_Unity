﻿//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace Haferbrei {
public class ToggleBaumodus_Controller : MonoBehaviour
{
    [SerializeField, BoxGroup("Info"), ReadOnly] private bool baumodusIsActive;
    [SerializeField, FoldoutGroup("References"), Required] private GameObject baumodusWindow;
    [SerializeField, FoldoutGroup("References"), Required] private Buildings noGebaeude;
    [SerializeField, FoldoutGroup("References"), Required] private Bauauswahl bauauswahl;
    [SerializeField, BoxGroup("Atom Values"), Required] private BuildingsVariable zuBauendesGebaeude;

    public UnityEvent onEnterBaumodus;
    public UnityEvent onLeaveBaumodus;

    public void ToggleBaumodus(bool _enter)
    {
        if(_enter) EnterBaumodus();
        else       LeaveBaumodus();
    }
    
    private void EnterBaumodus()
    {
        if (baumodusIsActive) return;
        baumodusIsActive = true;
        
        onEnterBaumodus.Invoke();
    }

    private void LeaveBaumodus()
    {
        if (!baumodusIsActive) return;
        baumodusIsActive = false;
        
        onLeaveBaumodus.Invoke();
    }
}
}