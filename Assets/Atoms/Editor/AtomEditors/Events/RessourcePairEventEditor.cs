#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using Haferbrei;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `RessourcePair`. Inherits from `AtomEventEditor&lt;RessourcePair, RessourcePairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(RessourcePairEvent))]
    public sealed class RessourcePairEventEditor : AtomEventEditor<RessourcePair, RessourcePairEvent> { }
}
#endif
