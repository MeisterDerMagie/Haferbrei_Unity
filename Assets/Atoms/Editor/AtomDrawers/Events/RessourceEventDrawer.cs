#if UNITY_2019_1_OR_NEWER
using UnityEditor;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Ressource`. Inherits from `AtomDrawer&lt;RessourceEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RessourceEvent))]
    public class RessourceEventDrawer : AtomDrawer<RessourceEvent> { }
}
#endif
