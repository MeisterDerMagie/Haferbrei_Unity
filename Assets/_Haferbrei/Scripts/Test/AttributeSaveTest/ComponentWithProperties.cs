using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei{
public class ComponentWithProperties : MonoBehaviour
{
    [Saveable, SerializeField] private float privateFloatWithAttribute;
    [Saveable, SerializeField] private string saveableString;
    public float floatWithoutAttribute;
    [Saveable] public Vector3 vector3Test;
    [Saveable] public Ressource ressourceTest;
    [Saveable] public RessourceContainer container;
    
    [Saveable] public float propertyTest { get; set; }

    private void OnEnable()
    {
        container = ScriptableObject.CreateInstance<RessourceContainer>();
    }
}

[AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
public class SaveableAttribute : Attribute
{
    
}
}