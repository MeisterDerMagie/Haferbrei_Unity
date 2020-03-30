using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event of type `Ressource`. Inherits from `AtomEvent&lt;Ressource&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Ressource", fileName = "RessourceEvent")]
    public sealed class RessourceEvent : AtomEvent<Ressource> { }
}
