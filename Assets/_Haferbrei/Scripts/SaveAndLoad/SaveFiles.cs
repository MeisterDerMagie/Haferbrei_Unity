using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei{
public class SaveFiles : MonoBehaviour
{
    private static Dictionary<string/*Hash + Filename*/, SaveFilePreview> loadedHeadDatas = new Dictionary<string, SaveFilePreview>();
    
    public static Action<List<SaveFilePreview>> onLoadedList;

    void OnApplicationFocus(bool hasFocus)
    {
        if(hasFocus) LoadList();
    }

    public static bool FileExists(string _fileName)
    {
        foreach (var headData in loadedHeadDatas)
        {
            if (headData.Value.fileName == _fileName) return true;
        }
        return false;
    }

    private void OnEnable() => LoadList();

    public static void LoadList()
    {
        if(loadedHeadDatas == null) loadedHeadDatas = new Dictionary<string, SaveFilePreview>();

        string folderPath    = SaveLoadControllerSingleton.Instance.SaveLoadController.saveGameDirectoryPath;
        string fileExtension = SaveLoadControllerSingleton.Instance.SaveLoadController.saveGameFileExtension;
        bool encryptSaveFile = SaveLoadControllerSingleton.Instance.SaveLoadController.encryptSaveFile;
        
        string[] files = encryptSaveFile ? System.IO.Directory.GetFiles(folderPath, "*" + fileExtension) : System.IO.Directory.GetFiles(folderPath, "*.json");
        
        //remove deleted files from the cached data
        var fileIdentifiers = new List<string>();
        var entriesToRemove = new List<string>();
        foreach (var filePath in files) //get file identifiers (hash + filename) for all files
        {
            string fileIdentifier = FileHash.GetHashSha256AsString(filePath) + Path.GetFileNameWithoutExtension(filePath);
            fileIdentifiers.Add(fileIdentifier);
        }
        
        foreach (var fileIdentifier in loadedHeadDatas.Keys.ToList())
        {
            if(!fileIdentifiers.Contains(fileIdentifier)) entriesToRemove.Add(fileIdentifier);
        }
        foreach (var entry in entriesToRemove) loadedHeadDatas.Remove(entry);

        //-- load head data --
        foreach (var filePath in files)
        {
            //ignore files that have already been loaded and cached before
            string fileHash = FileHash.GetHashSha256AsString(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string fileIdentifier = fileHash + fileName;
            if (loadedHeadDatas.ContainsKey(fileIdentifier)) continue;

            //if not already loaded and cached: load the head data
            var previewData = new SaveFilePreview();
            previewData.filePath = filePath;
            previewData.fileName = fileName;
            
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
                    loadedHeadDatas.Add(fileIdentifier, previewData);
                    continue;
                }
                
                //try to load the head data
                var headData = new SaveFile_HeadData();
                SaveFileSerializer.Deserialize("Head", fileContent, encryptSaveFile, ref headData);
                previewData.headData = headData;

                previewData.previewImage = Sprite.Create( headData.screenshot,
                                                          new Rect(0, 0, headData.screenshot.width, headData.screenshot.height),
                                                          new Vector2(0.5f, 0.5f));
                
                loadedHeadDatas.Add(fileIdentifier, previewData);
            }
            catch (Exception exception)
            {
                Debug.LogWarning("Could not read file: " + filePath + " \n" + exception);
                previewData.compatibility = SaveFilePreview.SaveFileCompatibility.FileError;
                loadedHeadDatas.Add(fileIdentifier, previewData);
                continue;
            }
        }
        
        onLoadedList?.Invoke(loadedHeadDatas.Values.ToList());
    }
}
}