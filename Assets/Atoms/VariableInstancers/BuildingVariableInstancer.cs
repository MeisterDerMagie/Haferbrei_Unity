using UnityEngine;
using UnityAtoms.BaseAtoms;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Variable Instancer of type `Building`. Inherits from `AtomVariableInstancer&lt;BuildingVariable, BuildingPair, Building, BuildingEvent, BuildingPairEvent, BuildingBuildingFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Building Variable Instancer")]
    public class BuildingVariableInstancer : AtomVariableInstancer<
        BuildingVariable,
        BuildingPair,
        Building,
        BuildingEvent,
        BuildingPairEvent,
        BuildingBuildingFunction>
    { }
}
