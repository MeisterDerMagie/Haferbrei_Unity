//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine;
using Doozy.Engine.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Haferbrei {
public class ButtonSaveSpielstand : MonoBehaviour
{
    [SerializeField, BoxGroup("References"), Required] private TMP_InputField inputField;

    public static Action overrideSaveFile = delegate {  };

    private void OnEnable() => overrideSaveFile += PopupCallback;
    private void OnDisable() => overrideSaveFile -= PopupCallback;

    public void ButtonClick()
    {
        //if file doesn't exist: save
        if (!SaveFiles.FileExists(inputField.text))
        {
            DoSave();
            return;
        }
        
        //if file exists: ask user if they want to overwrite it
        UIPopup popup = UIPopup.GetPopup("SpielstandWirklichUeberschreiben");
        popup.Show();
    }

    private void PopupCallback()
    {
        DoSave();
    }

    private void DoSave()
    {
        //save game
        SaveLoadControllerSingleton.Instance.SaveLoadController.SaveGameState(inputField.text);
        
        //return to game (hide save-window)
        GameEventMessage.SendEvent("portal_ToplevelMenus_toNoView");
    }
}
}