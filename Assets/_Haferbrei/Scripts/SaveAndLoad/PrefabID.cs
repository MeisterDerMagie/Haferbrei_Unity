//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class PrefabID : MonoBehaviour
{
    [ReadOnly]
    public string prefabID;

    private void OnValidate()
    {
        if(string.IsNullOrEmpty(prefabID)) prefabID = new Guid().ToString();
    }
}
}