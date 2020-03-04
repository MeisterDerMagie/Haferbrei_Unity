//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[ExecuteInEditMode]
[HideMonoScript]
public abstract class SaveableComponent : MonoBehaviour, IStoreable
{
    [SerializeField, DisplayAsString] public string componentID;
    
    //holt sich das dazugehörige SaveableObject (entweder auf demselben GameObject oder im nächsten Parent, das ein SaveableObject besitzt)
    private SaveableGameObject AssociatedSaveableGameObject => (GetComponent<SaveableGameObject>() != null)
        ? GetComponent<SaveableGameObject>()
        : GetComponentsInParent<SaveableGameObject>(true)[0];

    public abstract SaveableComponentData StoreData();

    public abstract void RestoreData(SaveableComponentData _loadedData);
    
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