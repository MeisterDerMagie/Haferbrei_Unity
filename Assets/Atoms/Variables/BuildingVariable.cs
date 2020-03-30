using UnityEngine;
using System;
using Haferbrei;


namespace UnityAtoms
{

    /// <summary>
    /// Variable of type `Building`. Inherits from `AtomVariable&lt;Building, BuildingPair, BuildingEvent, BuildingPairEvent, BuildingBuildingFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Building", fileName = "BuildingVariable")]
    public sealed class BuildingVariable : AtomVariable<Building, BuildingPair, BuildingEvent, BuildingPairEvent, BuildingBuildingFunction>
    {
        protected override bool ValueEquals(Building other)
        {
            return Value == other;
        }
    }
}
