//(c) copyright by HolyHammer Entertainment
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "Ressource", menuName = "Scriptable Objects/Ressources/Ressource", order = 0)]
//[TypeConverter(typeof (RessourceTypeConverter))]
public class Ressource : SerializedScriptableObject
{
    public string identifier;
    public int order;
    public float goldwert;
    public RessourceCategory category;
    [PreviewField] public Sprite icon;
}
}