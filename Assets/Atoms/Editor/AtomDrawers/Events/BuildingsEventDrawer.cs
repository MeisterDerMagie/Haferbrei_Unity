#if UNITY_2019_1_OR_NEWER
using UnityEditor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Buildings`. Inherits from `AtomDrawer&lt;BuildingsEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BuildingsEvent))]
    public class BuildingsEventDrawer : AtomDrawer<BuildingsEvent> { }
}
#endif
