#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;

using Haferbrei;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `&lt;Ressource, Ressource&gt;`. Inherits from `AtomEventEditor&lt;Ressource, Ressource, RessourceEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(RessourceRessourceEvent))]
    public sealed class RessourceRessourceEventEditor : AtomEventEditor<Ressource, Ressource, RessourceRessourceEvent> { }
}
#endif
