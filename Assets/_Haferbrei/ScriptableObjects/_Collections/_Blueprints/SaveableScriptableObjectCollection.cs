//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "SaveableScriptableObjectCollection", menuName = "Scriptable Objects/Collections/SaveableScriptableObject", order = 0)]
public class SaveableScriptableObjectCollection : ScriptableObjectCollectionGeneric<SaveableScriptableObject>
{
    //--- Singleton ---
    static SaveableScriptableObjectCollection _instance = null;
    public static SaveableScriptableObjectCollection Instance
    {
        get
        {
            if (!_instance)
                _instance = Resources.FindObjectsOfTypeAll<SaveableScriptableObjectCollection>().FirstOrDefault();
            return _instance;
        }
    }
    //--- ---
}
}