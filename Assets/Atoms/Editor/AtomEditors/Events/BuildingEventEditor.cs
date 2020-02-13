#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;

using Haferbrei;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Building`. Inherits from `AtomEventEditor&lt;Building, BuildingEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(BuildingEvent))]
    public sealed class BuildingEventEditor : AtomEventEditor<Building, BuildingEvent>
    {
        protected override VisualElement GetRaiseValueInput()
        {
            var input = new Toggle() { label = "Raise value", name = "Raise value", viewDataKey = "Raise value" };
            input.RegisterCallback<ChangeEvent<Building>>((evt) => { _raiseValue = evt.newValue; });
            return input;
        }
    }
}
#endif
