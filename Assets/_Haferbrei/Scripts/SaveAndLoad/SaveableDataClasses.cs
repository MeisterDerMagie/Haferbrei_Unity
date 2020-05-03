using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei{
[Serializable]
public abstract class SaveableObjectData //base class for all classes that can be saved
{
    public Guid guid; //each saveable instance has a unique ID
}

[Serializable]
public class SaveableScriptableObjectData : SaveableObjectData
{
    public string scriptableObjectName;
    public string scriptableObjectType;
    public SaveableData data;
}

[Serializable]
public class SaveFile_BodyData
{
    public List<SaveableScriptableObjectData> scriptableObjectDatas = new List<SaveableScriptableObjectData>();
    public List<SaveablePrefabData> prefabDatas = new List<SaveablePrefabData>();
    public List<SaveableData> componentDatas = new List<SaveableData>();
}

[Serializable]
public class SaveFile_HeadData
{
    public DateTime timeStamp;
    public string runPlaytime;
    public int screenshotWidth;
    public int screenshotHeight;
    [PreviewField] public Texture2D screenshot;

    public SaveFile_HeadData(DateTime _timeStamp, Texture2D _screenshot)
    {
        timeStamp = _timeStamp;
        runPlaytime = Playtime.Run.ToString(@"hh\:mm");
        screenshot = _screenshot;
        screenshotWidth = _screenshot.width;
        screenshotHeight = _screenshot.height;
    }
    public SaveFile_HeadData(){}
}
}