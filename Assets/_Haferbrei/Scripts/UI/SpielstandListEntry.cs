//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using I2.Loc;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Haferbrei {
public class SpielstandListEntry : MonoBehaviour
{
    [SerializeField, BoxGroup("References"), Required] private GameObject previewCompatible, previewIncompatible, previewFileError, selection;
    [SerializeField, BoxGroup("References"), Required] private TextMeshProUGUI fileName, timeStamp, version;
    [SerializeField, BoxGroup("References"), Required] private Image previewImage;
    [SerializeField, BoxGroup("References")] private LocalizedString yesterday;

    public SaveFilePreview spielstandData;
    public SaveFileListUI uiController;
    
    public void SelectThis()
    {
        uiController.OnSelectedSpielstand(this);
    }

    public void LoadThis()
    {
        uiController.OnLoadSpielstand(this);
    }

    public void SetSelection(bool _isSelected)
    {
        selection.SetActive(_isSelected);
    }
    
    public void UpdatePreview(SaveFilePreview _previewData)
    {
        spielstandData = _previewData;
        
        switch (spielstandData.compatibility)
        {
            case SaveFilePreview.SaveFileCompatibility.IsCompatible:
                SetPreviewCompatible();
                break;
            case SaveFilePreview.SaveFileCompatibility.NotCompatible:
                SetPreviewIncompatible();
                break;
            case SaveFilePreview.SaveFileCompatibility.FileError:
                SetPreviewFileError();
                break;
        }
    }

    private void SetPreviewCompatible()
    {
        //activate & deactivate gameObjects
        previewCompatible.SetActive(true);
        previewIncompatible.SetActive(false);
        previewFileError.SetActive(false);
        
        //fill UI with values
        fileName.text = spielstandData.fileName;
        previewImage.sprite = spielstandData.previewImage;

        
        DateTime dateTime = spielstandData.headData.timeStamp;
        string dateTimeAsText;
        
        if (dateTime.Date == DateTime.Today) dateTimeAsText = dateTime.ToString("t");
        else if (dateTime.Date == DateTime.Today.AddDays(-1)) dateTimeAsText = yesterday + ", " + dateTime.ToString("t");
        else dateTimeAsText = dateTime.ToString("f");
        
        timeStamp.text = dateTimeAsText;
    }

    private void SetPreviewIncompatible()
    {
        //activate & deactivate gameObjects
        previewCompatible.SetActive(false);
        previewIncompatible.SetActive(true);
        previewFileError.SetActive(false);
        
        //fill UI with values
        fileName.text = spielstandData.fileName;
        version.text = spielstandData.version;
    }

    private void SetPreviewFileError()
    {
        //activate & deactivate gameObjects
        previewCompatible.SetActive(false);
        previewIncompatible.SetActive(false);
        previewFileError.SetActive(true);
        
        //fill UI with values
        fileName.text = spielstandData.fileName;
    }
}
}