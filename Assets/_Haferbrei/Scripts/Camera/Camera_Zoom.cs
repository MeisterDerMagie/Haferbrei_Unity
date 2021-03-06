﻿//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityBehaviour;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Haferbrei {
public class Camera_Zoom : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private bool isEnabled = true;
    [SerializeField, BoxGroup("Settings")] public bool invertScrollDirection;
    [SerializeField, BoxGroup("Settings"), Min(0f)] private float zoomSpeed;
    [SerializeField, BoxGroup("Settings"), Min(0f)] private float sensitivity;
    [SerializeField, BoxGroup("Settings"), Min(0f)] private float minZoomValue;
    [SerializeField, BoxGroup("Settings"), Min(0f)] private float maxZoomValue;
    [SerializeField, BoxGroup("Settings"), Required] private AnimationCurve zoomSensitivityCurve;
    [SerializeField, BoxGroup("References"), Required] private Camera mainCamera;

    [SerializeField, BoxGroup("Info"), ReadOnly] public float relativeZoom;
    [SerializeField, BoxGroup("Info"), ReadOnly] private float newSize;
    
    private Tween zoomTween;

    private void OnEnable() => newSize = mainCamera.orthographicSize;

    private void Update()
    {
        if(GameTime.GameIsPaused || !isEnabled) return;

        relativeZoom = Wichtel.MathW.Remap(newSize, minZoomValue, maxZoomValue, 0f, 1f);

        var newSizeDelta = Input.mouseScrollDelta.y * sensitivity * zoomSensitivityCurve.Evaluate(relativeZoom);
        if (invertScrollDirection) newSize += newSizeDelta;
        else                       newSize -= newSizeDelta;
        
        newSize = Mathf.Clamp(newSize, minZoomValue, maxZoomValue);

        if (newSize != mainCamera.orthographicSize)
        {
            zoomTween.Kill();
            zoomTween = mainCamera.DOOrthoSize(newSize, zoomSpeed).SetEase(Ease.OutExpo);
        }
    }
}
}