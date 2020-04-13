//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
public class LoadPlayerPrefs : MonoBehaviour
{
    private void Awake()
    {
        AutosaveController.lastAutosaveIndex   = PlayerPrefs.GetInt("lastAutosaveIndex");
        QuickSaveController.lastQuicksaveIndex = PlayerPrefs.GetInt("lastQuicksaveIndex");
    }
}
}