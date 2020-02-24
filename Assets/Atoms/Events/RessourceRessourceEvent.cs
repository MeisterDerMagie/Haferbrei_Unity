using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event x 2 of type `Ressource`. Inherits from `AtomEvent&lt;Ressource, Ressource&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Ressource x 2", fileName = "RessourceRessourceEvent")]
    public sealed class RessourceRessourceEvent : AtomEvent<Ressource, Ressource> { }
}
