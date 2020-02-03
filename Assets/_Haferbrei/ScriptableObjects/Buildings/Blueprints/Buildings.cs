﻿//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Buildings/Building", order = 0)]
public class Buildings : ScriptableObject
{
    public bool canBeBuilt = true;
    public string identifier;
    public int capacity;
    public bool isUnique;
    public GameObject instancePrefab;
    public GameObject previewPrefab;
}
}