//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "DummyScriptableObject", menuName = "Scriptable Objects/DummyScriptableObject", order = 0)]
public class DummyScriptableObject : ScriptableObject
{
    [DisplayAsString]
    public string info = "Dieses ScriptableObject wird beim Speichern und Laden verwendet.";
}
}