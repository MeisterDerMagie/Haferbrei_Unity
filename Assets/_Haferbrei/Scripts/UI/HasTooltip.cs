//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Haferbrei{
public class HasTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, BoxGroup("Settings")] public Sprite tooltipIcon;
    [SerializeField, BoxGroup("Settings")] public LocalizedString tooltipTitle;
    [SerializeField, BoxGroup("Settings"), Required] public List<TooltipBodyElement> bodyElements;
    public bool isRectTransform;

    private CoroutineHandle countdown;
    
    //--- Show and Hide ---
    private void OnEnterTooltipArea() => Tooltip.Instance.ShowTooltip(this);
    private void OnLeaveTooltipArea() => Tooltip.Instance.HideTooltip();
    
    //--- Mouse Events ---
    public void OnPointerEnter(PointerEventData eventData) => OnEnterTooltipArea();
    public void OnPointerExit(PointerEventData eventData) => OnLeaveTooltipArea();
    private void OnMouseExit() => OnLeaveTooltipArea();
    private void OnMouseEnter() => OnEnterTooltipArea();
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        isRectTransform = (GetComponent<RectTransform>() != null);
    }
    #endif
}
}