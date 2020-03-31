//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
public class DummyScriptableObjectReference : MonoBehaviour
{
    public DummyScriptableObject dummyScriptableObject;
    
    //--- Singleton Behaviour ---
    #region Singleton
    private static DummyScriptableObjectReference instance_;
    public static DummyScriptableObjectReference Instance
        => instance_ == null ? FindObjectOfType<DummyScriptableObjectReference>() : instance_;

    public void Awake()
    {
        if (instance_ == null)
        {
            instance_ = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
    #endregion
    //--- ---
}
}