#if UNITY_2019_1_OR_NEWER
using UnityEditor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event x 2 property drawer of type `Building`. Inherits from `AtomDrawer&lt;BuildingBuildingEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BuildingBuildingEvent))]
    public class BuildingBuildingEventDrawer : AtomDrawer<BuildingBuildingEvent> { }
}
#endif
