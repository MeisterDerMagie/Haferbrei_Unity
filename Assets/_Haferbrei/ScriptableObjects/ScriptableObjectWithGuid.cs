//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public abstract class ScriptableObjectWithGuid : SerializedScriptableObject
{
    [ReadOnly, DisplayAsString] public Guid guid;

    public void Awake()
    {
        if(guid == Guid.Empty) guid = Guid.NewGuid();
    }
}
}