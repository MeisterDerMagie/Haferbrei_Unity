//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {

[CreateAssetMenu(fileName = "AllRessourceContainers", menuName = "Scriptable Objects/Collections/All Ressource Containers", order = 0)]
public class AllRessourceContainers : ScriptableObject
{
    [SerializeField, InlineEditor, BoxGroup("Prebuilt Containers")] private List<RessourceContainer> allRessourceContainers_prebuilt = new List<RessourceContainer>();
    [SerializeField, InlineEditor, BoxGroup("Instantiated at runtime")] private List<RessourceContainer> allRessourceContainers_instantiatedAtRuntime = new List<RessourceContainer>();
    

    public void RegisterNewRessourceContainer(RessourceContainer _newContainer)
    {
        allRessourceContainers_instantiatedAtRuntime.Add(_newContainer);
    }

    public void UnregisterRessourceContainer(RessourceContainer _containerToRemoveFromList)
    {
        allRessourceContainers_instantiatedAtRuntime.Remove(_containerToRemoveFromList);
    }

    //scan Assets to add prebuilt Containers to the list
    private void OnValidate()
    {
        allRessourceContainers_prebuilt.Clear();
        
        string[] foldersToSearch = {"Assets/_Haferbrei/ScriptableObjects/Ressources/RessourceContainers/PrebuiltContainers"};
        var allPrebuiltContainers = Wichtel.UT_ScriptableObjectsUtilities_W.GetAllScriptableObjectInstances<RessourceContainer>(foldersToSearch);

        foreach (var container in allPrebuiltContainers)
        {
            if (!allRessourceContainers_prebuilt.Contains(container)) allRessourceContainers_prebuilt.Add(container);
        }
    }
}
}