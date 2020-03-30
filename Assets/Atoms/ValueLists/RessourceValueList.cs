using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Value List of type `Ressource`. Inherits from `AtomValueList&lt;Ressource, RessourceEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Ressource", fileName = "RessourceValueList")]
    public sealed class RessourceValueList : AtomValueList<Ressource, RessourceEvent> { }
}
