using System;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `RessourcePair`. Inherits from `AtomEventReference&lt;RessourcePair, RessourceVariable, RessourcePairEvent, RessourceVariableInstancer, RessourcePairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RessourcePairEventReference : AtomEventReference<
        RessourcePair,
        RessourceVariable,
        RessourcePairEvent,
        RessourceVariableInstancer,
        RessourcePairEventInstancer>, IGetEvent 
    { }
}
