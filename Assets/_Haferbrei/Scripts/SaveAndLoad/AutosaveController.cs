using System;
using UnityEngine;

namespace Haferbrei {
public class AutosaveController : MonoBehaviour, IInitSelf
{
    public static int lastAutosaveIndex = 0;

    public static Action TriggerQuickSave;

    public void InitSelf() => TriggerQuickSave += DoAutoSave;
    private void OnDestroy() => TriggerQuickSave -= DoAutoSave;

    public void DoAutoSave()
    {
        //increase index
        if(lastAutosaveIndex++ > 9) lastAutosaveIndex = 0;
        PlayerPrefs.SetInt("lastAutosaveIndex", lastAutosaveIndex);
        PlayerPrefs.Save();
        
        //save file
        string saveGameFileName = "Autosave_" + lastAutosaveIndex.ToString();
        SaveLoadControllerSingleton.Instance.SaveLoadController.SaveGameState(saveGameFileName);
    }
}
}