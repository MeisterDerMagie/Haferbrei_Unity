using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Haferbrei{
public class LoadSaveFileList : MonoBehaviour
{
    public List<SaveFilePreview> loadedHeadDatas;
    
    public static Action<List<SaveFilePreview>> onLoadedList;

    void OnApplicationFocus(bool hasFocus)
    {
        if(hasFocus) LoadList();
    }

    private void OnEnable() => LoadList();

    public void LoadList()
    {
        loadedHeadDatas.Clear();
        
        string folderPath    = SaveLoadControllerSingleton.Instance.SaveLoadController.saveGameDirectoryPath;
        string fileExtension = SaveLoadControllerSingleton.Instance.SaveLoadController.saveGameFileExtension;
        bool encryptSaveFile = SaveLoadControllerSingleton.Instance.SaveLoadController.encryptSaveFile;
        
        string[] files = encryptSaveFile ? System.IO.Directory.GetFiles(folderPath, "*" + fileExtension) : System.IO.Directory.GetFiles(folderPath, "*.json");

        foreach (var filePath in files)
        {
            var previewData = new SaveFilePreview();
            previewData.filePath = filePath;
            previewData.fileName = Path.GetFileNameWithoutExtension(filePath);
            
            try
            {
                //try to read the file as text
                string fileContent = System.IO.File.ReadAllText(filePath); // read file from disk

                //try to deserialize the version
                string version= "";
                SaveFileSerializer.Deserialize("Version", fileContent, encryptSaveFile, ref version);

                previewData.version = version;
                //check if the version is compatible
                if (!HaferbreiVersion.IsCompatible(version))
                {
                    previewData.compatibility = SaveFilePreview.SaveFileCompatibility.NotCompatible;
                    loadedHeadDatas.Add(previewData);
                    continue;
                }
                
                //try to load the head data
                var headData = new SaveFile_HeadData();
                SaveFileSerializer.Deserialize("Head", fileContent, encryptSaveFile, ref headData);
                previewData.headData = headData;

                previewData.previewImage = Sprite.Create( headData.screenshot,
                                                          new Rect(0, 0, headData.screenshot.width, headData.screenshot.height),
                                                          new Vector2(0.5f, 0.5f));
                
                loadedHeadDatas.Add(previewData);
            }
            catch (Exception exception)
            {
                Debug.LogWarning("Could not read file: " + filePath + " \n" + exception);
                previewData.compatibility = SaveFilePreview.SaveFileCompatibility.FileError;
                loadedHeadDatas.Add(previewData);
                continue;
            }
        }
        
        onLoadedList?.Invoke(loadedHeadDatas);
    }
}
}