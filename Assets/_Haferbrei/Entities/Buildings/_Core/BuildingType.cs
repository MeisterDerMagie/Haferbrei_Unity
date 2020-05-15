//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "BuildingType", menuName = "Scriptable Objects/Buildings/BuildingType", order = 0)]
public class BuildingType : ScriptableObject
{
    public bool canBeBuilt = true;
    //public string identifier;
    public LocalizedString buildingName;
    public int capacity;
    public bool isUnique;
    public ModRessourceRecipe cost;

    public bool hasConstructionSite;
    [ShowIf("hasConstructionSite"), LabelText("Construction Site Duration [s]")] public float constructionSiteBaseDuration;
    [ShowIf("hasConstructionSite")] public GameObject constructionSitePrefab;
    
    public GameObject instancePrefab;
    public GameObject previewPrefab;
    [PreviewField] public Sprite icon;
}
}