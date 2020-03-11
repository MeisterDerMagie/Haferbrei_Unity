//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
public class ScriptableObjectReferences : MonoBehaviour
{
    //--- Singleton Behaviour ---
    #region Singleton
    private static ScriptableObjectReferences instance_;
    public static ScriptableObjectReferences Instance
        => instance_ == null ? FindObjectOfType<ScriptableObjectReferences>() : instance_;

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

    public SaveableScriptableObjects saveableScriptableObjects;
}
}