#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `Ressource`. Inherits from `AtomDrawer&lt;RessourceValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RessourceValueList))]
    public class RessourceValueListDrawer : AtomDrawer<RessourceValueList> { }
}
#endif
