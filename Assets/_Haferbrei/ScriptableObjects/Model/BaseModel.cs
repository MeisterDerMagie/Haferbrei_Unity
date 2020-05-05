using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei{
public abstract class BaseModel : SerializedScriptableObject
{
    public Action onModelChanged;
    public Action onModelDestroyed;
}
}