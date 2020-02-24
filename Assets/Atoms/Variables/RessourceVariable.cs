
using System;
using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{

    /// <summary>
    /// Variable of type `Ressource`. Inherits from `AtomVariable&lt;Ressource, RessourceEvent, RessourceRessourceEvent, RessourceRessourceFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Ressource", fileName = "RessourceVariable")]
    public sealed class RessourceVariable : AtomVariable<Ressource, RessourceEvent, RessourceRessourceEvent, RessourceRessourceFunction>
    {
        protected override bool ValueEquals(Ressource other)
        {
            throw new NotImplementedException();
        }
    }
}
