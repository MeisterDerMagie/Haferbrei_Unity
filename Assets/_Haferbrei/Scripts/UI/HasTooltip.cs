//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Haferbrei{
public class HasTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, BoxGroup("Settings")] private string title;

    private void ShowTooltip()
    {
        Debug.Log("Entered tooltip area!");
    }

    private void HideTooltip()
    {
        Debug.Log("Left tooltip area!");
    }
    
    #region MouseEvents

    public void OnPointerEnter(PointerEventData eventData) => ShowTooltip();
    public void OnPointerExit(PointerEventData eventData) => HideTooltip();
    private void OnMouseExit() => HideTooltip();
    private void OnMouseEnter() => ShowTooltip();
    
    #endregion
}
}