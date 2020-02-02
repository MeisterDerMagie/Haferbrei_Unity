#if UNITY_2019_1_OR_NEWER
using UnityEditor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `Buildings`. Inherits from `AtomDrawer&lt;BuildingsVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BuildingsVariable))]
    public class BuildingsVariableDrawer : VariableDrawer<BuildingsVariable> { }
}
#endif
