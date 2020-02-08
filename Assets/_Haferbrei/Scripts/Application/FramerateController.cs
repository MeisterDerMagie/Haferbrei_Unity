using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FramerateController : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private int targetFramerate = 60; 
    
    private void Awake()
    {
        SetTargetFramerate();
    }

    [Button]
    private void SetTargetFramerate()
    {
        Application.targetFrameRate = targetFramerate;
    }
}
