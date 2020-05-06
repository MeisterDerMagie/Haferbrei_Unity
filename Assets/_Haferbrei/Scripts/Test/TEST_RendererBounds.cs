//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wichtel.Extensions;

namespace Haferbrei {
public class TEST_RendererBounds : MonoBehaviour
{
    public Bounds bounds;
    
    private void Update()
    {
        bounds = BoundsExtensions.CalculateBoundsInChildren(transform);
    }

    
}
}