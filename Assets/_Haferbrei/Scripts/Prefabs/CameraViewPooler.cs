//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel.Extensions;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
using Wappen.Editor;
#endif

namespace Haferbrei {
public class CameraViewPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefabRepresentative;
    [SerializeField, ReadOnly] private string prefab;
    [SerializeField, ReadOnly] private Bounds rendererBounds;
    [SerializeField] private float safetyMargin = 3f;

    private void OnEnable()
    {
        rendererBounds.center = transform.position;
        Timing.RunCoroutine(_CheckIfObjectLeftCameraView());
    }
    
    private IEnumerator<float> _CheckIfObjectLeftCameraView()
    {
        yield return Timing.WaitForSeconds(2f);
        while (rendererBounds.Intersects(Camera_ViewBounds.cameraViewBounds))
        {
            yield return Timing.WaitForSeconds(Random.Range(1.8f, 2.2f));
        }
        
        OnLeaveCameraView();
    }

    private void OnLeaveCameraView()
    {
        //Spawn Representative
        //var representative = LeanPool.Spawn(prefabRepresentative, transform.position, transform.rotation, transform.parent);
        var representative = Instantiate(prefabRepresentative, transform.position, transform.rotation, transform.parent);
        representative.name = $"PrfRep({gameObject.name})";
        var representativeController = representative.GetComponent<PrefabRepresentative>();
        representativeController.prefab = prefab;
        representativeController.rendererBounds = rendererBounds;
        
        LeanPool.Despawn(gameObject);
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        //-- check if this script sits on a prefab root --
        var prefabProperties = PrefabHelper.GetPrefabProperties(gameObject);
        if (!prefabProperties.isRootOfAnyPrefab)
        {
            Debug.LogError("CameraViewPooler only works on prefab root objects!", this);
            return;
        }
        //-- --

        if(!prefabProperties.isSceneObject) prefab = gameObject.name;
        rendererBounds = BoundsExtensions.CalculateBoundsInChildren(transform);
        rendererBounds.Expand(safetyMargin);
    }
    #endif
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(rendererBounds.center, rendererBounds.size);
    }
    #endif
}
}