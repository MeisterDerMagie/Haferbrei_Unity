//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Haferbrei {
public class RessourceBarEntry : MonoBehaviour
{
    [SerializeField, BoxGroup("References"), Required] private Image ressourceIcon;
    [SerializeField, BoxGroup("References"), Required] public TextMeshProUGUI amountText;
    [SerializeField, BoxGroup("Info"), ReadOnly] public Ressource ressource;
    [SerializeField, BoxGroup("Info"), ReadOnly] public RessourceContainer container;

    public void InitializeEntry()
    {
        ressourceIcon.sprite = ressource.icon;
        
        // Tooltip
        var tooltip = GetComponent<HasTooltip>();
        if (tooltip != null)
        {
            tooltip.tooltipTitle.mTerm = $"Ressources/{ressource.identifier}";
            tooltip.tooltipIcon = ressource.icon;
        }
        
        //Update
        UpdateAmount();
    }
    
    public void UpdateAmount()
    {
        amountText.text = container.GetRessourceAmount(ressource).ToString();
    }
}
}