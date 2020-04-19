//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Wichtel.Extensions;
using Wichtel.UI;

namespace Haferbrei {
public class TooltipPosition : MonoBehaviour, IInitSelf
{
    [SerializeField, BoxGroup("Settings"), Required] private float yOffset;
    [SerializeField, BoxGroup("References"), Required] private RectTransform rectTransform;
    [SerializeField, BoxGroup("References"), Required] private CanvasGroup canvasGroup;
    private Camera mainCamera;
    private float initialYOffset;

    public void InitSelf()
    {
        mainCamera = Camera.main;
        initialYOffset = yOffset;
    }
    
    public void UpdatePosition(HasTooltip _source)
    {
        //yOffset berechnen
        yOffset = initialYOffset + _source.optionalAdditionalTooltipYOffset;
        //generelle Position auf dem Bildschirm berechnen
        var position = _source.transform.position;
        rectTransform.position = _source.isRectTransform ? position : mainCamera.WorldToScreenPoint(position);
        
        //yOffset anwenden und Tooltip verschieben, falls er oben aus dem Bild ragt
        Timing.RunCoroutine(_SetYPosition());
        //Tooltip auf der X-Achse im Bildschirmbereich halten
        SetXPosition();
    }
    
    private IEnumerator<float> _SetYPosition()
    {
        //einen Frame warten, damit die TooltipLayouts korrekt rebuildet sind. Das hier führt zu dem kurzen Flackern des Tooltips...
        canvasGroup.alpha = 0f;
        yield return Timing.WaitForOneFrame;
        canvasGroup.alpha = 1f;
        
        //Offset anwenden
        rectTransform.position = rectTransform.position.With(y: rectTransform.position.y + yOffset);
        
        //ScreenSize Größe des Tooltips berechnen
        Bounds rectTransformBounds = UI_RectTransformExtensions.GetRectTransformBounds(rectTransform);
        float yMax = rectTransformBounds.max.y;
        
        //Wenn der Tooltip oben aus dem Bild herausragt, wird er statt über dem Element, darunter angezeigt
        if (yMax > Screen.height)
        {
            float newYPosition = rectTransform.position.y - 2 * yOffset - rectTransformBounds.size.y;
            rectTransform.position = rectTransform.position.With(y: newYPosition);
        }
    }

    private void SetXPosition()
    {
        float xMin = UI_RectTransformExtensions.GetRectTransformBounds(rectTransform).min.x;
        if (xMin < 0f) //ragt über den linken Bildschirmrand hinaus
        {
            rectTransform.position = rectTransform.position.With(x: rectTransform.position.x + -xMin);
            return;
        }
        
        float xMax = UI_RectTransformExtensions.GetRectTransformBounds(rectTransform).max.x;
        if (xMax > Screen.width) //ragt über den rechten Bildschirmrand hinaus
        {
            rectTransform.position = rectTransform.position.With(x: rectTransform.position.x - (xMax - Screen.width));
        }
    }
}
}