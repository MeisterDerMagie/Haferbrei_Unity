using System;

using Haferbrei;


namespace UnityAtoms
{

    /// <summary>
    /// Reference of type `Ressource`. Inherits from `AtomReference&lt;Ressource, RessourceConstant, RessourceVariable, RessourceEvent, RessourceRessourceEvent, RessourceRessourceFunction, RessourceVariableInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RessourceReference : AtomReference<
        Ressource,
        RessourceConstant,
        RessourceVariable,
        RessourceEvent,
        RessourceRessourceEvent,
        RessourceRessourceFunction,
        RessourceVariableInstancer>, IEquatable<RessourceReference>
    {
        public RessourceReference() : base() { }
        public RessourceReference(Ressource value) : base(value) { }
        public bool Equals(RessourceReference other) { return base.Equals(other); }
        protected override bool ValueEquals(Ressource other)
        {
            throw new NotImplementedException();
        } 
    }
}
