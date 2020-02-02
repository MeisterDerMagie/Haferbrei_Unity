using System;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Reference of type `Buildings`. Inherits from `AtomReference&lt;Buildings, BuildingsVariable, BuildingsConstant&gt;`.
    /// </summary>
    [Serializable]
    public sealed class BuildingsReference : AtomReference<
        Buildings,
        BuildingsVariable,
        BuildingsConstant> { }
}
