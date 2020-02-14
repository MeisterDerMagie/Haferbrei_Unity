//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
[System.Serializable]
public abstract class Item : ScriptableObject
{
    public string identifier;
    public Sprite icon;
}
}