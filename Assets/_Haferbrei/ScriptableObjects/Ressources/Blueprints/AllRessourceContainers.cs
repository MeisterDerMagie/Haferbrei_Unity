//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {

[CreateAssetMenu(fileName = "AllRessourceContainers", menuName = "Scriptable Objects/Ressourcen/All Ressource Containers Collection", order = 0)]
public class AllRessourceContainers : ScriptableObject
{
    [SerializeField, InlineEditor, BoxGroup("Prebuilt Containers (add manually!)")] private List<RessourceContainer> allRessourceContainers_prebuilt = new List<RessourceContainer>();
    [SerializeField, InlineEditor, BoxGroup("Instantiated at runtime")] private List<RessourceContainer> allRessourceContainers_instantiatedAtRuntime = new List<RessourceContainer>();
    

    public void RegisterNewRessourceContainer(RessourceContainer _newContainer)
    {
        allRessourceContainers_instantiatedAtRuntime.Add(_newContainer);
    }

    public void UnregisterRessourceContainer(RessourceContainer _containerToRemoveFromList)
    {
        allRessourceContainers_instantiatedAtRuntime.Remove(_containerToRemoveFromList);
    }
}
}