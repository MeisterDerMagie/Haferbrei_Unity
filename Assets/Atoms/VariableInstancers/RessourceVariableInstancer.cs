using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Variable Instancer of type `Ressource`. Inherits from `AtomVariableInstancer&lt;RessourceVariable, Ressource, RessourceEvent, RessourceRessourceEvent, RessourceRessourceFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Ressource Instancer")]
    public class RessourceVariableInstancer : AtomVariableInstancer<RessourceVariable, Ressource, RessourceEvent, RessourceRessourceEvent, RessourceRessourceFunction> { }
}
