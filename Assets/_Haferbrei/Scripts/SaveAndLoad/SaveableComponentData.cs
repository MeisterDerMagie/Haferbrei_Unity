using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Haferbrei{
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
            object fieldValue = HaferbreiSerializationHandler.HandleSpecialSerialization(field.GetValue(_component));
            componentFields.Add(field.Name, fieldValue);
        }

        foreach (var property in saveableProperties)
        {
            object propertyValue = HaferbreiSerializationHandler.HandleSpecialSerialization(property.GetValue(_component));
            componentProperties.Add(property.Name, propertyValue);
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
                HaferbreiSerializationHandler.HandleSpecialDeserialization(fieldValue);

                _targetComponent.GetType()
                    .GetField(field.Key, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    ?.SetValue(_targetComponent, fieldValue);
            }

            foreach (KeyValuePair<string, object> field in componentProperties)
            {
                object fieldValue = field.Value;
                HaferbreiSerializationHandler.HandleSpecialDeserialization(fieldValue);

                _targetComponent.GetType()
                    .GetProperty(field.Key, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    ?.SetValue(_targetComponent, fieldValue);
            }
        }
    }
}
}