using System;
using UnityEngine.Events;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// None generic Unity Event x 2 of type `Building`. Inherits from `UnityEvent&lt;Building, Building&gt;`.
    /// </summary>
    [Serializable]
    public sealed class BuildingBuildingUnityEvent : UnityEvent<Building, Building> { }
}
