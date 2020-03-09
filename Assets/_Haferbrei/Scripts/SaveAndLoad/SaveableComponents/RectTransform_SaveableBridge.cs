using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei{
[RequireComponent(typeof(RectTransform)), DisallowMultipleComponent]
public class RectTransform_SaveableBridge : MonoBehaviour
{ 
    private RectTransform rectTransform;

    [Saveable] private Vector3 localPosition { get => transform.localPosition; set => transform.localPosition = value; }
    [Saveable] private Quaternion localRotation { get => transform.localRotation; set => transform.localRotation = value; }
    [Saveable] private Vector3 localScale { get => transform.localScale; set => transform.localScale = value; }
    
    [Saveable] private Vector2 anchoredPosition { get => rectTransform.anchoredPosition; set => rectTransform.anchoredPosition = value; }
    [Saveable] private Vector3 anchoredPosition3D { get => rectTransform.anchoredPosition3D; set => rectTransform.anchoredPosition3D = value; }
    [Saveable] private Vector2 anchorMax { get => rectTransform.anchorMax; set => rectTransform.anchorMax = value; }
    [Saveable] private Vector2 offsetMax { get => rectTransform.offsetMax; set => rectTransform.offsetMax = value; }
    [Saveable] private Vector2 offsetMin { get => rectTransform.offsetMin; set => rectTransform.offsetMin = value; }
    [Saveable] private Vector2 pivot { get => rectTransform.pivot; set => rectTransform.pivot = value; }
    [Saveable] private Vector2 sizeDelta { get => rectTransform.sizeDelta; set => rectTransform.sizeDelta = value; }

    #if UNITY_EDITOR
    private void OnValidate() => rectTransform = GetComponent<RectTransform>();
    #endif
}
}