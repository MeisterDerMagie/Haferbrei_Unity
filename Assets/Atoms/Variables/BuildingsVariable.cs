
using System;
using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{

    /// <summary>
    /// Variable of type `Buildings`. Inherits from `AtomVariable&lt;Buildings, BuildingsEvent, BuildingsBuildingsEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Buildings", fileName = "BuildingsVariable")]
    public sealed class BuildingsVariable : AtomVariable<Buildings, BuildingsEvent, BuildingsBuildingsEvent>
    {
        protected override bool AreEqual(Buildings first, Buildings second)
        {
            return first == second;
        }
    }
}
