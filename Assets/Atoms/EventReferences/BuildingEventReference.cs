using System;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `Building`. Inherits from `AtomEventReference&lt;Building, BuildingVariable, BuildingEvent, BuildingBuildingEvent, BuildingBuildingFunction, BuildingVariableInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class BuildingEventReference : AtomEventReference<
        Building,
        BuildingVariable,
        BuildingEvent,
        BuildingBuildingEvent,
        BuildingBuildingFunction,
        BuildingVariableInstancer> { }
}
