using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei{
public class Transform_SaveableBridge : SaveableComponent
{
    [Saveable] private Vector3 localPosition { get => transform.localPosition; set => transform.localPosition = value; }
    [Saveable] private Quaternion localRotation { get => transform.localRotation; set => transform.localRotation = value; }
    [Saveable] private Vector3 localScale { get => transform.localScale; set => transform.localScale = value; }
    
    #if UNITY_EDITOR
    public override void OnValidate()
    {
        base.OnValidate();
        componentToSave = transform;
    }
    #endif
}
}