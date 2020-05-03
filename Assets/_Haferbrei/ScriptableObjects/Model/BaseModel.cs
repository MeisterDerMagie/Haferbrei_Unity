using System;
using Sirenix.OdinInspector;

namespace Haferbrei{
public abstract class BaseModel : SerializedScriptableObject
{
    public Action onModelChanged;
}
}