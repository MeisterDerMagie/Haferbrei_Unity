#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `RessourcePair`. Inherits from `AtomDrawer&lt;RessourcePairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RessourcePairEvent))]
    public class RessourcePairEventDrawer : AtomDrawer<RessourcePairEvent> { }
}
#endif
