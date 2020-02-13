using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Listener x 2 of type `Building`. Inherits from `AtomListener&lt;Building, Building, BuildingBuildingAction, BuildingBuildingEvent, BuildingBuildingUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Building x 2 Listener")]
    public sealed class BuildingBuildingListener : AtomListener<
        Building,
        Building,
        BuildingBuildingAction,
        BuildingBuildingEvent,
        BuildingBuildingUnityEvent>
    { }
}
