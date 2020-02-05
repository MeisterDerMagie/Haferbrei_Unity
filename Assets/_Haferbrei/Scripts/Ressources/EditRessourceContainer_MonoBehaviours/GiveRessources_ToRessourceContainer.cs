//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class GiveRessources_ToRessourceContainer : SerializedMonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] public RessourceContainer givingContainer;
    [SerializeField, BoxGroup("Settings")] public RessourceContainer receivingContainer;
    [SerializeField, BoxGroup("Settings")] private Mode mode;
    [SerializeField, BoxGroup("Settings"), Required, ShowIf("modeIsSingleOrAllOfOne")] public Ressource ressource;
    [SerializeField, BoxGroup("Settings"), Required, ShowIf("modeIsSingle")] private int amount;
    [SerializeField, BoxGroup("Settings"), Required, ShowIf("modeIsMultiple")] public Dictionary<Ressource, int> ressources;
    [SerializeField, BoxGroup("Settings"), Required, ShowIf("modeIsRecipe")] private RessourceRecipe recipe;
    
    private enum Mode{ Single, Multiple, Recipe, AllOfOne, All }
    private bool modeIsSingle => (mode == Mode.Single); //for Odin
    private bool modeIsMultiple => (mode == Mode.Multiple); //for Odin
    private bool modeIsRecipe => (mode == Mode.Recipe); //for Odin
    private bool modeIsAllOfOne => (mode == Mode.AllOfOne); //for Odin
    private bool modeIsAll => (mode == Mode.All); //for Odin
    private bool modeIsSingleOrAllOfOne => (mode == Mode.Single || mode == Mode.AllOfOne);

    public void SetGivingContainer(RessourceContainer _ressourceContainer) => givingContainer = _ressourceContainer;
    public RessourceContainer GetGivingContainer() => givingContainer;
    
    public void SetReceivingContainer(RessourceContainer _ressourceContainer) => receivingContainer = _ressourceContainer;
    public RessourceContainer GetReceivingContainer() => receivingContainer;
    
    public void GiveRessources()
    {
        if (givingContainer == null || receivingContainer == null)
        {
            Debug.LogError("Achtung, Container ist null! Hier stimmt etwas nicht.");
            return;
        }
        
        switch (mode)
        {
            case Mode.Single:
                if(ressource != null) givingContainer.GiveRessource(ref receivingContainer, ressource, amount);
                break;
            case Mode.Multiple:
                if(ressources != null) givingContainer.GiveRessources(ref receivingContainer, ressources);
                break;
            case Mode.Recipe:
                if(recipe != null) givingContainer.GiveRessources(ref receivingContainer, recipe);
                break;
            case Mode.AllOfOne:
                if(ressource != null) givingContainer.GiveAllOfOneRessource(ref receivingContainer, ressource);
                break;
            case Mode.All:
                givingContainer.GiveAllRessources(ref receivingContainer);
                break;
        }
    }
}
}