//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {

[CreateAssetMenu(fileName = "RessourceContainerCollection", menuName = "Scriptable Objects/Collections/RessourceContainer Collection", order = 0)]
public class RessourceContainerCollection : SerializedScriptableObject
{
    [SerializeField, Delayed] private string prebuiltContainersFolder; //"Assets/_Haferbrei/ScriptableObjects/Ressources/RessourceContainers/PrebuiltContainers"
    
    [SerializeField, InlineEditor, BoxGroup("Prebuilt Containers")] public List<RessourceContainer> ressourceContainers_prebuilt = new List<RessourceContainer>();
    [SerializeField, InlineEditor, BoxGroup("Instantiated at runtime")] public List<RessourceContainer> ressourceContainers_instantiatedAtRuntime = new List<RessourceContainer>();
    
    [SerializeField, InlineEditor, BoxGroup("All"), ReadOnly] private List<RessourceContainer> allRessourceContainers = new List<RessourceContainer>();
    
    public void RegisterNewRessourceContainer(RessourceContainer _newContainer)
    {
        ressourceContainers_instantiatedAtRuntime.Add(_newContainer);
        allRessourceContainers.Add(_newContainer);
    }

    public void UnregisterRessourceContainer(RessourceContainer _containerToRemoveFromList)
    {
        ressourceContainers_instantiatedAtRuntime.Remove(_containerToRemoveFromList);
        allRessourceContainers.Remove(_containerToRemoveFromList);
    }

    public List<RessourceContainer> GetAllRessourceContainers() => allRessourceContainers;

    #if UNITY_EDITOR
    //scan Assets to add prebuilt Containers to the list
    private void OnValidate()
    {
        ressourceContainers_prebuilt.Clear();
        allRessourceContainers.Clear();
        
        string[] foldersToSearch = { prebuiltContainersFolder };
        var prebuiltContainers = Wichtel.UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<RessourceContainer>(foldersToSearch);

        foreach (var container in prebuiltContainers)
        {
            ressourceContainers_prebuilt.Add(container);
            allRessourceContainers.Add(container);
        }
    }
    #endif
}
}