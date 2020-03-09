//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Haferbrei{
public class HasRessourceContainer : MonoBehaviour, IInitSelf
{
    [SerializeField, BoxGroup("References"), Required, ReadOnly] private RessourceContainerCollection allRessourceContainersCollection;
    [SerializeField, BoxGroup("Settings"), DisableInPlayMode] private CreationMode creationMode;
    [SerializeField, BoxGroup("Settings"), ShowIf("creationModeIsTemplate"), DisableInPlayMode, Required]  public RessourceContainer template;
    
    [InlineEditor, DisableInEditorMode][Saveable]
    public RessourceContainer ressourceContainer;
    
    private enum CreationMode {CreateNewEmptyContainer, CreateNewContainerFromTemplate}
    private bool creationModeIsTemplate => creationMode == CreationMode.CreateNewContainerFromTemplate; //for OdinInspector


    public void InitSelf()
    {
        if (ressourceContainer != null) return;
        
        //Create new container instance
        ressourceContainer = (creationMode == CreationMode.CreateNewEmptyContainer) ? ScriptableObject.CreateInstance<RessourceContainer>() : Instantiate(template);
        ressourceContainer.name = (gameObject.name + "_RessourceContainer");
        
        //register at the collection of all containers
        allRessourceContainersCollection.RegisterNewRessourceContainer(ressourceContainer);
    }
    
    private void OnDestroy()
    {
        //unregister from the collection of all containers
        allRessourceContainersCollection.UnregisterRessourceContainer(ressourceContainer);
    }
    
    #if UNITY_EDITOR
    //automatically get the reference to the "allRessourceContainersCollection"
    private void OnValidate()
    {
        if(allRessourceContainersCollection != null) return;

        string[] foldersToSearch = {"Assets/_Haferbrei/ScriptableObjects/_Collections"};
        var allScriptableObjects = Wichtel.UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<RessourceContainerCollection>(foldersToSearch);

        if (allScriptableObjects.Count != 0) allRessourceContainersCollection = allScriptableObjects[0];
    }
    #endif
}
}