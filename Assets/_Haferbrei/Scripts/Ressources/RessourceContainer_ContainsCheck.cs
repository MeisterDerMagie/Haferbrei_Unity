//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Haferbrei {
public class RessourceContainer_ContainsCheck : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] private RessourceContainer containerToCheck;
    [SerializeField, BoxGroup("Settings")] private Mode mode;
    [SerializeField, BoxGroup("Settings"), ShowIf("modeIsSingle")] private Ressource ressource;
    [SerializeField, BoxGroup("Settings"), ShowIf("modeIsSingle")] private int amount;
    [SerializeField, BoxGroup("Settings"), ShowIf("modeIsMultiple")] private Dictionary<Ressource, int> ressources;
    [SerializeField, BoxGroup("Settings"), ShowIf("modeIsRecipe")] private RessourceRecipe recipe;
    [SerializeField, BoxGroup("Settings"), ShowIf("modeIsModRecipe")] private ModRessourceRecipe modRecipe;

    [ReadOnly] public bool containerContainsRessources;
    private bool initialCheck = true;
    
    
    private enum Mode{ Single, Multiple, Recipe, ModRecipe }
    private bool modeIsSingle => (mode == Mode.Single); //for Odin
    private bool modeIsMultiple => (mode == Mode.Multiple); //for Odin
    private bool modeIsRecipe => (mode == Mode.Recipe); //for Odin
    private bool modeIsModRecipe => (mode == Mode.ModRecipe); //for Odin
    
    public UnityEvent onContainerContainsRessources;
    public UnityEvent onContainerDoesNotContainRessources;

    private void OnEnable()
    {
        SubscribeToChangedEvent();
        initialCheck = true;
        DoCheck();
    }

    private void OnDisable() => UnsubscribeFromChangedEvent();

    public void SetRessourceContainer(RessourceContainer _ressourceContainer)
    {
        UnsubscribeFromChangedEvent();
        containerToCheck = _ressourceContainer;
        SubscribeToChangedEvent();

        initialCheck = true;
        DoCheck();
    }

    public RessourceContainer GetRessourceContainer() => containerToCheck;

    public void SetRessourcesToCheckFor(Ressource _ressource, int _amount)
    {
        mode = Mode.Single;
        ressource = _ressource;
        amount = _amount;
        DoCheck();
    }

    public void SetRessourcesToCheckFor(Dictionary<Ressource, int> _ressources)
    {
        mode = Mode.Multiple;
        ressources = _ressources;
        DoCheck();
    }

    public void SetRessourcesToCheckFor(RessourceRecipe _recipe)
    {
        mode = Mode.Recipe;
        recipe = _recipe;
        DoCheck();
    }

    public void SetRessourcesToCheckFor(ModRessourceRecipe _recipe)
    {
        mode = Mode.ModRecipe;
        modRecipe = _recipe;
        DoCheck();
    }
    
    private void SubscribeToChangedEvent()
    {
        if(containerToCheck != null) containerToCheck.onRessourcesChanged += DoCheck;
    }

    private void UnsubscribeFromChangedEvent()
    {
        if(containerToCheck != null) containerToCheck.onRessourcesChanged -= DoCheck;
    }

    private void DoCheck()
    {
        bool containerContainsRessourcesNew = false;
        switch (mode)
        {
            case Mode.Single:
                if (ressource != null) containerContainsRessourcesNew = containerToCheck.ContainsRessource(ressource, amount);
                break;
            case Mode.Multiple:
                if(ressources != null) containerContainsRessourcesNew = containerToCheck.ContainsRessources(ressources);
                break;
            case Mode.Recipe:
                if(recipe != null) containerContainsRessourcesNew = containerToCheck.ContainsRessources(recipe);
                break;
            case Mode.ModRecipe:
                if (modRecipe != null) containerContainsRessourcesNew = containerToCheck.ContainsRessources(modRecipe);
                break;
        }

        if (containerContainsRessources == containerContainsRessourcesNew && !initialCheck) return;
        initialCheck = false;

        containerContainsRessources = containerContainsRessourcesNew;
        
        if(containerContainsRessources) onContainerContainsRessources.Invoke();
        else                            onContainerDoesNotContainRessources.Invoke();
    }
    }
}