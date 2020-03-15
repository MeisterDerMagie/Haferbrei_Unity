//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using BindingFlags = System.Reflection.BindingFlags;

namespace Haferbrei {
[ExecuteInEditMode]
[HideMonoScript]
public class SaveableComponent : MonoBehaviour, IStoreable
{
    [InfoBox("Missing SaveableObject!", InfoMessageType.Error, "missingSaveableObject")]
    [SerializeField, DisplayAsString] public string componentID;
    [SerializeField] protected Component componentToSave;
    
    //holt sich das dazugehörige SaveableObject (entweder auf demselben GameObject oder im nächsten Parent, das ein SaveableObject besitzt)
    private SaveableGameObject AssociatedSaveableGameObject => GetAssociatedSaveableGameObject();
    private bool missingSaveableObject; //für Odin
    
    public SaveableData StoreData() => new SaveableData(componentToSave, componentID);

    public void RestoreData(SaveableData _loadedData) => _loadedData.LoadIntoComponent(componentToSave);

    private SaveableGameObject GetAssociatedSaveableGameObject()
    {
        var onOwnObject = GetComponent<SaveableGameObject>();
        var inParents = GetComponentsInParent<SaveableGameObject>(true);

        if (onOwnObject == null && inParents.Length == 0)
        {
            Debug.LogWarning("Achtung, jede SaveableComponent benötigt ein dazugehöriges SaveableObject! (Entweder auf dem selben Object oder im Parent)", this);
            missingSaveableObject = true;
            return null;
        }

        missingSaveableObject = false;
        return (onOwnObject != null) ? onOwnObject : inParents[0];
    }
    
    public void OnDestroy()
    {
        if(AssociatedSaveableGameObject != null) AssociatedSaveableGameObject.RemoveSaveableComponent(this);
    }

    public virtual void OnValidate()
    {
        if(AssociatedSaveableGameObject != null) AssociatedSaveableGameObject.AddSaveableComponent(this);
    }
}
}