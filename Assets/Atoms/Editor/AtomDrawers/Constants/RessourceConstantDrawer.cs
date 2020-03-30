#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Constant property drawer of type `Ressource`. Inherits from `AtomDrawer&lt;RessourceConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RessourceConstant))]
    public class RessourceConstantDrawer : VariableDrawer<RessourceConstant> { }
}
#endif
