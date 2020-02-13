using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// List of type `Building`. Inherits from `AtomList&lt;Building, BuildingEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Lists/Building", fileName = "BuildingList")]
    public sealed class BuildingList : AtomList<Building, BuildingEvent> { }
}
