//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Doozy.Engine;
using Doozy.Engine.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Haferbrei {
public class SaveFileListUI : MonoBehaviour, IInitSelf
{
    [SerializeField, BoxGroup("Settings"), Required] private List<string> ignoreFilenamesThatContain = new List<string>();
    [SerializeField, BoxGroup("Settings"), Required] bool autoSelectFirstSpielstand = true;

    [SerializeField, BoxGroup("Prefabs"), Required] private GameObject spielstandPrefab;
    [SerializeField, BoxGroup("Prefabs"), ReadOnly] private List<SpielstandListEntry> spielstande = new List<SpielstandListEntry>();
    
    [SerializeField, BoxGroup("References"), Required] private Transform parentForSpielstandPrefabs;
    [SerializeField, BoxGroup("References"), Required] private GameObject detailedPreviewCompatible, detailedPreviewNotCompatible, detailedPreviewFileError;

    public static Action onDeleteCurrentlySelectedSpielstand = delegate {  };
    
    private SpielstandListEntry currentlySelected;
    
    public void InitSelf()
    {
        SaveFiles.onLoadedList += UpdateUI;
        onDeleteCurrentlySelectedSpielstand += OnDeleteCurrentlySelectedSpielstand;
    }

    public void OnDestroy()
    {
        SaveFiles.onLoadedList -= UpdateUI;
        onDeleteCurrentlySelectedSpielstand -= OnDeleteCurrentlySelectedSpielstand;
    }

    public void OnSelectedSpielstand(SpielstandListEntry _selectedSpielstand)
    {
        Debug.Log("Spielstand ist null: " + (_selectedSpielstand == null));
        
        //update selection UI
        currentlySelected = _selectedSpielstand;
        DeselectAllSpielstande();
        _selectedSpielstand.SetSelection(true);
        
        //Update preview window
        switch (_selectedSpielstand.spielstandData.compatibility)
        {
            case SaveFilePreview.SaveFileCompatibility.IsCompatible:
                HideAllPreviewWindows();
                detailedPreviewCompatible.SetActive(true);

                detailedPreviewCompatible.GetComponent<SpielstandDetailedPreview>().UpdatePreview(_selectedSpielstand.spielstandData);
                break;
            case SaveFilePreview.SaveFileCompatibility.NotCompatible:
                HideAllPreviewWindows();
                detailedPreviewNotCompatible.SetActive(true);

                detailedPreviewNotCompatible.GetComponent<SpielstandDetailedPreview>().UpdatePreview(_selectedSpielstand.spielstandData);
                break;
            case SaveFilePreview.SaveFileCompatibility.FileError:
                HideAllPreviewWindows();
                detailedPreviewFileError.SetActive(true);
                
                detailedPreviewFileError.GetComponent<SpielstandDetailedPreview>().UpdatePreview(_selectedSpielstand.spielstandData);
                break;
        }
    }

    public void OnLoadSpielstand(SpielstandListEntry _selectedSpielstand)
    {
        //load game
        SaveLoadControllerSingleton.Instance.SaveLoadController.PrepareLoading(_selectedSpielstand.spielstandData.fileName);
        
        //return to game (hide load-window)
        GameEventMessage.SendEvent("portal_ToplevelMenus_toNoView");
    }

    public void LoadCurrentlySelectedSpielstand() //used by button
    {
        if(currentlySelected != null) OnLoadSpielstand(currentlySelected);
    }

    public void DeleteCurrentlySelectedSpielstand() //used by button
    {
        UIPopup popup = UIPopup.GetPopup("SpielstandWirklichLoeschen");
        popup.Show();
    }

    private void OnDeleteCurrentlySelectedSpielstand()
    {
        //File löschen
        if (currentlySelected == null) return;
        DeleteFile.Delete(currentlySelected.spielstandData.filePath);
        //Liste updaten
        SaveFiles.LoadList();
    }

    private void DeselectAllSpielstande()
    {
        foreach (var spielstand in spielstande)
        {
            spielstand.SetSelection(false);
        }
    }

    private void HideAllPreviewWindows()
    {
        detailedPreviewCompatible.SetActive(false);
        detailedPreviewNotCompatible.SetActive(false);
        detailedPreviewFileError.SetActive(false);
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
            bool ignoreThisFile = false;
            foreach (var stringPart in ignoreFilenamesThatContain)
            {
                if (!preview.fileName.Contains(stringPart)) continue;
                ignoreThisFile = true;
                break;
            }
            if(ignoreThisFile) continue;
            
            var listEntry = Instantiate(spielstandPrefab, Vector3.zero, Quaternion.identity, parentForSpielstandPrefabs).GetComponent<SpielstandListEntry>();
            listEntry.uiController = this;
            listEntry.UpdatePreview(preview);
            spielstande.Add(listEntry);
        }
        
        //select first entry
        if (!_saveFilePreviews.Any())
        {
            DeselectAllSpielstande();
            HideAllPreviewWindows();
            return;
        }
        if (autoSelectFirstSpielstand)
        {
            OnSelectedSpielstand(spielstande[0]);
        }
        else
        {
            DeselectAllSpielstande();
            HideAllPreviewWindows();
        }
    }
}
}