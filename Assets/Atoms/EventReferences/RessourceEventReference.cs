using System;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `Ressource`. Inherits from `AtomEventReference&lt;Ressource, RessourceVariable, RessourceEvent, RessourceVariableInstancer, RessourceEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RessourceEventReference : AtomEventReference<
        Ressource,
        RessourceVariable,
        RessourceEvent,
        RessourceVariableInstancer,
        RessourceEventInstancer>, IGetEvent 
    { }
}
