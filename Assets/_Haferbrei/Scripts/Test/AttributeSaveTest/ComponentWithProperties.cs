using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei{
public class ComponentWithProperties : MonoBehaviour
{
    [Saveable] public float floatWithAttribute;
    public float floatWithoutAttribute;
    [Saveable] public Vector3 vector3Test;
    [Saveable] public Ressource ressourceTest;
    
    [Saveable] public float propertyTest { get; set; }
}

[AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
public class SaveableAttribute : Attribute
{
    
}
}