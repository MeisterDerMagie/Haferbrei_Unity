//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "Ressource", menuName = "Scriptable Objects/Ressourcen/Ressource", order = 0)]
public class Ressource : SerializedScriptableObject
{
    public string identifier;
    public float goldwert;
    public RessourceCategory category;
}
}