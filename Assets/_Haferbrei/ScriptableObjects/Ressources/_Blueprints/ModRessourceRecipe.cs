﻿//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Haferbrei {

[CreateAssetMenu(fileName = "ModRessourceRecipe", menuName = "Scriptable Objects/Ressourcen/ModRecipe", order = 0)]
public class ModRessourceRecipe : SerializedScriptableObject
{
    public string identifier;
    [OdinSerialize] public Dictionary<Ressource, ModFloat> recipe = new Dictionary<Ressource, ModFloat>();
}
}