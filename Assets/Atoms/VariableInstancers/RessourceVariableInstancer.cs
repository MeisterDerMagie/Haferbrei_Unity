using UnityEngine;
using UnityAtoms.BaseAtoms;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Variable Instancer of type `Ressource`. Inherits from `AtomVariableInstancer&lt;RessourceVariable, RessourcePair, Ressource, RessourceEvent, RessourcePairEvent, RessourceRessourceFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Ressource Variable Instancer")]
    public class RessourceVariableInstancer : AtomVariableInstancer<
        RessourceVariable,
        RessourcePair,
        Ressource,
        RessourceEvent,
        RessourcePairEvent,
        RessourceRessourceFunction>
    { }
}
