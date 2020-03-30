using System;
using UnityEngine;
using Haferbrei;

namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;Building&gt;`. Inherits from `IPair&lt;Building&gt;`.
    /// </summary>
    [Serializable]
    public struct BuildingPair : IPair<Building>
    {
        public Building Item1 { get => _item1; set => _item1 = value; }
        public Building Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private Building _item1;
        [SerializeField]
        private Building _item2;

        public void Deconstruct(out Building item1, out Building item2) { item1 = Item1; item2 = Item2; }
    }
}