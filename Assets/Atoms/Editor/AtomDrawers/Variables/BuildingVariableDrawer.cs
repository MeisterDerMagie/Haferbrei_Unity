#if UNITY_2019_1_OR_NEWER
using UnityEditor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `Building`. Inherits from `AtomDrawer&lt;BuildingVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BuildingVariable))]
    public class BuildingVariableDrawer : VariableDrawer<BuildingVariable> { }
}
#endif
