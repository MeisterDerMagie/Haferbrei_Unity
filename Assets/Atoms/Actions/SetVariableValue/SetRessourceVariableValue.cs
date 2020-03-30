using UnityEngine;
using UnityAtoms.BaseAtoms;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Set variable value Action of type `Ressource`. Inherits from `SetVariableValue&lt;Ressource, RessourcePair, RessourceVariable, RessourceConstant, RessourceReference, RessourceEvent, RessourcePairEvent, RessourceVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Ressource", fileName = "SetRessourceVariableValue")]
    public sealed class SetRessourceVariableValue : SetVariableValue<
        Ressource,
        RessourcePair,
        RessourceVariable,
        RessourceConstant,
        RessourceReference,
        RessourceEvent,
        RessourcePairEvent,
        RessourceRessourceFunction,
        RessourceVariableInstancer>
    { }
}
