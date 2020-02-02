using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Listener of type `Buildings`. Inherits from `AtomListener&lt;Buildings, BuildingsAction, BuildingsEvent, BuildingsUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Buildings Listener")]
    public sealed class BuildingsListener : AtomListener<
        Buildings,
        BuildingsAction,
        BuildingsEvent,
        BuildingsUnityEvent>
    { }
}
