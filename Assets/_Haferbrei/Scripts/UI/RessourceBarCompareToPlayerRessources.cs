//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Haferbrei {
[RequireComponent(typeof(RessourceBar))]
public class RessourceBarCompareToPlayerRessources : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private Color negativeColor;
    [SerializeField, BoxGroup("References"), Required] private RessourceContainer playerRessources;
    private RessourceContainer container;
    private readonly Dictionary<Ressource, TextMeshProUGUI> ressourceEntriesAmountTexts = new Dictionary<Ressource, TextMeshProUGUI>();
    private Color initialColor = Color.magenta;

    private void OnEnable() => playerRessources.onRessourcesChanged += UpdateColor;
    private void OnDisable() => playerRessources.onRessourcesChanged -= UpdateColor;

    public void UpdateComparison(RessourceContainer _container, Dictionary<Ressource, RessourceBarEntry> _ressourceBarEntries)
    {
        container = _container;
        ressourceEntriesAmountTexts.Clear();
        
        foreach (var ressourceEntry in _ressourceBarEntries)
        {
            ressourceEntriesAmountTexts.Add(ressourceEntry.Key, ressourceEntry.Value.amountText);
            initialColor = ressourceEntry.Value.amountText.color;
        }
        
        UpdateColor();
    }

    private void UpdateColor()
    {
        //Set Color
        foreach (var ressourceEntry in container.GetRessources())
        {
            bool containsRessource = playerRessources.ContainsRessource(ressourceEntry.Key, ressourceEntry.Value);
            
            if (!ressourceEntriesAmountTexts.ContainsKey(ressourceEntry.Key)) return;
            ressourceEntriesAmountTexts[ressourceEntry.Key].color = containsRessource ? initialColor : negativeColor;
        }
    }
}
}