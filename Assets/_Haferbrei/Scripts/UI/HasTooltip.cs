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
    [SerializeField, BoxGroup("Settings")] public float delay;
    [SerializeField, BoxGroup("Settings"), Required] public List<TooltipBodyElement> bodyElements;

    private CoroutineHandle countdown;
    
    //--- Show and Hide ---
    private void OnEnterTooltipArea() => Tooltip.Instance.ShowTooltip(this);
    private void OnLeaveTooltipArea() => Tooltip.Instance.HideTooltip();
    
    //--- Mouse Events ---
    public void OnPointerEnter(PointerEventData eventData) => OnEnterTooltipArea();
    public void OnPointerExit(PointerEventData eventData) => OnLeaveTooltipArea();
    private void OnMouseExit() => OnLeaveTooltipArea();
    private void OnMouseEnter() => OnEnterTooltipArea();
}
}