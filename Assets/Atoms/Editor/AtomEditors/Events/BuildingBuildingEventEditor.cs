#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;

using Haferbrei;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `&lt;Building, Building&gt;`. Inherits from `AtomEventEditor&lt;Building, Building, BuildingEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(BuildingBuildingEvent))]
    public sealed class BuildingBuildingEventEditor : AtomEventEditor<Building, Building, BuildingBuildingEvent> { }
}
#endif
