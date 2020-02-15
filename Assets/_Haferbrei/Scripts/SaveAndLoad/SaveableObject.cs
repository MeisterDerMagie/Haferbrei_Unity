//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class SaveableObject : MonoBehaviour, IInitSelf
{
    [Button]
    public void InitSelf() => SaveLoadController.registerSaveableGameObject(gameObject);
    private void OnDestroy() => SaveLoadController.unregisterSaveableGameObject(gameObject);
}
}