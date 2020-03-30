#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `Building`. Inherits from `AtomDrawer&lt;BuildingValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BuildingValueList))]
    public class BuildingValueListDrawer : AtomDrawer<BuildingValueList> { }
}
#endif
