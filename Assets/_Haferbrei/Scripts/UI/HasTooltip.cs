//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Haferbrei{
public class HasTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, BoxGroup("Settings")] private string title;
    [SerializeField, BoxGroup("Settings"), Required] private float delay;

    private CoroutineHandle countdown;
    
    private void OnEnterTooltipArea()
    {
        countdown = Timing.CallDelayed(delay, ShowTooltip);
    }

    private void OnLeaveTooltipArea()
    {
        Timing.KillCoroutines(countdown);
    }

    private void ShowTooltip()
    {
        Debug.Log("Show tooltip!");
    }

    private void HideTooltip()
    {
        Debug.Log("Hide tooltip!");
    }
    
    #region MouseEvents

    public void OnPointerEnter(PointerEventData eventData) => OnEnterTooltipArea();
    public void OnPointerExit(PointerEventData eventData) => OnLeaveTooltipArea();
    private void OnMouseExit() => OnLeaveTooltipArea();
    private void OnMouseEnter() => OnEnterTooltipArea();
    
    #endregion
}
}