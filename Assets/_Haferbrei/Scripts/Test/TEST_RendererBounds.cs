//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
public class TEST_RendererBounds : MonoBehaviour
{
    public Bounds bounds;
    
    private void Update()
    {
        bounds = CalculateBounds();
    }

    private Bounds CalculateBounds()
    {
        Bounds bounds = new Bounds(transform.position, Vector3.zero);
 
        foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
    }
}
}