#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;

using Haferbrei;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Buildings`. Inherits from `AtomEventEditor&lt;Buildings, BuildingsEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(BuildingsEvent))]
    public sealed class BuildingsEventEditor : AtomEventEditor<Buildings, BuildingsEvent>
    {
        protected override VisualElement GetRaiseValueInput()
        {
            var input = new Toggle() { label = "Raise value", name = "Raise value", viewDataKey = "Raise value" };
            input.RegisterCallback<ChangeEvent<Buildings>>((evt) => { _raiseValue = evt.newValue; });
            return input;
        }
    }
}
#endif
