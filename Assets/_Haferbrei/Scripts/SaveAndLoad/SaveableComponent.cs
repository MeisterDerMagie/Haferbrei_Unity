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
    [SerializeField, DisplayAsString] public string componentID;
    [SerializeField] protected Component componentToSave;
    
    //holt sich das dazugehörige SaveableObject (entweder auf demselben GameObject oder im nächsten Parent, das ein SaveableObject besitzt)
    private SaveableGameObject AssociatedSaveableGameObject => GetAssociatedSaveableGameObject();

    public SaveableComponentData StoreData() => new SaveableComponentData(componentToSave, componentID);

    public void RestoreData(SaveableComponentData _loadedData) => _loadedData.LoadIntoComponent(componentToSave);

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

    public virtual void OnValidate()
    {
        if(AssociatedSaveableGameObject != null) AssociatedSaveableGameObject.AddSaveableComponent(this);
    }
}


[Serializable]
public struct SaveableComponentData
{
    public string componentID;
    
    public Type componentType;
    public Dictionary<string, object> componentFields;
    public Dictionary<string, object> componentProperties;

    public SaveableComponentData(object _component, string _componentID)
    {
        componentID = _componentID;
        componentType = _component.GetType();
        componentFields = new Dictionary<string, object>();
        componentProperties = new Dictionary<string, object>();
        
        var saveableFields = _component.GetType()
                                       .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                       .Where(f => f.GetCustomAttributes(typeof(SaveableAttribute)).Any());
        
        var saveableProperties = _component.GetType()
                                           .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                           .Where(f => f.GetCustomAttributes(typeof(SaveableAttribute)).Any());
        
        foreach(FieldInfo field in saveableFields)
        {
            /*if (field.FieldType.IsSerializable)*/ componentFields.Add(field.Name, field.GetValue(_component));
        }

        foreach (var property in saveableProperties)
        {
            componentProperties.Add(property.Name, property.GetValue(_component));
        }
    }

    public void LoadIntoComponent(object _targetComponent)
    {
        if (_targetComponent.GetType() != componentType) Debug.LogError($"Tried to load the save data of a different script. Tried to load type: {_targetComponent.GetType()} into: {componentType}");
        else
        {
            foreach (KeyValuePair<string, object> field in componentFields)
            {
                Debug.Log("FIELDNAME: " + field.Key);
                
                object fieldValue = field.Value;
                if (field.Value is double) fieldValue = Convert.ToSingle(field.Value);
                if (field.Value is long) fieldValue = Convert.ToInt32(field.Value);

                _targetComponent.GetType()
                    .GetField(field.Key, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    ?.SetValue(_targetComponent, fieldValue);
            }

            foreach (KeyValuePair<string, object> field in componentProperties)
            {
                object fieldValue = field.Value;
                if (field.Value is double) fieldValue = Convert.ToSingle(field.Value);
                if (field.Value is long) fieldValue = Convert.ToInt32(field.Value);

                _targetComponent.GetType()
                    .GetProperty(field.Key, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    ?.SetValue(_targetComponent, fieldValue);
            }
        }
    }
}
}