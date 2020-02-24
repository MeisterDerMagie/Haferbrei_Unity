using System;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event x 2 Reference of type `Building`. Inherits from `AtomEventX2Reference&lt;Building, BuildingVariable, BuildingEvent, BuildingBuildingEvent, BuildingBuildingFunction, BuildingVariableInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class BuildingBuildingEventReference : AtomEventX2Reference<
        Building,
        BuildingVariable,
        BuildingEvent,
        BuildingBuildingEvent,
        BuildingBuildingFunction,
        BuildingVariableInstancer> { }
}
