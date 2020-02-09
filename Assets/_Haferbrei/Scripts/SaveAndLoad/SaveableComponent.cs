//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Haferbrei {
[ExecuteInEditMode]
[HideMonoScript]
public class SaveableComponent : MonoBehaviour, IStoreable
{
    [SerializeField, DisplayAsString] public string componentID;
    
    //holt sich das dazugehörige SaveableObject (entweder auf demselben GameObject oder im nächsten Parent, das ein SaveableObject besitzt)
    private SaveableGameObject AssociatedSaveableGameObject => (GetComponent<SaveableGameObject>() != null)
        ? GetComponent<SaveableGameObject>()
        : GetComponentsInParent<SaveableGameObject>(true)[0];
    
    public virtual SaveableComponentData StoreData()
    {
        throw new NotImplementedException();
    }

    public virtual void RestoreData(SaveableComponentData _loadedData)
    {
        throw new NotImplementedException();
    }

    public void InitStoreable()
    {
        throw new NotImplementedException();
    }

    public void OnDestroy()
    {
        AssociatedSaveableGameObject.RemoveSaveableComponent(this);
    }

    private void OnValidate()
    {
        AssociatedSaveableGameObject.AddSaveableComponent(this);
    }
}


[Serializable]
public abstract class SaveableComponentData
{
    public string componentID;
}
}