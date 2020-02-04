using System;
using System.Collections.Generic;

namespace Haferbrei{
//[Serializable]
public class RessourcePackage
{
    private Dictionary<Ressource, int> ressources = new Dictionary<Ressource, int>();

    //-- Constructor --
    public RessourcePackage()
    {
        ressources = new Dictionary<Ressource, int>();
    }
    public RessourcePackage(RessourcePackage ressourcePackage)
    {
        ressources = new Dictionary<Ressource, int>(ressourcePackage.ressources);
    }
    //-- --
    
    //-- Add Ressources to the package --
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

    //-- Subtract Ressources from the package --
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

    //-- Transfer Ressources from this package to another --
    public void GiveRessource(ref RessourcePackage _receiver, Ressource _ressourceToCut, int _amount)
    {
        SubtractRessource(_ressourceToCut, _amount);
        _receiver.AddRessource(_ressourceToCut, _amount);
    }
    
    public void GiveRessources(ref RessourcePackage _receiver, Dictionary<Ressource, int> _ressourcesToCut)
    {
        SubtractRessources(_ressourcesToCut);
        _receiver.AddRessources(_ressourcesToCut);
    }

    public void GiveAllRessources(ref RessourcePackage _receiver)
    {
        _receiver.AddRessources(ressources);
        EmptyPackage();
    }
    //-- --
    
    //-- Check if ressources are in the package
    public bool ContainsRessource(Ressource _ressource, int _amount)
    {
        return (ressources.ContainsKey(_ressource) && ressources[_ressource] >= _amount);
    }

    public bool ContainsRessources(Dictionary<Ressource, int> _ressources)
    {
        foreach (var ressource in _ressources)
        {
            if(!ressources.ContainsKey(ressource.Key)) return false;
            if (ressources[ressource.Key] < ressource.Value) return false;
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
        var difference = new Dictionary<Ressource, int>();

        foreach (var ressource in _ressources)
        {
            int ressourceDifference;
            if (ressources.ContainsKey(ressource.Key))
                ressourceDifference = ressources[ressource.Key] - ressource.Value;
            else ressourceDifference = 0 - ressource.Value;
            
            difference.Add(ressource.Key, ressourceDifference);
        }

        return difference;
    }
    //-- --

    //-- Get the ressources Dictionary --
    public Dictionary<Ressource, int> GetRessources()
    {
        return ressources;
    }
    //-- --
    
    public void EmptyPackage()
    {
        ressources.Clear();
    }
}
}