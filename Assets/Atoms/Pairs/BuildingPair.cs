using System;
using UnityEngine;
using Haferbrei;

namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;Building&gt;`. Inherits from `IPair&lt;Building&gt;`.
    /// </summary>
    [Serializable]
    public struct BuildingPair : IPair<BuildingType>
    {
        public BuildingType Item1 { get => _item1; set => _item1 = value; }
        public BuildingType Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private BuildingType _item1;
        [SerializeField]
        private BuildingType _item2;

        public void Deconstruct(out BuildingType item1, out BuildingType item2) { item1 = Item1; item2 = Item2; }
    }
}