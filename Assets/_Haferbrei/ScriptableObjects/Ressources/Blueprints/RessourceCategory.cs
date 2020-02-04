//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "RessourceCategory", menuName = "Scriptable Objects/Ressourcen/Ressource Category", order = 0)]
public class RessourceCategory : ScriptableObject
{
    public string identifier;
}
}