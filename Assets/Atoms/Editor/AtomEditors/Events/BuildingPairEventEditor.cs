#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using Haferbrei;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `BuildingPair`. Inherits from `AtomEventEditor&lt;BuildingPair, BuildingPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(BuildingPairEvent))]
    public sealed class BuildingPairEventEditor : AtomEventEditor<BuildingPair, BuildingPairEvent> { }
}
#endif
