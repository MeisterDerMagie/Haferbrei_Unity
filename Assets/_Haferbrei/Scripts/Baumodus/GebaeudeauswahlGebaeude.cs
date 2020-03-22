//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Haferbrei {
public class GebaeudeauswahlGebaeude : MonoBehaviour
{
    public Building Building => building;
    [SerializeField, BoxGroup("Building")] private Building building;

    [SerializeField, BoxGroup("References"), Required] private HasTooltip tooltip;
    [SerializeField, BoxGroup("References"), Required] private Image icon;
    
    
    public void SetBuilding(Building _newBuilding)
    {
        building = _newBuilding;

        tooltip.tooltipIcon  = building.icon;
        tooltip.tooltipTitle = building.buildingName;
        tooltip.bodyElements.Clear();
        var tooltipBodyElement = new TooltipBodyElement
        {
            elementType = TooltipBodyElement.ElementType.ModRecipe,
            modRecipe = building.cost
        };
        tooltip.bodyElements.Add(tooltipBodyElement);

        icon.sprite = building.icon;
    }
    
    #if UNITY_EDITOR
    private void OnValidate() => SetBuilding(building);
    #endif
}
}