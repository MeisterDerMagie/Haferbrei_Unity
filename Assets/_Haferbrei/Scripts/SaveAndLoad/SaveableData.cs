using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Haferbrei{
[Serializable]
public struct SaveableData
{
    public string objectId;
    
    public Type componentType;
    public Dictionary<string, object> objectFields;
    public Dictionary<string, object> objectProperties;

    public SaveableData(object _objectToSave, string _objectID)
    {
        objectId = _objectID;
        componentType = _objectToSave.GetType();
        objectFields = new Dictionary<string, object>();
        objectProperties = new Dictionary<string, object>();
        
        var saveableFields = _objectToSave.GetType()
                                       .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                       .Where(f => f.GetCustomAttributes(typeof(SaveableAttribute)).Any());
        
        var saveableProperties = _objectToSave.GetType()
                                           .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                           .Where(f => f.GetCustomAttributes(typeof(SaveableAttribute)).Any());
        
        foreach(FieldInfo field in saveableFields)
        {
            object fieldValue = field.GetValue(_objectToSave);
            objectFields.Add(field.Name, fieldValue);
        }

        foreach (var property in saveableProperties)
        {
            object propertyValue = property.GetValue(_objectToSave);
            objectProperties.Add(property.Name, propertyValue);
        }
    }

    public void PopulateObject(object _objectToPopulate)
    {
        if (_objectToPopulate.GetType() != componentType) Debug.LogError($"Tried to load the save data of a different script. Tried to load type: {_objectToPopulate.GetType()} into: {componentType}");
        else
        {
            foreach (KeyValuePair<string, object> field in objectFields)
            {
                Debug.Log("FIELDNAME: " + field.Key);
                
                object fieldValue = field.Value;
                if (fieldValue is double) fieldValue = Convert.ToSingle(fieldValue);
                if (fieldValue is long)   fieldValue = Convert.ToInt32(fieldValue);

                _objectToPopulate.GetType()
                    .GetField(field.Key, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    ?.SetValue(_objectToPopulate, fieldValue);
            }

            foreach (KeyValuePair<string, object> field in objectProperties)
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