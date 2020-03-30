using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event of type `BuildingPair`. Inherits from `AtomEvent&lt;BuildingPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/BuildingPair", fileName = "BuildingPairEvent")]
    public sealed class BuildingPairEvent : AtomEvent<BuildingPair> { }
}
