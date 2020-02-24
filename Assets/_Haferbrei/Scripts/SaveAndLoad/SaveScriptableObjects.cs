using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei{
public class SaveScriptableObjects : MonoBehaviour
{
    //[InfoBox("Alle ScriptableObjects, die gespeichert werden sollen, müssen in dieser List referenziert werden.")]
    public List<ScriptableObject> scriptableObjectsToSave = new List<ScriptableObject>();
}
}