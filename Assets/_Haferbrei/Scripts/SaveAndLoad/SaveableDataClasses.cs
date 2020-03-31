using System;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei{
[Serializable]
public abstract class SaveableObjectData //base class for all classes that can be saved
{
    public Guid guid; //each saveable instance has a unique ID
    public string saveableType;
}

[Serializable]
public class SaveableScriptableObjectData : SaveableObjectData
{
    public string scriptableObjectName;
    public string scriptableObjectType;
    public SaveableData data;
}

[Serializable]
public class SaveFileData
{
    public List<SaveableObjectData> saveableDatas = new List<SaveableObjectData>();
}
}