#if UNITY_2019_1_OR_NEWER
using UnityEditor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Constant property drawer of type `Buildings`. Inherits from `AtomDrawer&lt;BuildingsConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BuildingsConstant))]
    public class BuildingsConstantDrawer : VariableDrawer<BuildingsConstant> { }
}
#endif
