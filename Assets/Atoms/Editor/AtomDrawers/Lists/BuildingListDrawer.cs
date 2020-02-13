#if UNITY_2019_1_OR_NEWER
using UnityEditor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// List property drawer of type `Building`. Inherits from `AtomDrawer&lt;BuildingList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BuildingList))]
    public class BuildingListDrawer : AtomDrawer<BuildingList> { }
}
#endif
