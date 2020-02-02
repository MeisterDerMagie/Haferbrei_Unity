using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// List of type `Buildings`. Inherits from `AtomList&lt;Buildings, BuildingsEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Lists/Buildings", fileName = "BuildingsList")]
    public sealed class BuildingsList : AtomList<Buildings, BuildingsEvent> { }
}
