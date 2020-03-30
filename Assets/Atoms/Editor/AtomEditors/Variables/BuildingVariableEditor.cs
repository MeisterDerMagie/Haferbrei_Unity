using UnityEditor;
using UnityAtoms.Editor;
using Haferbrei;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `Building`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(BuildingVariable))]
    public sealed class BuildingVariableEditor : AtomVariableEditor<Building, BuildingPair> { }
}
