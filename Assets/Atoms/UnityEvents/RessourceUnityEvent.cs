using System;
using UnityEngine.Events;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// None generic Unity Event of type `Ressource`. Inherits from `UnityEvent&lt;Ressource&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RessourceUnityEvent : UnityEvent<Ressource> { }
}
