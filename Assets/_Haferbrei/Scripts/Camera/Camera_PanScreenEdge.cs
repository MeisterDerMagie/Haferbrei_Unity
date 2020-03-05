//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel.Extensions;

namespace Haferbrei {
public class Camera_PanScreenEdge : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private float edgeThreshold;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private AnimationCurve scrollSpeedZoomInfluence;
    [SerializeField, Range(0f, 1f)] private float dampingAmount;
    [SerializeField, ReadOnly] private Vector3 deltaPos;
    [SerializeField, ReadOnly] private Vector3 dampingDelta;
    [SerializeField, ReadOnly] private bool isScrolling;
    [SerializeField, ReadOnly] private bool isDamping;
    
    private Tween dampTween;

    [SerializeField, Required] Camera_Zoom zoomScript; 
    
    private void Update()
    {
        if (GameTime.GameIsPaused || !isEnabled) return;
        
        Vector3 mousePos = Input.mousePosition;
        deltaPos = Vector3.zero;

        isScrolling = false;
        
        //left screen edge
        if (mousePos.x < edgeThreshold)
        {
            deltaPos.x -= scrollSpeed;
            isScrolling = true;
            isDamping = true;
        }
        
        //right screen edge
        else if (mousePos.x > Screen.width - edgeThreshold)
        {
            deltaPos.x += scrollSpeed;
            isScrolling = true;
            isDamping = true;
        }

        //lower screen edge
        if (mousePos.y < edgeThreshold)
        {
            deltaPos.y -= scrollSpeed;
            isScrolling = true;
            isDamping = true;
        }
        
        //upper screen edge
        else if (mousePos.y > Screen.height - edgeThreshold)
        {
            deltaPos.y += scrollSpeed;
            isScrolling = true;
            isDamping = true;
        }

        PreventScrollingIfCursorOutsideOfGameWindow(mousePos);

        var deltaPosFactored = Time.deltaTime * scrollSpeedZoomInfluence.Evaluate(zoomScript.relativeZoom) * deltaPos;
        
        if(isScrolling) dampingDelta = deltaPosFactored;
        Damp();

        transform.position += deltaPosFactored;
    }

    private void Damp()
    {
        if (isScrolling) return;
        if (!isDamping)  return;
        
        transform.position += dampingDelta;
        dampingDelta *= Mathf.Pow(dampingAmount, Time.deltaTime);
        
        //Auf 0 setzen, wenn der Wert so klein ist, dass man keine Bewegung mehr sieht
        if (Mathf.Abs(dampingDelta.x) < 0.0001f && Mathf.Abs(dampingDelta.y) < 0.0001f)
        {
            dampingDelta = Vector3.zero;
            isDamping = false;
        }
    }

    private void PreventScrollingIfCursorOutsideOfGameWindow(Vector3 mousePos)
    {
        //prevent scrolling if cursor is outisde of the game view
        if (mousePos.x < -edgeThreshold || mousePos.x > Screen.width + edgeThreshold || mousePos.y < -edgeThreshold || mousePos.y > Screen.height + edgeThreshold)
        {
            isScrolling = false;
            deltaPos = Vector3.zero;
        } 
    }
}
}