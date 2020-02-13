//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace Haferbrei {
public class Bauauswahl : MonoBehaviour
{
    [SerializeField, BoxGroup("Atom Values"), Required] private BuildingVariable zuBauendesGebaeude;
    [SerializeField, FoldoutGroup("References"), Required] private ToggleGroup toggleGroup;
    [SerializeField, FoldoutGroup("References"), Required] private Toggle noBuilding;

    public void UpdateZuBauendesGebaeude()
    {
        var activeToggles = toggleGroup.ActiveToggles();
        var activeBuildingSelection = activeToggles.Any() ? activeToggles.First() : noBuilding;
        if (activeBuildingSelection == null)
        {
            Debug.LogError("Die Gebäudeauswahl sollte niemals null sein!");
            return;
        }

        zuBauendesGebaeude.Value = activeBuildingSelection.GetComponent<GebaeudeauswahlGebaeude>().gebaeude;
    }

    public void DeselectZuBauendesGebaeude() => noBuilding.isOn = true;
}
}