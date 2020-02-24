using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Set variable value Action of type `Building`. Inherits from `SetVariableValue&lt;Building, BuildingVariable, BuildingConstant, BuildingReference, BuildingEvent, BuildingBuildingEvent, BuildingVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Building", fileName = "SetBuildingVariableValue")]
    public sealed class SetBuildingVariableValue : SetVariableValue<
        Building,
        BuildingVariable,
        BuildingConstant,
        BuildingReference,
        BuildingEvent,
        BuildingBuildingEvent,
        BuildingBuildingFunction,
        BuildingVariableInstancer>
    { }
}
