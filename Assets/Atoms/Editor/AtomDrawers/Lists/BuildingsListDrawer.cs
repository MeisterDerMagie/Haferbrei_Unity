#if UNITY_2019_1_OR_NEWER
using UnityEditor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// List property drawer of type `Buildings`. Inherits from `AtomDrawer&lt;BuildingsList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BuildingsList))]
    public class BuildingsListDrawer : AtomDrawer<BuildingsList> { }
}
#endif
