using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei{
public class Camera_SaveableBridge : SaveableComponent
{
    public Camera cameraComponent;
    
    [Saveable] private float orthographicSize { get => cameraComponent.orthographicSize; set => cameraComponent.orthographicSize = value; }
    
    #if UNITY_EDITOR
    public override void OnValidate()
    {
        base.OnValidate();
        componentToSave = this;
        cameraComponent = GetComponent<Camera>();
    }
    #endif
}
}