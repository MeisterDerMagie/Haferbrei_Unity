using System;
using Object = UnityEngine.Object;

namespace Haferbrei{
public class BuildingDestroyer
{
    public static Action<BuildingModel> onBuildingDestroyed = delegate(BuildingModel _model) {  };
    
    public static void Destroy(BuildingModel _model)
    {
        onBuildingDestroyed?.Invoke(_model);
        _model.OnModelDestroyed?.Invoke();
        
        _model.Terminate();
        Object.Destroy(_model);
    }
}
}