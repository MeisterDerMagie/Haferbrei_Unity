using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Listener of type `Building`. Inherits from `AtomListener&lt;Building, BuildingAction, BuildingEvent, BuildingUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Building Listener")]
    public sealed class BuildingListener : AtomListener<
        Building,
        BuildingAction,
        BuildingEvent,
        BuildingUnityEvent>
    { }
}
