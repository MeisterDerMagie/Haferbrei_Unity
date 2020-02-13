//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
public class HasRessourceContainer_Saveable : SaveableComponent
{
    public override SaveableComponentData StoreData()
    {
        var data = new HasRessourceContainerData();

        data.componentID = componentID;
        data.ressourceContainerAsGuid = GetComponent<HasRessourceContainer>().ressourceContainer.guid;

        return data;
    }

    public override void RestoreData(SaveableComponentData _loadedData)
    {
        var data = _loadedData as HasRessourceContainerData;

        GetComponent<HasRessourceContainer>().ressourceContainer = ScriptableObjectWithGuidCollection.Instance.scriptableObjects[data.ressourceContainerAsGuid] as RessourceContainer;
    }
}

public class HasRessourceContainerData : SaveableComponentData
{
    public Guid ressourceContainerAsGuid;
}
}