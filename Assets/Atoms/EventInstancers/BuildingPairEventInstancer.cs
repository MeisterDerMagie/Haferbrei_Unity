using UnityEngine;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Event Instancer of type `BuildingPair`. Inherits from `AtomEventInstancer&lt;BuildingPair, BuildingPairEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/BuildingPair Event Instancer")]
    public class BuildingPairEventInstancer : AtomEventInstancer<BuildingPair, BuildingPairEvent> { }
}
