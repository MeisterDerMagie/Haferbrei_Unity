//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "RessourceCategory", menuName = "Scriptable Objects/Ressourcen/Ressource Category", order = 0)]
public class RessourceCategory : SerializedScriptableObject
{
    public string identifier;
}
}