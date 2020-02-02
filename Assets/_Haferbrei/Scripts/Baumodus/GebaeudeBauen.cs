//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;
using Wichtel.Extensions;

namespace Haferbrei {
public class GebaeudeBauen : MonoBehaviour
{
    [SerializeField, BoxGroup("Info"), ReadOnly] private GameObject gebaeudePreview;
    [SerializeField, FoldoutGroup("References"), Required] private Transform previewParent;
    [SerializeField, BoxGroup("Atom Events"), Required] private BuildingsEvent onZuBauendesGebaeudeChanged;

    private void OnEnable() => onZuBauendesGebaeudeChanged.Register(OnZuBauendesGebaeudeChanged);
    private void OnDisable() => onZuBauendesGebaeudeChanged.Unregister(OnZuBauendesGebaeudeChanged);


    private void OnZuBauendesGebaeudeChanged(Buildings _newBuilding)
    {
        if(gebaeudePreview != null) Destroy(gebaeudePreview);

        if (_newBuilding.previewPrefab != null)
        {
            gebaeudePreview = Instantiate(   _newBuilding.previewPrefab,
                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).With(z: previewParent.transform.position.z),
                                            Quaternion.identity,
                                            previewParent);
        }
    }
}
}