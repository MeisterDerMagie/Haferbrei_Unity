
using System;
using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{

    /// <summary>
    /// Variable of type `Building`. Inherits from `AtomVariable&lt;Building, BuildingEvent, BuildingBuildingEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Building", fileName = "BuildingVariable")]
    public sealed class BuildingVariable : AtomVariable<Building, BuildingEvent, BuildingBuildingEvent>
    {
        protected override bool AreEqual(Building first, Building second)
        {
            return first == second;
        }
    }
}