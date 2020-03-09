//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FullscreenEditor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class ReflectOtherComponent : SerializedMonoBehaviour
{
    public Component componentToReflect;
    public const BindingFlags FULL_BINDING = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    public List<SerializedField> serializedFields = new List<SerializedField>();

    [Button]
    public void GetFieldValueByReflection()
    {
        var componentType = componentToReflect.GetType();
        
        //get all public, private and static fields marked with SaveableAttribute
        var fields = componentType.GetFields(FULL_BINDING).Where(f => f.GetCustomAttributes(typeof(SaveableAttribute)).Any());
        
        serializedFields.Clear();
        foreach (var field in fields)
        {
            var fieldValue = field.GetValue(componentToReflect);
            var fieldType = field.FieldType.FullName;

            var type = typeof(SerializedField<>).MakeGenericType(field.FieldType);
            var serializedField = Activator.CreateInstance(type);
            (serializedField as ISerializedField)?.SetValues(fieldType, field.Name, fieldValue);
            
            
            Debug.Log($"Public field: {field.Name}, Value: {fieldValue}, Type: {fieldType}");
        }
    }
}

[Serializable]
public struct SaveableComponentTest
{
    public Type componentType;
    public Dictionary<string, object> componentVariables;

    public SaveableComponentTest(object _component)
    {
        componentType = _component.GetType();
        componentVariables = new Dictionary<string, object>();
        
        FieldInfo[] foundFields = _component.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach(FieldInfo field in foundFields)
        {
            if (field.FieldType.IsSerializable) componentVariables.Add(field.Name, field.GetValue(_component));
        }
    }
    
    public void LoadIntoComponent(object _targetComponent)
    {
        if (_targetComponent.GetType() != componentType) Debug.LogError("Tried to load the save data of a different script");
        else
        {
            foreach (KeyValuePair<string, object> field in componentVariables)
            {
                _targetComponent.GetType()
                    .GetField(field.Key, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    ?.SetValue(_targetComponent, field.Value);
            }
        }
    }
}
}