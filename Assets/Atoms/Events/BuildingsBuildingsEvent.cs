using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event x 2 of type `Buildings`. Inherits from `AtomEvent&lt;Buildings, Buildings&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Buildings x 2", fileName = "BuildingsBuildingsEvent")]
    public sealed class BuildingsBuildingsEvent : AtomEvent<Buildings, Buildings> { }
}
