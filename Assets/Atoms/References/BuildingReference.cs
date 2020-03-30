using System;
using UnityAtoms.BaseAtoms;
using Haferbrei;


namespace UnityAtoms
{

    /// <summary>
    /// Reference of type `Building`. Inherits from `AtomReference&lt;Building, BuildingPair, BuildingConstant, BuildingVariable, BuildingEvent, BuildingPairEvent, BuildingBuildingFunction, BuildingVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class BuildingReference : AtomReference<
        Building,
        BuildingPair,
        BuildingConstant,
        BuildingVariable,
        BuildingEvent,
        BuildingPairEvent,
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
