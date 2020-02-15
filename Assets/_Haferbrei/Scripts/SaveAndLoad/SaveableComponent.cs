//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class SaveableComponent : MonoBehaviour, IInitSelf
{
    public Component component;
    
    [Button]
    public void InitSelf() => SaveLoadController.registerSaveableComponent(component);
    private void OnDestroy() => SaveLoadController.unregisterSaveableComponent(component);
}
}