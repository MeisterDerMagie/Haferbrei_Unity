using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Listener x 2 of type `Ressource`. Inherits from `AtomX2Listener&lt;Ressource, RessourceRessourceAction, RessourceVariable, RessourceEvent, RessourceRessourceEvent, RessourceRessourceFunction, RessourceVariableInstancer, RessourceRessourceEventReference, RessourceRessourceUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Ressource x 2 Listener")]
    public sealed class RessourceRessourceListener : AtomX2Listener<
        Ressource,
        RessourceRessourceAction,
        RessourceVariable,
        RessourceEvent,
        RessourceRessourceEvent,
        RessourceRessourceFunction,
        RessourceVariableInstancer,
        RessourceRessourceEventReference,
        RessourceRessourceUnityEvent>
    { }
}
