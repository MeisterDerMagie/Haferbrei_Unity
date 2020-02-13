using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei{
public class Transform_SaveableComponent : SaveableComponent
{ 
    private RectTransform RectTransform => GetComponent<RectTransform>();

    public override SaveableComponentData StoreData()
    {
        Transform_SaveableComponentData saveableComponentData = new Transform_SaveableComponentData();

        saveableComponentData.componentID = componentID;

        //Store TransformData
        saveableComponentData.localPosition = transform.localPosition;
        saveableComponentData.localRotation = transform.localRotation;
        saveableComponentData.localScale    = transform.localScale;

        if (RectTransform == null) return saveableComponentData;
        
        //Store RectTransformData
        saveableComponentData.anchoredPosition   = RectTransform.anchoredPosition;
        saveableComponentData.anchoredPosition3D = RectTransform.anchoredPosition3D;
        saveableComponentData.anchorMax          = RectTransform.anchorMax;
        saveableComponentData.anchorMin          = RectTransform.anchorMin;
        saveableComponentData.offsetMax          = RectTransform.offsetMax;
        saveableComponentData.offsetMin          = RectTransform.offsetMin;
        saveableComponentData.pivot              = RectTransform.pivot;
        saveableComponentData.sizeDelta          = RectTransform.sizeDelta;

        return saveableComponentData;
    }

    public override void RestoreData(SaveableComponentData _loadedData)
    {
        Transform_SaveableComponentData saveableComponentData = _loadedData as Transform_SaveableComponentData; 

        //Restore TransformData
        transform.localPosition   = saveableComponentData.localPosition;
        transform.localRotation   = saveableComponentData.localRotation;
        transform.localScale      = saveableComponentData.localScale;

        if (RectTransform == null) return;
        
        //Restore RectTransformData
        RectTransform.anchoredPosition   = saveableComponentData.anchoredPosition;
        RectTransform.anchoredPosition3D = saveableComponentData.anchoredPosition3D;
        RectTransform.anchorMax          = saveableComponentData.anchorMax;
        RectTransform.anchorMin          = saveableComponentData.anchorMin;
        RectTransform.offsetMax          = saveableComponentData.offsetMax;
        RectTransform.offsetMin          = saveableComponentData.offsetMin;
        RectTransform.pivot              = saveableComponentData.pivot;
        RectTransform.sizeDelta          = saveableComponentData.sizeDelta;
    }
}

public class Transform_SaveableComponentData : SaveableComponentData
{
    //TransformData
    public Vector3 localPosition;
    public Quaternion localRotation;
    public Vector3 localScale;

    //RectTransformData    
    public Vector2 anchoredPosition;
    public Vector3 anchoredPosition3D;
    public Vector2 anchorMax;
    public Vector2 anchorMin;
    public Vector2 offsetMax;
    public Vector2 offsetMin;
    public Vector2 pivot;
    public Vector2 sizeDelta;
}
}