//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Wichtel;

namespace Haferbrei {
[CreateAssetMenu(fileName = "AutoAssignBuildingCostsRecipes", menuName = "Scriptable Objects/Utilities/Auto Assign Building Costs", order = 0)]
public class AutoAssignCostsRecipes : ScriptableObject
{
    [Button]
    public void AutoAssign()
    {
        var alleBuildings = UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<Buildings>();
        var alleRecipes = UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<RessourceRecipe>();

        var buildingsByName = new Dictionary<string, Buildings>();
        var recipesByName = new Dictionary<string, RessourceRecipe>();
        
        foreach (var building in alleBuildings)
        {
            var nameSplitBuilding = building.name.Split('_');
            buildingsByName.Add(nameSplitBuilding[0], building);
        }
        
        foreach (var recipe in alleRecipes)
        {
            var nameSplitBuilding = recipe.name.Split('_');
            recipesByName.Add(nameSplitBuilding[0], recipe);
        }

        foreach (var building in buildingsByName)
        {
            if (recipesByName.ContainsKey(building.Key))
            {
                building.Value.cost = recipesByName[building.Key];
                EditorUtility.SetDirty(building.Value);
                Debug.Log("Assigned " + building.Key);
            }
            else
            {
                Debug.LogWarning("Could not assign " + building.Key);
            }
        }
    }
}
}