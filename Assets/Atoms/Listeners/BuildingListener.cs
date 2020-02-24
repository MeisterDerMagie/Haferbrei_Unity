using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Listener of type `Building`. Inherits from `AtomListener&lt;Building, BuildingAction, BuildingVariable, BuildingEvent, BuildingBuildingEvent, BuildingBuildingFunction, BuildingVariableInstancer, BuildingEventReference, BuildingUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Building Listener")]
    public sealed class BuildingListener : AtomListener<
        Building,
        BuildingAction,
        BuildingVariable,
        BuildingEvent,
        BuildingBuildingEvent,
        BuildingBuildingFunction,
        BuildingVariableInstancer,
        BuildingEventReference,
        BuildingUnityEvent>
    { }
}
