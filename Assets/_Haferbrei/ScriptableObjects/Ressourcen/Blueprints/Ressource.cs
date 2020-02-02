//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "Ressource", menuName = "Scriptable Objects/Ressourcen/Ressource", order = 0)]
public class Ressource : ScriptableObject
{
    public string identifier;
    public string description;
    public float goldwert;
}
}