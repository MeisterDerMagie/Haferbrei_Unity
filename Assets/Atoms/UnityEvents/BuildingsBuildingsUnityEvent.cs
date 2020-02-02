using System;
using UnityEngine.Events;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// None generic Unity Event x 2 of type `Buildings`. Inherits from `UnityEvent&lt;Buildings, Buildings&gt;`.
    /// </summary>
    [Serializable]
    public sealed class BuildingsBuildingsUnityEvent : UnityEvent<Buildings, Buildings> { }
}
