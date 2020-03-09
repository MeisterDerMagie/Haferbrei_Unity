//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bayat.SaveSystem;
using FullscreenEditor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class SaveOtherComponent : SerializedMonoBehaviour
{
    public Component componentToSave;

    [Button]
    public void Save()
    {
        var saveData = new SaveableComponentTest(componentToSave);
        
        SaveSystemSettings settings = SaveSystemSettings.DefaultSettings.Clone();
        SaveSystemAPI.SaveAsync("ReflectionTest.hsave", saveData, settings);
    }

    [Button]
    public void Load()
    {
        var loadedData = new SaveableComponentTest(componentToSave);
        SaveSystemAPI.LoadIntoAsync<List<SaveableData>>("ReflectionTest.hsave", loadedData);
        
        loadedData.LoadIntoComponent(componentToSave);
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
        
        var saveableFields = _component.GetType().
                                        GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                        .Where(f => f.GetCustomAttributes(typeof(SaveableAttribute)).Any());
        
        foreach(FieldInfo field in saveableFields)
        {
            /*if (field.FieldType.IsSerializable)*/ componentVariables.Add(field.Name, field.GetValue(_component));
        }
    }
    
    public void LoadIntoComponent(object _targetComponent)
    {
        if (_targetComponent.GetType() != componentType) Debug.LogError($"Tried to load the save data of a different script. Tried to load type: {_targetComponent.GetType()} into: {componentType}");
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