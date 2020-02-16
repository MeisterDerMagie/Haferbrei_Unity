//https://breadcrumbsinteractive.com/two-unity-tricks-isometric-games/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class IsometricObject : MonoBehaviour
{
    [Tooltip("Will use this object to compute z-order")]
    public Transform target;
    
    [Tooltip("Use this to offset the object slightly in front or behind the Target object")]
    public int targetOffset = 1;
    
    void Update()
    {
        if (target == null)
            target = transform;
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingOrder = -(int)(target.position.y * Constants.Isometric.IsometricRangePerYUnit) + targetOffset;
    }
}
}