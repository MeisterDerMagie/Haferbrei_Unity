#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `BuildingPair`. Inherits from `AtomDrawer&lt;BuildingPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BuildingPairEvent))]
    public class BuildingPairEventDrawer : AtomDrawer<BuildingPairEvent> { }
}
#endif
