//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[HideMonoScript]
public abstract class ModelDistributor<T> : MonoBehaviour where T : IModel
{
    [SerializeField][Saveable] private T model;
    [SerializeField, ReadOnly] private List<Component> receivers = new List<Component>();
    
    public void DistributeModel(T _model)
    {
        model = _model;

        //hier bekommen die Receiver das Model
        foreach (var receiver in receivers)
        {
            if (receiver == null) continue;
            (receiver as IModelReceiver<T>).SetModel(model);
        }
    }
    
    //--- Fetch and cache Receivers ---
    #if UNITY_EDITOR
    private void OnValidate()
    {
        receivers.Clear();
        FetchReceiversInChildren(transform);
        DistributeModel(model);
    }

    private void FetchReceiversInChildren(Transform _root) //geht rekursiv alle children durch und holt sich dort die Receiver. Geht nicht in Children rein, die selbst einen gleichen ModelDistributor besitzen
    {
        Component[] components = _root.GetComponents<Component>();
        foreach (var component in components)
        {
            if(component is IModelReceiver<T>) receivers.Add(component);
        }

        foreach (Transform child in _root)
        {
            if ((child.GetComponent<ModelDistributor<T>>() != null) && (child != _root)) continue; //skip Children with ModelDistributors of the same type
            FetchReceiversInChildren(child);
        }
    }
    #endif
    //--- ---
}
}