using System;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `BuildingPair`. Inherits from `AtomEventReference&lt;BuildingPair, BuildingVariable, BuildingPairEvent, BuildingVariableInstancer, BuildingPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class BuildingPairEventReference : AtomEventReference<
        BuildingPair,
        BuildingVariable,
        BuildingPairEvent,
        BuildingVariableInstancer,
        BuildingPairEventInstancer>, IGetEvent 
    { }
}
