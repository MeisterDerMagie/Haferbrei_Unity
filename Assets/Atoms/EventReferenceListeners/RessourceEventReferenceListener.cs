using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `Ressource`. Inherits from `AtomEventReferenceListener&lt;Ressource, RessourceEvent, RessourceEventReference, RessourceUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Ressource Event Reference Listener")]
    public sealed class RessourceEventReferenceListener : AtomEventReferenceListener<
        Ressource,
        RessourceEvent,
        RessourceEventReference,
        RessourceUnityEvent>
    { }
}
