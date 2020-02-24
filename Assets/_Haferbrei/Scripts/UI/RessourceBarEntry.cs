﻿//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Haferbrei {
public class RessourceBarEntry : MonoBehaviour
{
    [SerializeField, BoxGroup("References"), Required] private TextMeshProUGUI amountText;
    [SerializeField, BoxGroup("References"), Required] private Image ressourceIcon;
    [SerializeField, BoxGroup("Info"), ReadOnly] public Ressource ressource;
    [SerializeField, BoxGroup("Info"), ReadOnly] public RessourceContainer container;

    public void InitializeEntry()
    {
        ressourceIcon.sprite = ressource.icon;
        UpdateAmount();
    }
    
    public void UpdateAmount()
    {
        amountText.text = container.GetRessourceAmount(ressource).ToString();
    }
}
}