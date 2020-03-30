using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event of type `RessourcePair`. Inherits from `AtomEvent&lt;RessourcePair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/RessourcePair", fileName = "RessourcePairEvent")]
    public sealed class RessourcePairEvent : AtomEvent<RessourcePair> { }
}
