using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `BuildingPair`. Inherits from `AtomEventReferenceListener&lt;BuildingPair, BuildingPairEvent, BuildingPairEventReference, BuildingPairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/BuildingPair Event Reference Listener")]
    public sealed class BuildingPairEventReferenceListener : AtomEventReferenceListener<
        BuildingPair,
        BuildingPairEvent,
        BuildingPairEventReference,
        BuildingPairUnityEvent>
    { }
}
