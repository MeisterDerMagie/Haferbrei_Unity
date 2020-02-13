using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event x 2 of type `Building`. Inherits from `AtomEvent&lt;Building, Building&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Building x 2", fileName = "BuildingBuildingEvent")]
    public sealed class BuildingBuildingEvent : AtomEvent<Building, Building> { }
}
