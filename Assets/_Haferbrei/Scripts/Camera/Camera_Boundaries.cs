//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel;
using Wichtel.Extensions;

namespace Haferbrei {
public class Camera_Boundaries : MonoBehaviour
{
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;
    [SerializeField] private float upperBoundary;
    [SerializeField] private float lowerBoundary;
    [SerializeField] private float elasticBoundarySize;
    
    [SerializeField, BoxGroup("Info"), ReadOnly] private float viewportLeft;
    [SerializeField, BoxGroup("Info"), ReadOnly] private float viewportRight;
    [SerializeField, BoxGroup("Info"), ReadOnly] private float viewportUpper;
    [SerializeField, BoxGroup("Info"), ReadOnly] private float viewportLower;
    [SerializeField, BoxGroup("Info"), ReadOnly] private float elasticBoundaryLeft;
    [SerializeField, BoxGroup("Info"), ReadOnly] private float elasticBoundaryRight;
    [SerializeField, BoxGroup("Info"), ReadOnly] private float elasticBoundaryUpper;
    [SerializeField, BoxGroup("Info"), ReadOnly] private float elasticBoundaryLower;

    [SerializeField, FoldoutGroup("References"), Required] private Camera_Pan panScript;
    [SerializeField, BoxGroup("Info"), ReadOnly] private bool isPanningBefore;
    
    private Camera mainCamera;

    private void OnEnable()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        var lowerLeftViewportPoint   = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f));
        var upperRightViewportPoint  = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f));

        viewportLeft  = lowerLeftViewportPoint.x;
        viewportRight = upperRightViewportPoint.x;
        viewportLower = lowerLeftViewportPoint.y;
        viewportUpper = upperRightViewportPoint.y;
        
        //elastic border
        //???
        
        //hard border
        if (viewportLeft < leftBoundary) transform.position = transform.position.With(x: transform.position.x + leftBoundary  - viewportLeft);
        if (viewportRight > rightBoundary) transform.position = transform.position.With(x: transform.position.x + rightBoundary - viewportRight);
        if (viewportLower  < lowerBoundary) transform.position = transform.position.With(y: transform.position.y + lowerBoundary - viewportLower);
        if (viewportUpper > upperBoundary) transform.position = transform.position.With(y: transform.position.y + upperBoundary - viewportUpper);

        isPanningBefore = panScript.isPanning;
    }


    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //hard boundaries gizmos
        Gizmos.color = Color.green;
        
        var boundariesLowerLeft  = new Vector3(leftBoundary, lowerBoundary);
        var boundariesUpperLeft  = new Vector3(leftBoundary, upperBoundary);
        var boundariesUpperRight = new Vector3(rightBoundary, upperBoundary);
        var boundariesLowerRight = new Vector3(rightBoundary, lowerBoundary);
        
        Gizmos.DrawLine(boundariesLowerLeft, boundariesUpperLeft);
        Gizmos.DrawLine(boundariesUpperLeft, boundariesUpperRight);
        Gizmos.DrawLine(boundariesUpperRight, boundariesLowerRight);
        Gizmos.DrawLine(boundariesLowerRight, boundariesLowerLeft);
        
        //elastic boundaries gizmos
        Gizmos.color = Color.yellow;
        
        var elasticBoundariesLowerLeft  = new Vector3(elasticBoundaryLeft, elasticBoundaryLower);
        var elasticBoundariesUpperLeft  = new Vector3(elasticBoundaryLeft, elasticBoundaryUpper);
        var elasticBoundariesUpperRight = new Vector3(elasticBoundaryRight, elasticBoundaryUpper);
        var elasticBoundariesLowerRight = new Vector3(elasticBoundaryRight, elasticBoundaryLower);
        
        Gizmos.DrawLine(elasticBoundariesLowerLeft, elasticBoundariesUpperLeft);
        Gizmos.DrawLine(elasticBoundariesUpperLeft, elasticBoundariesUpperRight);
        Gizmos.DrawLine(elasticBoundariesUpperRight, elasticBoundariesLowerRight);
        Gizmos.DrawLine(elasticBoundariesLowerRight, elasticBoundariesLowerLeft);
    }

    private void OnValidate()
    {
        elasticBoundaryLeft  = leftBoundary + elasticBoundarySize;
        elasticBoundaryRight = rightBoundary - elasticBoundarySize;
        elasticBoundaryUpper = upperBoundary - elasticBoundarySize;
        elasticBoundaryLower = lowerBoundary + elasticBoundarySize;
    }
    #endif
}
}