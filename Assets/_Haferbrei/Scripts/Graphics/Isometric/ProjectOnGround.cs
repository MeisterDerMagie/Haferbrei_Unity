//(c) copyright by Martin M. Klöckener
//based on: https://breadcrumbsinteractive.com/two-unity-tricks-isometric-games/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
[ExecuteInEditMode]
public class ProjectOnGround : MonoBehaviour
{
    private Quaternion savedRotation;
    private bool cached;

    // called before rendering the object        
    void OnWillRenderObject()
    {
        savedRotation = transform.rotation;
        cached = true;
        var eulers = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(Constants.Isometric.PerspectiveAngle, eulers.y, eulers.z);
    }

    //called right after rendering the object
    void OnRenderObject()
    {
        if (!cached) return;
        transform.rotation = savedRotation;
        cached = false;
    }
}
}