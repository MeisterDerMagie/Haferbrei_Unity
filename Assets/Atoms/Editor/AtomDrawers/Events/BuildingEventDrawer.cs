#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Building`. Inherits from `AtomDrawer&lt;BuildingEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BuildingEvent))]
    public class BuildingEventDrawer : AtomDrawer<BuildingEvent> { }
}
#endif
