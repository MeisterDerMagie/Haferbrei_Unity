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
    private SaveableGameObject AssociatedSaveableGameObject => GetAssociatedSaveableGameObject();

    public abstract SaveableComponentData StoreData();

    public abstract void RestoreData(SaveableComponentData _loadedData);

    private SaveableGameObject GetAssociatedSaveableGameObject()
    {
        var onOwnObject = GetComponent<SaveableGameObject>();
        var inParents = GetComponentsInParent<SaveableGameObject>(true);

        if (onOwnObject == null && inParents.Length == 0)
        {
            Debug.LogWarning("Achtung, jede SaveableComponent benötigt ein dazugehöriges SaveableObject! (Entweder auf dem selben Object oder im Parent)", this);
            componentID = "Error: Missing SaveableObject!";
            return null;
        }

        return (onOwnObject != null) ? onOwnObject : inParents[0];
    }
    
    public void OnDestroy()
    {
        if(AssociatedSaveableGameObject != null) AssociatedSaveableGameObject.RemoveSaveableComponent(this);
    }

    public void OnValidate()
    {
        if(AssociatedSaveableGameObject != null) AssociatedSaveableGameObject.AddSaveableComponent(this);
    }
}


[Serializable]
public abstract class SaveableComponentData
{
    public string componentID;
}
}