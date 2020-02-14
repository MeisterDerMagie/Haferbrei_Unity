using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Haferbrei{
public class ScriptableObjectExtensions
{
    public static T InstantiateScriptableObjectWithGuid<T>(T _template) where T : ScriptableObjectWithGuid
    {
        var newSOWithGuid = Object.Instantiate(_template);

        //if (newSOWithGuid is SaveableScriptableObject) (newSOWithGuid as SaveableScriptableObject).SetGuid(Guid.NewGuid());
        
        newSOWithGuid.guid = Guid.NewGuid();
        return newSOWithGuid;
    }
}
}