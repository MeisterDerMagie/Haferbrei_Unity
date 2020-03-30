#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Constant property drawer of type `Building`. Inherits from `AtomDrawer&lt;BuildingConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BuildingConstant))]
    public class BuildingConstantDrawer : VariableDrawer<BuildingConstant> { }
}
#endif
