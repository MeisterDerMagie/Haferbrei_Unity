//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class ReflectOtherComponent : MonoBehaviour
{
    public Component componentToReflect;
    
    public List<FieldInfo> fields = new List<FieldInfo>();
    public List<PropertyInfo> properties = new List<PropertyInfo>();
    
    
    [Button]
    public void GetFieldValueByReflection()
    {
        var componentType = componentToReflect.GetType();
        Debug.Log("componentType: " + componentType.Name);

        var fieldsbla = componentType.GetFields().Where(f => f.GetCustomAttributes(typeof(SaveableAttribute)).Any()); //get all fields marked with SaveableAttribute

        foreach (var field in fieldsbla)
        {
            var fieldValue = field.GetValue(field);
            Debug.Log(field.Name + ": " + fieldValue.ToString());
        }
        
        //var field = componentType.GetField("floatTest");
        //var fieldValue = field.GetValue(field);
        //Debug.Log(field.Name + ": " + fieldValue);
    }
}
}