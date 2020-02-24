using System;

using Haferbrei;


namespace UnityAtoms
{

    /// <summary>
    /// Reference of type `Building`. Inherits from `AtomReference&lt;Building, BuildingConstant, BuildingVariable, BuildingEvent, BuildingBuildingEvent, BuildingBuildingFunction, BuildingVariableInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class BuildingReference : AtomReference<
        Building,
        BuildingConstant,
        BuildingVariable,
        BuildingEvent,
        BuildingBuildingEvent,
        BuildingBuildingFunction,
        BuildingVariableInstancer>, IEquatable<BuildingReference>
    {
        public BuildingReference() : base() { }
        public BuildingReference(Building value) : base(value) { }
        public bool Equals(BuildingReference other) { return base.Equals(other); }
        protected override bool ValueEquals(Building other)
        {
            throw new NotImplementedException();
        } 
    }
}
