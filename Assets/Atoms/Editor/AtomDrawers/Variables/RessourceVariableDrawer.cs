#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `Ressource`. Inherits from `AtomDrawer&lt;RessourceVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RessourceVariable))]
    public class RessourceVariableDrawer : VariableDrawer<RessourceVariable> { }
}
#endif
