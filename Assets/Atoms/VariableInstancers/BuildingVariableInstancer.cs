using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Variable Instancer of type `Building`. Inherits from `AtomVariableInstancer&lt;BuildingVariable, Building, BuildingEvent, BuildingBuildingEvent, BuildingBuildingFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Building Instancer")]
    public class BuildingVariableInstancer : AtomVariableInstancer<BuildingVariable, Building, BuildingEvent, BuildingBuildingEvent, BuildingBuildingFunction> { }
}
