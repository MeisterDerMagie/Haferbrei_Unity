using UnityEngine;
using UnityAtoms.BaseAtoms;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Set variable value Action of type `Building`. Inherits from `SetVariableValue&lt;Building, BuildingPair, BuildingVariable, BuildingConstant, BuildingReference, BuildingEvent, BuildingPairEvent, BuildingVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Building", fileName = "SetBuildingVariableValue")]
    public sealed class SetBuildingVariableValue : SetVariableValue<
        BuildingType,
        BuildingPair,
        BuildingVariable,
        BuildingConstant,
        BuildingReference,
        BuildingEvent,
        BuildingPairEvent,
        BuildingBuildingFunction,
        BuildingVariableInstancer>
    { }
}
