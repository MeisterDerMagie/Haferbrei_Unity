using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event Instancer of type `Ressource`. Inherits from `AtomEventInstancer&lt;Ressource, RessourceEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/Ressource Event Instancer")]
    public class RessourceEventInstancer : AtomEventInstancer<Ressource, RessourceEvent> { }
}
