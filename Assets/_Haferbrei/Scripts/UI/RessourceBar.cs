using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace Haferbrei{
public class RessourceBar : MonoBehaviour, IInitSelf
{
    [SerializeField, BoxGroup("References"), Required] private RessourceContainer container;
    [SerializeField, BoxGroup("References"), Required] private GameObject ressourceEntryPrefab;

    [SerializeField, BoxGroup("References"), Required] private RessourceValueList ressourcesThatCanBeShownInRessourceBar;
    [SerializeField, BoxGroup("Info"), LabelText("Ressources to show (even if the value is 0)"), ReadOnly] public List<Ressource> ressourcesToShow = new List<Ressource>(); //einziges Field, das gespeichert werden muss

    [HideInInspector] public bool compareToPlayerRessources;
    
    private Dictionary<Ressource, RessourceBarEntry> ressourceBarEntries = new Dictionary<Ressource, RessourceBarEntry>();
    
    private List<Ressource> entriesToCreate = new List<Ressource>();
    private List<Ressource> ressourcesToShowOrdered = new List<Ressource>();
    private RessourceBarCompareToPlayerRessources ressourceBarCompareToPlayerRessources;
    
    public void InitSelf()
    {
        container.onRessourcesChanged += UpdateRessourceBar;
        ressourceBarCompareToPlayerRessources = GetComponent<RessourceBarCompareToPlayerRessources>();
        UpdateRessourceBar();
    }

    private void OnDisable() => container.onRessourcesChanged -= UpdateRessourceBar;
    private void OnDestroy() => container.onRessourcesChanged -= UpdateRessourceBar;

    public void SetContainer(RessourceContainer _container)
    {
        ResetRessourceBar();
        container.onRessourcesChanged -= UpdateRessourceBar;
        container = _container;
        
        InitSelf();
    }

    private void ResetRessourceBar()
    {
        ressourcesToShow.Clear();
        List<GameObject> entriesToDelete = new List<GameObject>();
        foreach (var entry in ressourceBarEntries) { entriesToDelete.Add(entry.Value.gameObject); }
        for (int i = entriesToDelete.Count-1; i >= 0; i--) { Destroy(entriesToDelete[i]); }
        ressourceBarEntries.Clear();
    }

    private void UpdateRessourceBar()
    {
        // 1. Update ressourcesToShow list
        // Welche Ressourcen werden gezeigt? Alle, die "zeigbar" sind (in ressourcesThatCanBeShownInRessourceBar definiert),
        // wenn der Container mindestens einmal diese Ressource enthalten hat.
        foreach (var ressource in ressourcesThatCanBeShownInRessourceBar.List)
        {
            if (ressourcesToShow.Contains(ressource)) continue;
            if(container.ressources.ContainsKey(ressource)) ressourcesToShow.Add(ressource);
        }
        
        // 2. Update existing Entries
        entriesToCreate.Clear();
        foreach (var ressource in ressourcesToShow)
        {
            if(ressourceBarEntries.ContainsKey(ressource)) ressourceBarEntries[ressource].UpdateAmount();
            else entriesToCreate.Add(ressource);
        }
        
        // 3. Create new Entries
        foreach (var ressource in entriesToCreate)
        {
            CreateNewEntry(ressource);
        }
        OrderEntries();
        
        if(ressourceBarCompareToPlayerRessources != null && compareToPlayerRessources) ressourceBarCompareToPlayerRessources.UpdateComparison(container, ressourceBarEntries);
    }

    private void CreateNewEntry(Ressource _ressource)
    {
        var newEntry = Instantiate(ressourceEntryPrefab, transform);
        #if UNITY_EDITOR 
        newEntry.name = "RessourceEntry_" + _ressource.identifier;
        #endif
        var entryComponent = newEntry.GetComponent<RessourceBarEntry>();
        entryComponent.ressource = _ressource;
        entryComponent.container = container;
        ressourceBarEntries.Add(entryComponent.ressource, entryComponent);
        entryComponent.InitializeEntry();
    }

    private void OrderEntries()
    {
        ressourcesToShowOrdered = ressourcesToShow.OrderBy(ressource => ressource.order).ToList();
        
        for (int i = 0; i < ressourcesToShowOrdered.Count; i++)
        {
            if(ressourceBarEntries.ContainsKey(ressourcesToShowOrdered[i])) ressourceBarEntries[ressourcesToShowOrdered[i]].transform.SetSiblingIndex(i);
        }
    }
}
}