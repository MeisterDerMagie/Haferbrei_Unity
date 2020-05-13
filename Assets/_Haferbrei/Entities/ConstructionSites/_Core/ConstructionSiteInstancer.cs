using System;
using UnityEditor;
using UnityEngine;

namespace Haferbrei{
public class ConstructionSiteInstancer
{
    public static Action<ConstructionSiteModel> onNewConstructionSite = delegate(ConstructionSiteModel _model) {  };
    
    public static ConstructionSiteModel Instantiate(BuildingType _buildingType, Vector3 _position, ConstructionSiteModel _template = null)
    {
        //-- instantiate Model --
        ConstructionSiteModel model = (_template == null) ? ScriptableObject.CreateInstance<ConstructionSiteModel>() : ScriptableObject.Instantiate(_template);
        
        //-- Set initial values --
        model.SetInitialValues(_buildingType, _position, IngameDateTime.Now);

        //-- send creation event --
        onNewConstructionSite?.Invoke(model);
        return model;
    }
}
}