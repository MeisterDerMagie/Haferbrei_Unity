//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace Haferbrei {
public class Bauauswahl : MonoBehaviour, IInitSelf
{
    [SerializeField, BoxGroup("Atom Values"), Required] private BuildingValueList baubareGebaude;
    [SerializeField, BoxGroup("Atom Values"), Required] private BuildingEvent onBaubareGebaudeChanged;
    [SerializeField, BoxGroup("Atom Values"), Required] private BuildingVariable zuBauendesGebaeude;
    [SerializeField, BoxGroup("References"), Required] private ToggleGroup toggleGroup;
    [SerializeField, BoxGroup("References"), Required] private Toggle noBuildingToggle;
    [SerializeField, BoxGroup("References"), Required] private Building noBuilding;
    [SerializeField, BoxGroup("References"), Required] private LeanGameObjectPool prefabPool;

    public void InitSelf()
    {
        onBaubareGebaudeChanged.Register(UpdateGebaudeAuswahl);
        UpdateGebaudeAuswahl();
    }

    private void OnDestroy() => onBaubareGebaudeChanged.Unregister(UpdateGebaudeAuswahl);

    private void UpdateGebaudeAuswahl()
    {
        for (int i = transform.childCount-1; i > 0; i--) //das erste Child wird ignoriert, weil das das "None" Gebäude ist
        {
            prefabPool.Despawn(transform.GetChild(i).gameObject);
        }

        foreach (var building in baubareGebaude)
        {
            if (building == noBuilding) continue;
            
            var newEntry = prefabPool.Spawn(Vector3.zero, Quaternion.identity, transform);
            newEntry.name = building.identifier;
            newEntry.GetComponent<GebaeudeauswahlGebaeude>().SetBuilding(building);
            var entryToggle = newEntry.GetComponent<Toggle>();
            entryToggle.group = toggleGroup;
            entryToggle.onValueChanged.AddListener(UpdateZuBauendesGebaeude);
            entryToggle.isOn = false;
        }
    }
    
    public void UpdateZuBauendesGebaeude(bool _isOn)
    {
        var activeToggles = toggleGroup.ActiveToggles();
        var activeBuildingSelection = activeToggles.Any() ? activeToggles.First() : noBuildingToggle;
        if (activeBuildingSelection == null)
        {
            Debug.LogError("Die Gebäudeauswahl sollte niemals null sein!");
            return;
        }

        zuBauendesGebaeude.Value = activeBuildingSelection.GetComponent<GebaeudeauswahlGebaeude>().Building;
    }

    public void DeselectZuBauendesGebaeude() => noBuildingToggle.isOn = true;
}
}