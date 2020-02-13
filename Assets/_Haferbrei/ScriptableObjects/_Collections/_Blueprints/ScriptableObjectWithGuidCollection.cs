//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "ScriptableObjectCollection", menuName = "Scriptable Objects/Collections/ScriptableObjectWithGuid Collection", order = 0)]
public class ScriptableObjectWithGuidCollection : ScriptableObjectCollectionGeneric<ScriptableObjectWithGuid>
{
    //--- Singleton ---
    static ScriptableObjectWithGuidCollection _instance = null;
    public static ScriptableObjectWithGuidCollection Instance
    {
        get
        {
            if (!_instance)
                _instance = Resources.FindObjectsOfTypeAll<ScriptableObjectWithGuidCollection>().FirstOrDefault();
            return _instance;
        }
    }
    //--- ---
}
}