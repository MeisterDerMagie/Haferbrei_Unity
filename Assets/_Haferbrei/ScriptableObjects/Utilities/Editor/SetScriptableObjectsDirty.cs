//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "SetAllScriptableObjectsDirty", menuName = "ScriptableObjects/Utilities/SetAllScriptableObjectsDirty", order = 0)]
public class SetScriptableObjectsDirty : ScriptableObjectWithGuid
{
    public List<ScriptableObject> scriptableObjectsToSetDirty;

    [Button]
    public new void SetDirty()
    {
        foreach (var so in scriptableObjectsToSetDirty)
        {
            Debug.Log("Set dirty: " + so);
        }
    }
}
}