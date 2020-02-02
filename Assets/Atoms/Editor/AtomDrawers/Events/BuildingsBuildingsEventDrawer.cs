#if UNITY_2019_1_OR_NEWER
using UnityEditor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event x 2 property drawer of type `Buildings`. Inherits from `AtomDrawer&lt;BuildingsBuildingsEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BuildingsBuildingsEvent))]
    public class BuildingsBuildingsEventDrawer : AtomDrawer<BuildingsBuildingsEvent> { }
}
#endif
