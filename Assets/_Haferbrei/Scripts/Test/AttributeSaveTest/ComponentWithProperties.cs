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
    [Saveable, InlineEditor] public RessourceContainer container;
    [Saveable] public GameObject gameObjectReference;

    [Saveable] public Transform ownTransform;
    
    //[Saveable] public Vector3 propertyBridge
    //{
    //    get => transform.position;
    //    set => transform.position = value;
    //}

    
    
    private void OnEnable()
    {
        container = ScriptableObject.CreateInstance<RessourceContainer>();
    }
}


}