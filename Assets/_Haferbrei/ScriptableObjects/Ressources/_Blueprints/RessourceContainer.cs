using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Haferbrei{
[CreateAssetMenu(fileName = "RessourcePackage", menuName = "Scriptable Objects/Ressourcen/Ressource Container", order = 0)]
public class RessourceContainer : SaveableScriptableObject
{
    [OdinSerialize] private Dictionary<Ressource, int> ressources = new Dictionary<Ressource, int>();

    [ReadOnly] public Action onRessourcesChanged = delegate { };
    
    //temporary lists & dictionaries that are declared here to avoid GC
    private List<Ressource> entriesToRemove = new List<Ressource>(); //this is used in the method RemoveEmptyRessourceEntries
    private Dictionary<Ressource, int> difference = new Dictionary<Ressource, int>();
    
    
    //-- Add Ressources to the container --
    public void AddRessource(Ressource _ressource, int _amount, bool _callOnChangedEvent = true)
    {
        if (!ressources.ContainsKey(_ressource))  ressources.Add(_ressource, _amount);
        else                                      ressources[_ressource] += _amount;
        //---
        RemoveEmptyRessourceEntries();
        if(_callOnChangedEvent) onRessourcesChanged();
    }

    public void AddRessources(Dictionary<Ressource, int> _ressources, bool _callOnChangedEvent = true)
    {
        foreach (var ressource in _ressources)
        {
            AddRessource(ressource.Key, ressource.Value, false);
        }
        //---
        RemoveEmptyRessourceEntries();
        if(_callOnChangedEvent) onRessourcesChanged();
    }

    public void AddRessources(RessourceRecipe _recipe) => AddRessources(_recipe.recipe);
    //-- --

    //-- Subtract Ressources from the container --
    public void SubtractRessource(Ressource _ressource, int _amount, bool _callOnChangedEvent = true)
    {
        if (!ressources.ContainsKey(_ressource))  ressources.Add(_ressource, -_amount);
        else                                      ressources[_ressource] -= _amount;
        //---
        RemoveEmptyRessourceEntries();
        if(_callOnChangedEvent) onRessourcesChanged();
    }
    
    public void SubtractRessources(Dictionary<Ressource, int> _ressources, bool _callOnChangedEvent = true)
    {
        foreach (var ressource in _ressources)
        {
            SubtractRessource(ressource.Key, ressource.Value, false);
        }
        //---
        RemoveEmptyRessourceEntries();
        if(_callOnChangedEvent) onRessourcesChanged();
    }

    public void SubtractRessources(RessourceRecipe _recipe) => SubtractRessources(_recipe.recipe);
    //-- --

    //-- Transfer Ressources from this container to another --
    public void GiveRessource(ref RessourceContainer _receiver, Ressource _ressourceToGive, int _amount, bool _callOnChangedEvent = true)
    {
        _receiver.AddRessource(_ressourceToGive, _amount);
        SubtractRessource(_ressourceToGive, _amount, false);
        //---
        RemoveEmptyRessourceEntries();
        if(_callOnChangedEvent) onRessourcesChanged();
    }

    public void GiveAllOfOneRessource(ref RessourceContainer _receiver, Ressource _ressourceToGive, bool _callOnChangedEvent = true)
    {
        int amount = GetRessourceAmount(_ressourceToGive);
        if(amount != 0) _receiver.AddRessource(_ressourceToGive, amount);
        EmptyRessource(_ressourceToGive, false);
        //---
        RemoveEmptyRessourceEntries();
        if(_callOnChangedEvent) onRessourcesChanged();
    }
    
    public void GiveRessources(ref RessourceContainer _receiver, Dictionary<Ressource, int> _ressourcesToGive, bool _callOnChangedEvent = true)
    {
        _receiver.AddRessources(_ressourcesToGive);
        SubtractRessources(_ressourcesToGive, false);
        //---
        RemoveEmptyRessourceEntries();
        if(_callOnChangedEvent) onRessourcesChanged();
    }

    public void GiveRessources(ref RessourceContainer _receiver, RessourceRecipe _recipe) => GiveRessources(ref _receiver, _recipe.recipe);

    public void GiveAllRessources(ref RessourceContainer _receiver, bool _callOnChangedEvent = true)
    {
        _receiver.AddRessources(ressources);
        EmptyContainer(false);
        //---
        if(_callOnChangedEvent) onRessourcesChanged();
    }
    //-- --
    
    //-- Check if ressources are in the container
    public bool ContainsRessource(Ressource _ressource, int _amount)
    {
        return (ressources.ContainsKey(_ressource) && ressources[_ressource] >= _amount);
    }

    public bool ContainsRessources(Dictionary<Ressource, int> _ressources)
    {
        foreach (var ressource in _ressources)
        {
            if (!ContainsRessource(ressource.Key, ressource.Value)) return false;
        }

        return true;
    }

    public bool ContainsRessources(RessourceRecipe _recipe) => ContainsRessources(_recipe.recipe); 
    //-- --
    
    //-- Get ressource difference (wenn kleiner als 0, bedeutet das, dass dieses Paket nicht so viele Ressourcen hat) --
    public int GetRessourceDifference(Ressource _ressource, int _amount)
    {
        if(!ressources.ContainsKey(_ressource)) return (0 - _amount);
        return ressources[_ressource] - _amount;
    }

    public Dictionary<Ressource, int> GetRessourcesDifference(Dictionary<Ressource, int> _ressources)
    {
        difference.Clear();

        foreach (var ressource in _ressources)
        {
            int ressourceDifference = GetRessourceDifference(ressource.Key, ressource.Value);
            difference.Add(ressource.Key, ressourceDifference);
        }

        return difference;
    }

    public Dictionary<Ressource, int> GetRessourcesDifference(RessourceRecipe _recipe) => GetRessourcesDifference(_recipe.recipe);
    //-- --

    //-- Get infos about the ressources Dictionary --
    public Dictionary<Ressource, int> GetRessources()
    {
        return ressources;
    }

    public int GetRessourceAmount(Ressource _ressource)
    {
        return (ressources.ContainsKey(_ressource)) ? ressources[_ressource] : 0;
    }
    //-- --
    
    //-- Set the content of the container, ignoring/resetting all currently stored ressources --
    public void SetRessources(Dictionary<Ressource, int> _ressources, bool _callOnChangedEvent = true)
    {
        EmptyContainer(false);
        foreach (var ressource in _ressources)
        {
            AddRessource(ressource.Key, ressource.Value, false);
        }
        if(_callOnChangedEvent) onRessourcesChanged();
    }

    public void SetRessources(RessourceRecipe _recipe) => SetRessources(_recipe.recipe);
    //-- --
    
    //-- Empty the whole container or a specific ressource --
    public void EmptyRessource(Ressource _ressource, bool _callOnChangedEvent = true)
    {
        if (ressources.ContainsKey(_ressource)) ressources.Remove(_ressource);
        //---
        if(_callOnChangedEvent) onRessourcesChanged();
    }
    
    public void EmptyContainer(bool _callOnChangedEvent = true)
    {
        ressources.Clear();
        //---
        if(_callOnChangedEvent) onRessourcesChanged();
    }
    //-- --

    private void RemoveEmptyRessourceEntries()
    {
        entriesToRemove.Clear();
            
        foreach (var ressource in ressources)
        {
            if(ressource.Value == 0) entriesToRemove.Add(ressource.Key);
        }

        foreach (var ressource in entriesToRemove)
        {
            ressources.Remove(ressource);
        }
    }
}
}