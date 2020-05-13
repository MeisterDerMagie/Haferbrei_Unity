#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using Haferbrei;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Building`. Inherits from `AtomEventEditor&lt;Building, BuildingEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(BuildingEvent))]
    public sealed class BuildingEventEditor : AtomEventEditor<BuildingType, BuildingEvent> { }
}
#endif
