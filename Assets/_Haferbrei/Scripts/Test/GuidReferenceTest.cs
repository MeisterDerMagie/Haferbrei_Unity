using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GuidReferenceTest : MonoBehaviour
{
    [SerializeField, FoldoutGroup("References"), Required] private GuidReference reference;

    private void Update()
    {
        //Debug.Log(reference.gameObject.name);
    }
}
