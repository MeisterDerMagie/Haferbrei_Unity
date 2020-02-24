using System;
using UnityEngine.Events;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// None generic Unity Event x 2 of type `Ressource`. Inherits from `UnityEvent&lt;Ressource, Ressource&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RessourceRessourceUnityEvent : UnityEvent<Ressource, Ressource> { }
}
