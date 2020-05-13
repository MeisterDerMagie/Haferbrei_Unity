using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event Instancer of type `Building`. Inherits from `AtomEventInstancer&lt;Building, BuildingEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/Building Event Instancer")]
    public class BuildingEventInstancer : AtomEventInstancer<BuildingType, BuildingEvent> { }
}
