//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
public class PauseableTest : MonoBehaviour, IPauseable
{
    public void OnPause()
    {
        Debug.Log("Jep, Pause!");
    }

    public void OnUnpause()
    {
        Debug.Log("Hm, Pause vorbei. :(");
    }
}
}