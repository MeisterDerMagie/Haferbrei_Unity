//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Haferbrei {
public class SaveFileListUI : MonoBehaviour, IInitSelf
{
    [SerializeField, BoxGroup("Prefabs"), Required] private GameObject spielstandPrefab;
    [SerializeField, BoxGroup("Prefabs"), ReadOnly] private List<SpielstandListEntry> spielstande = new List<SpielstandListEntry>();
    
    [SerializeField, BoxGroup("References"), Required] private Transform parentForSpielstandPrefabs;
    [SerializeField, BoxGroup("References"), Required] private GameObject detailedPreviewCompatible, detailedPreviewNotCompatible, detailedPreviewFileError;

    public void InitSelf() => LoadSaveFileList.onLoadedList += UpdateUI;
    public void OnDestroy() => LoadSaveFileList.onLoadedList -= UpdateUI;

    public void OnSelectedSpielstand(SpielstandListEntry _selectedSpielstand)
    {
        //update selection UI
        foreach (var spielstand in spielstande)
        {
            spielstand.SetSelection(false);
        }
        _selectedSpielstand.SetSelection(true);
        
        //Update preview window
        switch (_selectedSpielstand.spielstandData.compatibility)
        {
            case SaveFilePreview.SaveFileCompatibility.IsCompatible:
                detailedPreviewCompatible.SetActive(true);
                detailedPreviewNotCompatible.SetActive(false);
                detailedPreviewFileError.SetActive(false);
                
                detailedPreviewCompatible.GetComponent<SpielstandDetailedPreview>().UpdatePreview(_selectedSpielstand.spielstandData);
                break;
            case SaveFilePreview.SaveFileCompatibility.NotCompatible:
                detailedPreviewCompatible.SetActive(false);
                detailedPreviewNotCompatible.SetActive(true);
                detailedPreviewFileError.SetActive(false);
                
                detailedPreviewNotCompatible.GetComponent<SpielstandDetailedPreview>().UpdatePreview(_selectedSpielstand.spielstandData);
                break;
            case SaveFilePreview.SaveFileCompatibility.FileError:
                detailedPreviewCompatible.SetActive(false);
                detailedPreviewNotCompatible.SetActive(false);
                detailedPreviewFileError.SetActive(true);
                
                detailedPreviewFileError.GetComponent<SpielstandDetailedPreview>().UpdatePreview(_selectedSpielstand.spielstandData);
                break;
        }
    }

    public void OnLoadSpielstand(SpielstandListEntry _selectedSpielstand)
    {
        SaveLoadControllerSingleton.Instance.SaveLoadController.PrepareLoading(_selectedSpielstand.spielstandData.fileName);
    }

    private void UpdateUI(List<SaveFilePreview> _saveFilePreviews)
    {
        //clear list
        for (int i = spielstande.Count-1; i >= 0 ; i--)
        {
            Destroy(spielstande[i].gameObject);
        }
        spielstande.Clear();
        
        //create new list
        foreach (var preview in _saveFilePreviews)
        {
            var listEntry = Instantiate(spielstandPrefab, Vector3.zero, Quaternion.identity, parentForSpielstandPrefabs).GetComponent<SpielstandListEntry>();
            listEntry.uiController = this;
            listEntry.UpdatePreview(preview);
            spielstande.Add(listEntry);
        }
        
        //select first entry
        if (!_saveFilePreviews.Any()) return;
        OnSelectedSpielstand(spielstande[0]);
    }
}
}