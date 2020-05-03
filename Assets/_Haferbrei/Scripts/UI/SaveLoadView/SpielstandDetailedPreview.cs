//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Haferbrei {
public class SpielstandDetailedPreview : MonoBehaviour
{
    [SerializeField, BoxGroup("References")] private TextMeshProUGUI fileNameText, versionText, timeStampText, playtimeText;
    [SerializeField, BoxGroup("References")] private Image previewImage;
    [SerializeField, BoxGroup("References")] private LocalizedString yesterday;

    private SaveFilePreview spielstandData;

    public void UpdatePreview(SaveFilePreview _spielstandData)
    {
        spielstandData = _spielstandData;
        
        if(fileNameText != null) fileNameText.text = spielstandData.fileName;
        if(versionText != null) versionText.text = spielstandData.version;
        if (previewImage != null) previewImage.sprite = spielstandData.previewImage;

        if (playtimeText != null) playtimeText.text = spielstandData.headData.runPlaytime;
        if (timeStampText != null)
        {
            DateTime dateTime = spielstandData.headData.timeStamp;
            string dateTimeAsText;
            
            if (dateTime.Date == DateTime.Today) dateTimeAsText = dateTime.ToString("t");
            else if (dateTime.Date == DateTime.Today.AddDays(-1)) dateTimeAsText = yesterday + ", " + dateTime.ToString("t");
            else dateTimeAsText = dateTime.ToString("f");

            timeStampText.text = dateTimeAsText;
        }
    }
}
}