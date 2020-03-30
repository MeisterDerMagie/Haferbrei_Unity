using UnityEngine;
using UnityAtoms.BaseAtoms;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Adds Variable Instancer's Variable of type Building to a Collection or List on OnEnable and removes it on OnDestroy. 
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync Building Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncBuildingVariableInstancerToCollection : SyncVariableInstancerToCollection<Building, BuildingVariable, BuildingVariableInstancer> { }
}
