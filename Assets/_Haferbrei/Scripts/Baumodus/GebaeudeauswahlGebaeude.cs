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
    public BuildingType BuildingType => buildingType;
    [SerializeField, BoxGroup("Building")] private BuildingType buildingType;

    [SerializeField, BoxGroup("References"), Required] private HasTooltip tooltip;
    [SerializeField, BoxGroup("References"), Required] private Image icon;
    
    
    public void SetBuilding(BuildingType _newBuildingType)
    {
        buildingType = _newBuildingType;

        tooltip.tooltipIcon  = buildingType.icon;
        tooltip.tooltipTitle = buildingType.buildingName;
        tooltip.bodyElements.Clear();
        var tooltipBodyElement = new TooltipBodyElement
        {
            elementType = TooltipBodyElement.ElementType.ModRecipe,
            modRecipe = buildingType.cost
        };
        tooltip.bodyElements.Add(tooltipBodyElement);

        icon.sprite = buildingType.icon;
    }
    
    #if UNITY_EDITOR
    private void OnValidate() => SetBuilding(buildingType);
    #endif
}
}