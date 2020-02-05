//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class EmptyRessources_FromRessourceContainer : SerializedMonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] private RessourceContainer containerToEdit;
    [SerializeField, BoxGroup("Settings")] private Mode mode;
    [SerializeField, BoxGroup("Settings"), Required, ShowIf("modeIsSingle")] private Ressource ressource;

    private enum Mode{ Single, All }
    private bool modeIsSingle => (mode == Mode.Single); //for Odin
    private bool modeIsAll => (mode == Mode.All); //for Odin

    public void SetRessourceContainer(RessourceContainer _ressourceContainer) => containerToEdit = _ressourceContainer;
    public RessourceContainer GetRessourceContainer() => containerToEdit;
    
    public void EmptyRessource()
    {
        if (containerToEdit == null)
        {
            Debug.LogError("Achtung, Container ist null! Hier stimmt etwas nicht.");
            return;
        }
        
        switch (mode)
        {
            case Mode.Single:
                if(ressource != null) containerToEdit.EmptyRessource(ressource);
                break;
            case Mode.All:
                containerToEdit.EmptyContainer();
                break;
        }
    }
}
}