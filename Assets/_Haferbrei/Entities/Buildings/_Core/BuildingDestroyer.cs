using System;
using Lean.Pool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Haferbrei{
public class BuildingDestroyer
{
    public static Action<BuildingModel> onBuildingDestroyed = delegate(BuildingModel _model) {  };
    
    public static void Destroy(BuildingModel _buildingModel)
    {
        onBuildingDestroyed?.Invoke(_buildingModel);
        Object.Destroy(_buildingModel);
    }
}
}