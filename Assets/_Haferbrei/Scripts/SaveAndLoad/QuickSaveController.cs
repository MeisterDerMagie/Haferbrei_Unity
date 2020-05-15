using System;
using UnityEngine;

namespace Haferbrei{
public class QuickSaveController : MonoBehaviour
{
    [Min(1)]
    public int quicksaveSlots = 1;
    
    public static int lastQuicksaveIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5)) DoQuickSave();
        #if !UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.F9)) DoQuickLoad();
        #else
        if(Input.GetKeyDown(KeyCode.F6)) DoQuickLoad();
        #endif
    }

    public void DoQuickSave()
    {
        //increase index
        if(lastQuicksaveIndex++ > (quicksaveSlots-2)) lastQuicksaveIndex = 0;
        PlayerPrefs.SetInt("lastQuicksaveIndex", lastQuicksaveIndex);
        PlayerPrefs.Save();
        
        //save file
        string saveGameFileName = "Quicksave_" + lastQuicksaveIndex.ToString();
        SaveLoadControllerSingleton.Instance.SaveLoadController.SaveGameState(saveGameFileName);
    }

    public void DoQuickLoad()
    {
        SaveLoadControllerSingleton.Instance.SaveLoadController.PrepareLoading("Quicksave_" + lastQuicksaveIndex.ToString());
    }
}
}