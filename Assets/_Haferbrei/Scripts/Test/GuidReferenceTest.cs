using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GuidReferenceTest : SerializedMonoBehaviour
{
    [SerializeField, FoldoutGroup("References"), Required] private GuidReference reference;

    [Button]
    private void SayName()
    {
        Debug.Log(reference.gameObject.name);
    }
}
