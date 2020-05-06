//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel.Extensions;

namespace Haferbrei {
public class Camera_ViewBounds : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [ShowInInspector] public static Bounds cameraViewBounds; 
    
    private void Update()
    {
        cameraViewBounds = camera.OrthographicBounds();
    }
}
}