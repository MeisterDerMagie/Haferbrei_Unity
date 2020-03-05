//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class HasRessourceContainer_Saveable : SaveableComponent
{
    [SerializeField, BoxGroup("References"), Required] private SaveableScriptableObjects saveableScriptableObjects;
    
    public override SaveableComponentData StoreData()
    {
        var data = new HasRessourceContainerData();
        var component = GetComponent<HasRessourceContainer>();

        data.componentID = componentID;
        data.ressourceContainerAsGuid = saveableScriptableObjects.ResolveReference(component.ressourceContainer);

        return data;
    }

    public override void RestoreData(SaveableComponentData _loadedData)
    {
        var data = _loadedData as HasRessourceContainerData;
        var component = GetComponent<HasRessourceContainer>();
        
        component.ressourceContainer = saveableScriptableObjects.ResolveGuid(data.ressourceContainerAsGuid) as RessourceContainer;
    }
}

public class HasRessourceContainerData : SaveableComponentData
{
    public Guid ressourceContainerAsGuid;
}
}