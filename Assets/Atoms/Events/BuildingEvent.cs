using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event of type `Building`. Inherits from `AtomEvent&lt;Building&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Building", fileName = "BuildingEvent")]
    public sealed class BuildingEvent : AtomEvent<Building> { }
}
