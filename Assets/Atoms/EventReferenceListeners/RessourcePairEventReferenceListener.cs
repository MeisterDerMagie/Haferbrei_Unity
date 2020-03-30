using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `RessourcePair`. Inherits from `AtomEventReferenceListener&lt;RessourcePair, RessourcePairEvent, RessourcePairEventReference, RessourcePairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/RessourcePair Event Reference Listener")]
    public sealed class RessourcePairEventReferenceListener : AtomEventReferenceListener<
        RessourcePair,
        RessourcePairEvent,
        RessourcePairEventReference,
        RessourcePairUnityEvent>
    { }
}
