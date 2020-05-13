using System;
using Lean.Pool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Haferbrei{
public class ConstructionSiteDestroyer
{
    public static Action<ConstructionSiteModel> onConstructionSiteDestroyed = delegate(ConstructionSiteModel _model) {  };
    
    public static void Destroy(ConstructionSiteModel _constructionSiteModel)
    {
        onConstructionSiteDestroyed?.Invoke(_constructionSiteModel);
        Object.Destroy(_constructionSiteModel);
    }
}
}