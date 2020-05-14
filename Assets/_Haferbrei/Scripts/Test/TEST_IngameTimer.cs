//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class TEST_IngameTimer : MonoBehaviour
{
    [Saveable] public IngameTimer ingameTimer;
    [SerializeField, BoxGroup("References"), Required] private TEST_IngameTimer2 otherScript;
    
    [Button]
    public void TEST_NewTimer(float _duration, float _updateFrequency)
    {
        ingameTimer = new IngameTimer(_duration, _updateFrequency);
        //ingameTimer.Start();

        otherScript.ingameTimer = ingameTimer;
    }
}
}