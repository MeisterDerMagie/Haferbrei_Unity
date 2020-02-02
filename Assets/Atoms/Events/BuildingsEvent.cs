using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event of type `Buildings`. Inherits from `AtomEvent&lt;Buildings&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Buildings", fileName = "BuildingsEvent")]
    public sealed class BuildingsEvent : AtomEvent<Buildings> { }
}
