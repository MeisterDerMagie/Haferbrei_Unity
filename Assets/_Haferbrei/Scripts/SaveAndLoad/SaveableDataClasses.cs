using System;
using UnityEngine;

namespace Haferbrei{
[Serializable]
public abstract class SaveableObjectData //base class for all classes that can be saved
{
    public Guid guid; //each saveable instance has a unique ID
    public string saveableType;
}

public abstract class SaveableScriptableObjectData : SaveableObjectData
{
    public string scriptableObjectName;
    public string scriptableObjectType;
}
}