using System;
using UnityEngine;

namespace Haferbrei{
public class QuickSaveController : MonoBehaviour
{
    public static int lastQuicksaveIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5)) DoQuickSave();
        if(Input.GetKeyDown(KeyCode.F6)) DoQuickLoad();
    }

    public void DoQuickSave()
    {
        //increase index
        if(lastQuicksaveIndex++ > 15) lastQuicksaveIndex = 0;
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