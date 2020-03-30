using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Constant of type `Building`. Inherits from `AtomBaseVariable&lt;Building&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-teal")]
    [CreateAssetMenu(menuName = "Unity Atoms/Constants/Building", fileName = "BuildingConstant")]
    public sealed class BuildingConstant : AtomBaseVariable<Building> { }
}
