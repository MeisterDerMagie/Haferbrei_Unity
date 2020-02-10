//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {

[CreateAssetMenu(fileName = "RessourceContainerCollection", menuName = "Scriptable Objects/Collections/RessourceContainer Collection", order = 0)]
public class RessourceContainerCollection : ScriptableObject
{
    [SerializeField, Delayed] private string prebuiltContainersFolder; //"Assets/_Haferbrei/ScriptableObjects/Ressources/RessourceContainers/PrebuiltContainers"
    
    [SerializeField, InlineEditor, BoxGroup("Prebuilt Containers")] private List<RessourceContainer> ressourceContainers_prebuilt = new List<RessourceContainer>();
    [SerializeField, InlineEditor, BoxGroup("Instantiated at runtime")] private List<RessourceContainer> ressourceContainers_instantiatedAtRuntime = new List<RessourceContainer>();
    

    public void RegisterNewRessourceContainer(RessourceContainer _newContainer)
    {
        ressourceContainers_instantiatedAtRuntime.Add(_newContainer);
    }

    public void UnregisterRessourceContainer(RessourceContainer _containerToRemoveFromList)
    {
        ressourceContainers_instantiatedAtRuntime.Remove(_containerToRemoveFromList);
    }

    #if UNITY_EDITOR
    //scan Assets to add prebuilt Containers to the list
    private void OnValidate()
    {
        ressourceContainers_prebuilt.Clear();
        
        string[] foldersToSearch = { prebuiltContainersFolder };
        var prebuiltContainers = Wichtel.UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<RessourceContainer>(foldersToSearch);

        foreach (var container in prebuiltContainers)
        {
            if (!ressourceContainers_prebuilt.Contains(container)) ressourceContainers_prebuilt.Add(container);
        }
    }
    #endif
}
}