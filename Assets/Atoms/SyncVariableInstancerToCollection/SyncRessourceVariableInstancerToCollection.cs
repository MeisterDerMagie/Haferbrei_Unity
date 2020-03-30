using UnityEngine;
using UnityAtoms.BaseAtoms;
using Haferbrei;


namespace UnityAtoms
{
    /// <summary>
    /// Adds Variable Instancer's Variable of type Ressource to a Collection or List on OnEnable and removes it on OnDestroy. 
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync Ressource Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncRessourceVariableInstancerToCollection : SyncVariableInstancerToCollection<Ressource, RessourceVariable, RessourceVariableInstancer> { }
}
