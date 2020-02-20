//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Sirenix.Serialization;
using UnityEngine;

namespace Haferbrei {
public class ModifiableRecipeExample : MonoBehaviour, IFloatModifierSource
{
    public int X;
    public bool allBuildingsCostXWoodLess;
    public ModRecipeCollection allBuildingCosts;
    public Ressource wood;

    public string sourceName;
    public string SourceName { get => sourceName; set => sourceName = value; }

    public void Toggle(bool _onOrOff)
    {
        if(_onOrOff) Activate();
        else Deactivate();
    }
    
    private void Activate()
    {
        foreach (var buildingCost in allBuildingCosts.collection)
        {
            var newModifier = new FloatModifier(-X, StatModType.Flat, this);
            if(buildingCost.Value.recipe.ContainsKey(wood)) buildingCost.Value.recipe[wood].AddModifier(newModifier);
        }
    }

    private void Deactivate()
    {
        foreach (var buildingCost in allBuildingCosts.collection)
        {
            buildingCost.Value.recipe[wood].RemoveAllModifiersFromSource(this);
        }
    }

    
}
}