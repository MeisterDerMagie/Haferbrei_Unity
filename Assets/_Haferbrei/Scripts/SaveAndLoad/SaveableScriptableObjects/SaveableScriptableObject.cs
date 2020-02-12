//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public abstract class SaveableScriptableObject : ScriptableObjectWithGuid
{
    private new void Awake()
    {
        base.Awake();
        SaveableScriptableObjectCollection.Instance.RegisterSaveableScriptableObject(this);
    }
    
    private void OnDestroy()
    {
        SaveableScriptableObjectCollection.Instance.UnregisterSaveableScriptableObject(this);
    }
}
}