using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Haferbrei{
public class ConstructionSiteDestroyer : MonoBehaviour
{
    public static Action<ConstructionSiteModel> onConstructionSiteDestroyed = delegate(ConstructionSiteModel _model) {  };
    
    public static void Destroy(ConstructionSiteModel _model)
    {
        onConstructionSiteDestroyed?.Invoke(_model);
        _model.OnModelDestroyed?.Invoke();
        
        _model.Terminate();
        Object.Destroy(_model);
    }

    public static void Destroy(List<ConstructionSiteModel> _models)
    {
        foreach (var model in _models) { Destroy(model); }
    }

    private void OnDestroy()
    {
        Destroy(ModelCollection.GetModels<ConstructionSiteModel>());
    }
}
}