using System;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Reference of type `Building`. Inherits from `AtomReference&lt;Building, BuildingVariable, BuildingConstant&gt;`.
    /// </summary>
    [Serializable]
    public sealed class BuildingReference : AtomReference<
        Building,
        BuildingVariable,
        BuildingConstant> { }
}
