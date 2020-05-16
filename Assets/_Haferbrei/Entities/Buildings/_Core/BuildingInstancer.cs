using System;
using UnityEngine;

namespace Haferbrei{
public class BuildingInstancer
{
    public static Action<BuildingModel> onNewBuilding = delegate(BuildingModel _model) {  };
    
    public static BuildingModel Instantiate(BuildingType _buildingType, Vector3 _position, BuildingModel _template = null)
    {
        //-- instantiate Model --
        BuildingModel model = (_template == null) ? ScriptableObject.CreateInstance<BuildingModel>() : ScriptableObject.Instantiate(_template);
        
        //-- Set initial values --
        model.Initialize(_buildingType, _position, IngameDateTime.Now);

        //-- send creation event --
        onNewBuilding?.Invoke(model);
        return model;
    }
}
}