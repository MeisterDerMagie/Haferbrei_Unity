//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
public class SpielstandWirklichUeberschreibenCallback : MonoBehaviour
{
    public void Yes() 
    {
        ButtonSaveSpielstand.overrideSaveFile?.Invoke();
    }
}
}