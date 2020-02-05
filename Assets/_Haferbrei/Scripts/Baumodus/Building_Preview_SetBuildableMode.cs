//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class Building_Preview_SetBuildableMode : MonoBehaviour
{
    [SerializeField, ReadOnly] private bool buildable;

    public SpriteRenderer buildingPreview;
    public Color colorBuildingAllowed, colorBuildingDisallowed;

    private void OnEnable() => OnBuildableChanged();

    
    public void SetBuildableMode(bool _buildable)
    {
        if(buildable == _buildable) return;

        buildable = _buildable;
        OnBuildableChanged();
    }

    private void OnBuildableChanged()
    {
        buildingPreview.color = (buildable) ? colorBuildingAllowed : colorBuildingDisallowed;
    }
}
}