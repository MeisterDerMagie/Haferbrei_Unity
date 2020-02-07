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
    [SerializeField] private float edgeThreshold;
    [SerializeField] private float scrollSpeed;
    [SerializeField, Range(0f, 1f)] private float dampingAmount;
    [SerializeField, ReadOnly] private Vector2 mousePosition;
    [SerializeField, ReadOnly] private Vector3 deltaPos;
    [SerializeField, ReadOnly] private Vector3 dampingDelta;
    [SerializeField, ReadOnly] private bool isScrolling;
    
    
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        deltaPos = Vector3.zero;

        isScrolling = false;
        
        //left screen edge
        if (mousePos.x < edgeThreshold)
        {
            deltaPos.x -= scrollSpeed;
            isScrolling = true;
        }
        
        //right screen edge
        else if (mousePos.x > Screen.width - edgeThreshold)
        {
            deltaPos.x += scrollSpeed;
            isScrolling = true;
        }

        //lower screen edge
        if (mousePos.y < edgeThreshold)
        {
            deltaPos.y -= scrollSpeed;
            isScrolling = true;
        }
        
        //upper screen edge
        else if (mousePos.y > Screen.height - edgeThreshold)
        {
            deltaPos.y += scrollSpeed;
            isScrolling = true;
        }

        PreventScrollingIfCursorOutsideOfGameWindow(mousePos);

        if(isScrolling) dampingDelta = deltaPos;
        Damp();

        transform.position += deltaPos;
    }

    private void Damp()
    {
        if (isScrolling) return;
        dampingDelta *= dampingAmount;
        transform.position += dampingDelta;
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