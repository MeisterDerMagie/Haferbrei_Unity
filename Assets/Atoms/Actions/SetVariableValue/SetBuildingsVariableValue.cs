using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Set variable value Action of type `Buildings`. Inherits from `SetVariableValue&lt;Buildings, BuildingsVariable, BuildingsConstant, BuildingsReference, BuildingsEvent, BuildingsBuildingsEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Buildings", fileName = "SetBuildingsVariableValue")]
    public sealed class SetBuildingsVariableValue : SetVariableValue<
        Buildings,
        BuildingsVariable,
        BuildingsConstant,
        BuildingsReference,
        BuildingsEvent,
        BuildingsBuildingsEvent>
    { }
}
