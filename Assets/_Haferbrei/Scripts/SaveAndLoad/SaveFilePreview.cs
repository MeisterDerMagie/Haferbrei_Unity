using System;
using UnityEngine;

namespace Haferbrei{
[Serializable]
public class SaveFilePreview
{
    public enum SaveFileCompatibility
    {
        IsCompatible,
        NotCompatible,
        FileError
    }

    public SaveFileCompatibility compatibility;
    public string filePath;
    public string fileName;
    public string version;
    public Sprite previewImage;
    public SaveFile_HeadData headData;
}
}