using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Listener of type `Ressource`. Inherits from `AtomListener&lt;Ressource, RessourceAction, RessourceVariable, RessourceEvent, RessourceRessourceEvent, RessourceRessourceFunction, RessourceVariableInstancer, RessourceEventReference, RessourceUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Ressource Listener")]
    public sealed class RessourceListener : AtomListener<
        Ressource,
        RessourceAction,
        RessourceVariable,
        RessourceEvent,
        RessourceRessourceEvent,
        RessourceRessourceFunction,
        RessourceVariableInstancer,
        RessourceEventReference,
        RessourceUnityEvent>
    { }
}
