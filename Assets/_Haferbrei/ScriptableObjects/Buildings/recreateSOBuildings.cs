//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Wichtel;


namespace Haferbrei {
[CreateAssetMenu(fileName = "asdf", menuName = "asdf", order = 0)]
public class recreateSOBuildings : ScriptableObjectWithGuid
{
    [Button]
    public void LosJetzt()
    {
        var alleBuildings = UT_ScriptableObjectsUtilities_W.GetScriptableObjectInstances<Buildings>();

        List<BuildingData> buildingDatas = new List<BuildingData>();
        
        foreach (var building in alleBuildings)
        {
            var newData = new BuildingData();
            newData.name = building.name;
            newData.assetPath = AssetDatabase.GetAssetPath(building);
            newData.canBeBuilt = building.canBeBuilt;
            newData.identifier = building.identifier;
            newData.capacity = 0;
            newData.isUnique = building.isUnique;
            
            buildingDatas.Add(newData);
        }

        foreach (var data in buildingDatas)
        {
            AssetDatabase.DeleteAsset(data.assetPath);
            
            Buildings asset = ScriptableObject.CreateInstance<Buildings>();
            asset.canBeBuilt = data.canBeBuilt;
            asset.identifier = data.identifier;
            asset.capacity = 0;
            asset.isUnique = data.isUnique;
            
            AssetDatabase.CreateAsset(asset, data.assetPath);
            
            
        }
    }
}

public class BuildingData
{
    public string name;
    public string assetPath;
    
    public bool canBeBuilt;
    public string identifier;
    public int capacity;
    public bool isUnique;
}
}