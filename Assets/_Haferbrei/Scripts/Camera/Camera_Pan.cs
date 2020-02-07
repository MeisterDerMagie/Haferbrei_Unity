//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class Camera_Pan : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField, Range(0f, 1f)] private float dampingAmount;
    [SerializeField, ReadOnly] private Vector3 mousePositionBefore;
    [SerializeField, ReadOnly] private Vector3 mousePositionDelta;
    [SerializeField, ReadOnly] private Vector3 dampingDelta;
    [SerializeField, ReadOnly] private bool dampMovement;
    
    
    
    private void Update()
    {
        mousePositionDelta = mousePositionBefore - gameCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            dampMovement = false;
        }
        
        if (Input.GetMouseButton(1))
        {
            transform.position += mousePositionDelta;
            dampingDelta = mousePositionDelta;
        }

        if (Input.GetMouseButtonUp(1))
        {
            dampMovement = true;
        }
        
        Damp();

        mousePositionBefore = gameCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Damp()
    {
        if (!dampMovement) return;

        transform.position += dampingDelta;

        dampingDelta *= dampingAmount;
    }
}
}