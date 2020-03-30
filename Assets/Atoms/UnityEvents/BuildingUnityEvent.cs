using System;
using UnityEngine.Events;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// None generic Unity Event of type `Building`. Inherits from `UnityEvent&lt;Building&gt;`.
    /// </summary>
    [Serializable]
    public sealed class BuildingUnityEvent : UnityEvent<Building> { }
}
