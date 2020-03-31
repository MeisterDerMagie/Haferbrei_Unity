using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Haferbrei{
[Serializable]
public struct SaveableData
{
    public string Id;
    
    public Type type;
    public Dictionary<string, object> fields;
    public Dictionary<string, object> properties;
    
    public SaveableData(object _objectToSave, string _id)
    {
        Id = _id;
        type = _objectToSave.GetType();
        fields = new Dictionary<string, object>();
        properties = new Dictionary<string, object>();
        
        var saveableFields = _objectToSave.GetType()
                                       .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                       .Where(f => f.GetCustomAttributes(typeof(SaveableAttribute)).Any());
        
        var saveableProperties = _objectToSave.GetType()
                                           .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                           .Where(f => f.GetCustomAttributes(typeof(SaveableAttribute)).Any());
        
        foreach(FieldInfo field in saveableFields)
        {
            object fieldValue = field.GetValue(_objectToSave);
            fields.Add(field.Name, fieldValue);
        }

        foreach (var property in saveableProperties)
        {
            object propertyValue = property.GetValue(_objectToSave);
            properties.Add(property.Name, propertyValue);
        }
    }

    public void PopulateObject(object _objectToPopulate)
    {
        if (_objectToPopulate.GetType() != type) Debug.LogError($"Tried to load the save data of a different script. Tried to load type: {_objectToPopulate.GetType()} into: {type}");
        else
        {
            foreach (KeyValuePair<string, object> field in fields)
            {
                object fieldValue = field.Value;

                _objectToPopulate.GetType()
                    .GetField(field.Key, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    ?.SetValue(_objectToPopulate, fieldValue);
            }

            foreach (KeyValuePair<string, object> field in properties)
            {
                object fieldValue = field.Value;

                _objectToPopulate.GetType()
                    .GetProperty(field.Key, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    ?.SetValue(_objectToPopulate, fieldValue);
            }
        }
    }
}
}