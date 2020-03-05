using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wichtel.Extensions;

public class TEST_ComponentOrder : MonoBehaviour
{
    public int index = 1;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        this.MoveComponentAtIndex(index);
        #endif
    }
}
