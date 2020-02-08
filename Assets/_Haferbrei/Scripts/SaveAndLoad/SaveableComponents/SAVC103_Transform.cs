using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Haferbrei{

public class SAVC103_Transform : MonoBehaviour, ISaveable
{
    [SerializeField, ReadOnly, HideInInspector]
    private RectTransform rectTransform;

    private void OnEnable()
    {
        GetRectTransform();
    }

    private void GetRectTransform()
    {
        if(GetComponent<RectTransform>() != null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }

    public SaveableComponentData StoreData()
    {
        SAVC103_TransformData _data = new SAVC103_TransformData();

        _data.ID         = ID;

        //Store TransformData
        _data.position   = transform.position;
        _data.rotation   = transform.rotation;
        _data.localScale = transform.localScale;

        //Store RectTransformData
        if(rectTransform != null)
        {
            _data.anchoredPosition   = rectTransform.anchoredPosition;
            _data.anchoredPosition3D = rectTransform.anchoredPosition3D;
            _data.anchorMax          = rectTransform.anchorMax;
            _data.anchorMin          = rectTransform.anchorMin;
            _data.offsetMax          = rectTransform.offsetMax;
            _data.offsetMin          = rectTransform.offsetMin;
            _data.pivot              = rectTransform.pivot;
            _data.sizeDelta          = rectTransform.sizeDelta;
        }

        return _data;
    }

    public override void RestoreData(SAL003_SaveableComponentData _loadedData)
    {
        
    }

    public SaveableData SaveData()
    {
        throw new System.NotImplementedException();
    }

    public void LoadData()
    {
        SAVC103_TransformData _data = _loadedData as SAVC103_TransformData; 

        //Restore TransformData
        transform.position   = _data.position;
        transform.rotation   = _data.rotation;
        transform.localScale = _data.localScale;

        //Restore RectTransformData
        if(rectTransform != null)
        {
            rectTransform.anchoredPosition   = _data.anchoredPosition;
            rectTransform.anchoredPosition3D = _data.anchoredPosition3D;
            rectTransform.anchorMax          = _data.anchorMax;
            rectTransform.anchorMin          = _data.anchorMin;
            rectTransform.offsetMax          = _data.offsetMax;
            rectTransform.offsetMin          = _data.offsetMin;
            rectTransform.pivot              = _data.pivot;
            rectTransform.sizeDelta          = _data.sizeDelta;
        }
    }

    public void InitSaveable()
    {
        throw new System.NotImplementedException();
    }

    public void OnDestroy()
    {
        throw new System.NotImplementedException();
    }
}

public class SAVC103_TransformData : SaveableComponentData
{
    //TransformData
    public Vector3 position;
    public Quaternion rotation;
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