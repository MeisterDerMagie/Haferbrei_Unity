//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
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
    [SerializeField, ReadOnly] private bool isDamping;
    [SerializeField, ReadOnly] public bool isPanning;

    private void Update()
    {
        mousePositionDelta = mousePositionBefore - gameCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            isDamping = false;
            mousePositionDelta = Vector3.zero;
            dampingDelta = Vector3.zero;
            isPanning = true;
        }
        
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            transform.position += mousePositionDelta;
            dampingDelta = mousePositionDelta;
        }

        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
        {
            isDamping = true;
            isPanning = false;
        }
        
        Damp();

        mousePositionBefore = gameCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Damp()
    {
        if (!isDamping) return;

        transform.position += dampingDelta;
        dampingDelta *= Mathf.Pow(dampingAmount, Time.deltaTime);

        //Auf 0 setzen, wenn der Wert so klein ist, dass man keine Bewegung mehr sieht
        if (Mathf.Abs(dampingDelta.x) < 0.0001f && Mathf.Abs(dampingDelta.y) < 0.0001f)
        {
            dampingDelta = Vector3.zero;
            isDamping = false;
        }
    }
}
}