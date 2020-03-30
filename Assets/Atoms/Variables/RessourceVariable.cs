using UnityEngine;
using System;
using Haferbrei;


namespace UnityAtoms
{

    /// <summary>
    /// Variable of type `Ressource`. Inherits from `AtomVariable&lt;Ressource, RessourcePair, RessourceEvent, RessourcePairEvent, RessourceRessourceFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Ressource", fileName = "RessourceVariable")]
    public sealed class RessourceVariable : AtomVariable<Ressource, RessourcePair, RessourceEvent, RessourcePairEvent, RessourceRessourceFunction>
    {
        protected override bool ValueEquals(Ressource other)
        {
            throw new NotImplementedException();
        }
    }
}
