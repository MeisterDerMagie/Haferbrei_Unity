using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `Building`. Inherits from `AtomEventReferenceListener&lt;Building, BuildingEvent, BuildingEventReference, BuildingUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Building Event Reference Listener")]
    public sealed class BuildingEventReferenceListener : AtomEventReferenceListener<
        Building,
        BuildingEvent,
        BuildingEventReference,
        BuildingUnityEvent>
    { }
}
