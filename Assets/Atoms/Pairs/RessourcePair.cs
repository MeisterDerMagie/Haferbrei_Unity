using System;
using UnityEngine;
using Haferbrei;

namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;Ressource&gt;`. Inherits from `IPair&lt;Ressource&gt;`.
    /// </summary>
    [Serializable]
    public struct RessourcePair : IPair<Ressource>
    {
        public Ressource Item1 { get => _item1; set => _item1 = value; }
        public Ressource Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private Ressource _item1;
        [SerializeField]
        private Ressource _item2;

        public void Deconstruct(out Ressource item1, out Ressource item2) { item1 = Item1; item2 = Item2; }
    }
}