using System;
using UnityEngine;

namespace Haferbrei{
[Serializable]
public abstract class SaveableData //base class for all classes that can be saved
{
    public Guid guid; //each saveable instance has a unique ID
    public string saveableType;
}

public abstract class SaveableSOData : SaveableData
{
    public string scriptableObjectName;
    public string scriptableObjectType;
}
}