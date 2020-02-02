using UnityEngine;

using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Listener x 2 of type `Buildings`. Inherits from `AtomListener&lt;Buildings, Buildings, BuildingsBuildingsAction, BuildingsBuildingsEvent, BuildingsBuildingsUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Buildings x 2 Listener")]
    public sealed class BuildingsBuildingsListener : AtomListener<
        Buildings,
        Buildings,
        BuildingsBuildingsAction,
        BuildingsBuildingsEvent,
        BuildingsBuildingsUnityEvent>
    { }
}
