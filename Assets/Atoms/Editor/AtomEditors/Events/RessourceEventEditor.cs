#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;

using Haferbrei;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Ressource`. Inherits from `AtomEventEditor&lt;Ressource, RessourceEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(RessourceEvent))]
    public sealed class RessourceEventEditor : AtomEventEditor<Ressource, RessourceEvent> { }
}
#endif
