//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Haferbrei {

[CreateAssetMenu(fileName = "RessourceRecipe", menuName = "Scriptable Objects/Ressourcen/Recipe", order = 0)]
public class RessourceRecipe : SerializedScriptableObject
{
    public string identifier;
    [OdinSerialize] public Dictionary<Ressource, int> recipe = new Dictionary<Ressource, int>();
}
}