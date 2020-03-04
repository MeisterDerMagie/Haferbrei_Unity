using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei{
public class SaveGameLoader : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] public string saveGameFileName;
    [SerializeField, BoxGroup("References"), Required] private SaveLoadController saveLoadController;
    [SerializeField, BoxGroup("References"), Required] private SO_LoadScenes gameEnterScene; //diese Szene wird geladen, wenn man das Spiel aus dem Hauptmenü oder sonstwoher betritt. Dort werden alle benötigten Szenen geladen und in diesem Fall auch das Save Game geladen.
    
    public void LoadSaveGame()
    {
        if (string.IsNullOrEmpty(saveGameFileName))
        {
            Debug.LogError("Set the name of the save game file before loading!", gameObject);
            return;
        }
        
        saveLoadController.PrepareLoading(saveGameFileName);
        gameEnterScene.LoadScenes();
    }
}
}