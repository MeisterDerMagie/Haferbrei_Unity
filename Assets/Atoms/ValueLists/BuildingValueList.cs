using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Value List of type `Building`. Inherits from `AtomValueList&lt;Building, BuildingEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Building", fileName = "BuildingValueList")]
    public sealed class BuildingValueList : AtomValueList<BuildingType, BuildingEvent> { }
}
