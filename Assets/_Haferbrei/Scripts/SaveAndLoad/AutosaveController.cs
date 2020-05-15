using System;
using UnityEngine;

namespace Haferbrei {
public class AutosaveController : MonoBehaviour, IInitSelf
{
    [Min(1)]
    public int autosaveSlots = 1;
    
    public static int lastAutosaveIndex = 0;

    public static Action TriggerAutoSave;

    public void InitSelf() => TriggerAutoSave += DoAutoSave;
    private void OnDestroy() => TriggerAutoSave -= DoAutoSave;

    public void DoAutoSave()
    {
        //increase index
        if(lastAutosaveIndex++ > (autosaveSlots - 2)) lastAutosaveIndex = 0;
        PlayerPrefs.SetInt("lastAutosaveIndex", lastAutosaveIndex);
        PlayerPrefs.Save();
        
        //save file
        string saveGameFileName = "Autosave_" + lastAutosaveIndex.ToString();
        SaveLoadControllerSingleton.Instance.SaveLoadController.SaveGameState(saveGameFileName);
    }
}
}