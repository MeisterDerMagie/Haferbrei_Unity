﻿//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class AddRessources_ToRessourceContainer : SerializedMonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] public RessourceContainer containerToEdit;
    [SerializeField, BoxGroup("Settings")] private Mode mode;
    [SerializeField, BoxGroup("Settings"), Required, ShowIf("modeIsSingle")] public Ressource ressource;
    [SerializeField, BoxGroup("Settings"), Required, ShowIf("modeIsSingle")] private int amount;
    [SerializeField, BoxGroup("Settings"), Required, ShowIf("modeIsMultiple")] public Dictionary<Ressource, int> ressources;
    [SerializeField, BoxGroup("Settings"), Required, ShowIf("modeIsRecipe")] private RessourceRecipe recipe;

    private enum Mode{ Single, Multiple, Recipe }
    private bool modeIsSingle => (mode == Mode.Single); //for Odin
    private bool modeIsMultiple => (mode == Mode.Multiple); //for Odin
    private bool modeIsRecipe => (mode == Mode.Recipe); //for Odin
    
    public void SetRessourceContainer(RessourceContainer _ressourceContainer) => containerToEdit = _ressourceContainer;
    public RessourceContainer GetRessourceContainer() => containerToEdit;
    
    public void AddRessource()
    {
        if (containerToEdit == null)
        {
            Debug.LogError("Achtung, Container ist null! Hier stimmt etwas nicht.");
            return;
        }
        
        switch (mode)
        {
            case Mode.Single:
                if(ressource != null) containerToEdit.AddRessource(ressource, amount);
                break;
            case Mode.Multiple:
                if(ressources != null) containerToEdit.AddRessources(ressources);
                break;
            case Mode.Recipe:
                if(recipe != null) containerToEdit.AddRessources(recipe);
                break;
        }
    }
}
}