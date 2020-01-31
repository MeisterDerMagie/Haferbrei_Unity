using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel.Extensions;

public class UT_GizmoDrawVerticalLine : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private float lenght = 3f;
    [SerializeField, BoxGroup("Settings"), Required] private Color color = Color.white;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        
        var startingPoint = transform.position.With(y: -lenght/2f);
        var endPoint = transform.position.With(y: lenght / 2f);
        
        Gizmos.DrawLine(startingPoint, endPoint);
    }
}
