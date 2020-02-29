//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class TooltipPosition : MonoBehaviour, IInitSelf
{
    [SerializeField, BoxGroup("References"), Required] private RectTransform rectTransform;
    private Camera mainCamera;
    
    public void UpdatePosition(HasTooltip _source)
    {
        var position = _source.transform.position;
        rectTransform.position = _source.isRectTransform ? position : mainCamera.WorldToScreenPoint(position);
    }
    
    public void InitSelf()
    {
        mainCamera = Camera.main;
    }
}
}