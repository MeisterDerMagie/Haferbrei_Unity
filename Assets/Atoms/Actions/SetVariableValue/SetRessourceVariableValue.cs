using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Set variable value Action of type `Ressource`. Inherits from `SetVariableValue&lt;Ressource, RessourceVariable, RessourceConstant, RessourceReference, RessourceEvent, RessourceRessourceEvent, RessourceVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Ressource", fileName = "SetRessourceVariableValue")]
    public sealed class SetRessourceVariableValue : SetVariableValue<
        Ressource,
        RessourceVariable,
        RessourceConstant,
        RessourceReference,
        RessourceEvent,
        RessourceRessourceEvent,
        RessourceRessourceFunction,
        RessourceVariableInstancer>
    { }
}
