using System;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `Building`. Inherits from `AtomEventReference&lt;Building, BuildingVariable, BuildingEvent, BuildingVariableInstancer, BuildingEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class BuildingEventReference : AtomEventReference<
        BuildingType,
        BuildingVariable,
        BuildingEvent,
        BuildingVariableInstancer,
        BuildingEventInstancer>, IGetEvent 
    { }
}
