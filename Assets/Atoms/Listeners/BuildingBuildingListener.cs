using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Listener x 2 of type `Building`. Inherits from `AtomX2Listener&lt;Building, BuildingBuildingAction, BuildingVariable, BuildingEvent, BuildingBuildingEvent, BuildingBuildingFunction, BuildingVariableInstancer, BuildingBuildingEventReference, BuildingBuildingUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Building x 2 Listener")]
    public sealed class BuildingBuildingListener : AtomX2Listener<
        Building,
        BuildingBuildingAction,
        BuildingVariable,
        BuildingEvent,
        BuildingBuildingEvent,
        BuildingBuildingFunction,
        BuildingVariableInstancer,
        BuildingBuildingEventReference,
        BuildingBuildingUnityEvent>
    { }
}
