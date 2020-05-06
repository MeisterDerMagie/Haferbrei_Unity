//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class PrefabRepresentative : MonoBehaviour
{
    [ReadOnly] public string prefab;
    [ReadOnly] public Bounds rendererBounds;

    private void Update()
    {
        if(rendererBounds.Intersects(Camera_ViewBounds.cameraViewBounds)) OnEnterCameraView();
    }

    private void OnEnterCameraView()
    {
        var prefabToSpawn = PrefabCollectionsSingletons.Instance.allPrefabs.GetPrefab(prefab);
        LeanPool.Spawn(prefabToSpawn, transform.position, transform.rotation, transform.parent);
        LeanPool.Despawn(gameObject);
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(rendererBounds.center, rendererBounds.size);
    }
    #endif
}
}