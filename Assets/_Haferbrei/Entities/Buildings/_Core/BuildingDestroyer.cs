using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Haferbrei{
public class BuildingDestroyer : MonoBehaviour
{
    public static Action<BuildingModel> onBuildingDestroyed = delegate(BuildingModel _model) {  };
    
    public static void Destroy(BuildingModel _model)
    {
        onBuildingDestroyed?.Invoke(_model);
        _model.OnModelDestroyed?.Invoke();
        
        _model.Terminate();
        Object.Destroy(_model);
    }

    public static void Destroy(List<BuildingModel> _models)
    {
        foreach (var model in _models) { Destroy(model); }
    }

    private void OnDestroy()
    {
        Destroy(ModelCollection.GetModels<BuildingModel>());
    }
}
}