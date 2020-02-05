//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei{
public class HasRessourceContainer : MonoBehaviour, IInitSelf
{
    [SerializeField, BoxGroup("Settings"), Required] private AllRessourceContainers allRessourceContainersCollection;
    [SerializeField, BoxGroup("Settings"), DisableInPlayMode]                                                           private CreationMode creationMode;
    [SerializeField, BoxGroup("Settings"), ShowIf("creationModeIsTemplate"), DisableInPlayMode, Required]  public RessourceContainer template;
    
    [InlineEditor, DisableInEditorMode]
    public RessourceContainer ressourceContainer;
    
    private enum CreationMode {CreateNewEmptyContainer, CreateNewContainerFromTemplate}
    private bool creationModeIsTemplate => creationMode == CreationMode.CreateNewContainerFromTemplate; //for OdinInspector


    public void InitSelf()
    {
        if (ressourceContainer != null) return;
        
        ressourceContainer = (creationMode == CreationMode.CreateNewEmptyContainer) ? ScriptableObject.CreateInstance<RessourceContainer>() : Instantiate(template);
        ressourceContainer.name = (gameObject.name + "_RessourceContainer");
        
        allRessourceContainersCollection.RegisterNewRessourceContainer(ressourceContainer);
    }

    private void OnDestroy()
    {
        allRessourceContainersCollection.UnregisterRessourceContainer(ressourceContainer);
    }
}
}