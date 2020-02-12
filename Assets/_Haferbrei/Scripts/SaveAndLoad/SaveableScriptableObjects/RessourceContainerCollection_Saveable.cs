using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei{
public class RessourceContainerCollection_Saveable : MonoBehaviour, ISaveable
{
    public Guid guid;
    public RessourceContainerCollection ressourceContainerCollection;
    
    public SaveableData SaveData()
    {
        var collectionData = new RessourceContainerCollectionData();

        collectionData.guid = guid;
        collectionData.saveableType = "ScriptableObject";

        foreach (var container in ressourceContainerCollection.GetAllRessourceContainers())
        {
            collectionData.ressourceContainers.Add(SerializeRessourceContainer(container));
        }

        return collectionData;
    }

    public void LoadData(SaveableData _loadedData)
    {
        
    }

    public void InitSaveable()
    {
        //saveLoadController.RegisterSaveableGameObject(this);
    }

    public void OnDestroy()
    {
        //saveLoadController.UnregisterSaveableGameObject(this);
    }

    private void OnValidate()
    {
        if(guid == Guid.Empty) guid = Guid.NewGuid();
    }

    private RessourceContainerData SerializeRessourceContainer(RessourceContainer _container)
    {
        var containerData = new RessourceContainerData();
            
        containerData.guid = guid;
        containerData.saveableType = "ScriptableObject";
            
        var ressourceContainerSerialized = new Dictionary<string, int>();
        foreach (var ressource in _container.GetRessources()) ressourceContainerSerialized.Add(ressource.Key.name, ressource.Value);

        return containerData;
    }

    private RessourceContainer DeserializeRessourceContainer(RessourceContainerData _loadedData)
    {
        var ressourcesDeserialized = new Dictionary<Ressource, int>();
        
        //check if container exists, if not, instantiate a new one
        if (!ressourceContainerCollection.GetAllRessourceContainersByGuid().ContainsKey(_loadedData.guid))
        {
            var newContainer = ScriptableObject.CreateInstance<RessourceContainer>();
            newContainer.guid = _loadedData.guid;
        }
        
        foreach (var ressource in _loadedData.ressources)
        {
            
            /*
            
            
            var ressourceDeserialized = ressourceContainerCollection. allRessources.GetScriptableObjectByName(ressource.Key) as Ressource;
            if(ressourceDeserialized != null) ressourcesDeserialized.Add(ressourceDeserialized, ressource.Value);*/
        }

        return null;
    }
}

[Serializable]
public class RessourceContainerData : SaveableData
{
    public Dictionary<string, int> ressources;
}

[Serializable]
public class RessourceContainerCollectionData : SaveableData
{
    public List<RessourceContainerData> ressourceContainers;
}
}