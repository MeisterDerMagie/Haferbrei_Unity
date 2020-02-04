//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class GebaeudePreviewIsColliding : MonoBehaviour
{
    [SerializeField, BoxGroup("Values"), Required] public bool gebaeudePreviewIsColliding;
    [SerializeField, BoxGroup("Info"), ReadOnly] private Collider2D previewCollider;
    
    private void Update()
    {
        if (previewCollider == null) previewCollider = GetComponentInChildren<Collider2D>();
        if (previewCollider == null)
        {
            gebaeudePreviewIsColliding = true;
            return;
        }

        List<Collider2D> colliders = new List<Collider2D>();
        colliders.Add(previewCollider);
        var colliderContacts = previewCollider.GetContacts(colliders);

        gebaeudePreviewIsColliding = (colliderContacts != 0);
    }
}
}