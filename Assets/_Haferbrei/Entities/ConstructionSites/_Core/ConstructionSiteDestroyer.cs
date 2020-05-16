using System;
using Object = UnityEngine.Object;

namespace Haferbrei{
public class ConstructionSiteDestroyer
{
    public static Action<ConstructionSiteModel> onConstructionSiteDestroyed = delegate(ConstructionSiteModel _model) {  };
    
    public static void Destroy(ConstructionSiteModel _model)
    {
        onConstructionSiteDestroyed?.Invoke(_model);
        _model.OnModelDestroyed?.Invoke();
        
        _model.Terminate();
        Object.Destroy(_model);
    }
}
}