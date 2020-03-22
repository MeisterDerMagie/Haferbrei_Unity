//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Buildings/Building", order = 0)]
public class Building : ScriptableObject
{
    public bool canBeBuilt = true;
    public string identifier;
    public LocalizedString buildingName;
    public int capacity;
    public bool isUnique;
    public ModRessourceRecipe cost;
    public GameObject instancePrefab;
    public GameObject previewPrefab;
    [PreviewField] public Sprite icon;
}
}