//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wichtel.Extensions;

namespace Haferbrei {
public class TEST_SetSizeToBounds : MonoBehaviour
{
    public TEST_RendererBounds rendererBounds;
    public float safetyMargin = 10f;
    public Bounds cameraBounds;
    
    private void Update()
    {
        if (Camera.main == null) return;

        cameraBounds = Camera.main.OrthographicBounds();
        cameraBounds.Expand(safetyMargin);
        
        bool insideCameraView = rendererBounds.bounds.Intersects(cameraBounds);
        
        rendererBounds.gameObject.SetActive(insideCameraView);
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(rendererBounds.bounds.center, rendererBounds.bounds.size);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(cameraBounds.center, cameraBounds.size);
    }
    #endif
}
}