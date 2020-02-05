using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Haferbrei{
[CreateAssetMenu(fileName = "RessourcePackage", menuName = "Scriptable Objects/Ressourcen/Ressource Container", order = 0)]
public class RessourceContainer : SerializedScriptableObject
{
    [OdinSerialize] private Dictionary<Ressource, int> ressources = new Dictionary<Ressource, int>();
    
    //temporary lists & dictionaries that are declared here to avoid GC
    private List<Ressource> entriesToRemove = new List<Ressource>(); //this is used in the method RemoveEmptyRessourceEntries
    private Dictionary<Ressource, int> difference = new Dictionary<Ressource, int>();
    
    
    //-- Add Ressources to the container --
    public void AddRessource(Ressource _ressource, int _amount)
    {
        if (!ressources.ContainsKey(_ressource))  ressources.Add(_ressource, _amount);
        else                                      ressources[_ressource] += _amount;
    }

    public void AddRessources(Dictionary<Ressource, int> _ressources)
    {
        foreach (var ressource in _ressources)
        {
            AddRessource(ressource.Key, ressource.Value);
        }
    }
    //-- --

    //-- Subtract Ressources from the container --
    public void SubtractRessource(Ressource _ressource, int _amount)
    {
        if (!ressources.ContainsKey(_ressource))  ressources.Add(_ressource, -_amount);
        else                                      ressources[_ressource] -= _amount;
    }
    
    public void SubtractRessources(Dictionary<Ressource, int> _ressources)
    {
        foreach (var ressource in _ressources)
        {
            SubtractRessource(ressource.Key, ressource.Value);
        }
    }
    //-- --

    //-- Transfer Ressources from this container to another --
    public void GiveRessource(ref RessourceContainer _receiver, Ressource _ressourceToGive, int _amount)
    {
        _receiver.AddRessource(_ressourceToGive, _amount);
        SubtractRessource(_ressourceToGive, _amount);
    }

    public void GiveAllOfOneRessource(ref RessourceContainer _receiver, Ressource _ressourceToGive)
    {
        int amount = GetRessourceAmount(_ressourceToGive);
        if(amount != 0) _receiver.AddRessource(_ressourceToGive, amount);
        EmptyRessource(_ressourceToGive);
    }
    
    public void GiveRessources(ref RessourceContainer _receiver, Dictionary<Ressource, int> _ressourcesToGive)
    {
        _receiver.AddRessources(_ressourcesToGive);
        SubtractRessources(_ressourcesToGive);
    }

    public void GiveAllRessources(ref RessourceContainer _receiver)
    {
        _receiver.AddRessources(ressources);
        EmptyContainer();
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
    
    
    //-- Empty the whole container or a specific ressource --
    public void EmptyRessource(Ressource _ressource)
    {
        if (ressources.ContainsKey(_ressource)) ressources.Remove(_ressource);
    }
    
    public void EmptyContainer()
    {
        ressources.Clear();
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