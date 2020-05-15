//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class TEST_IngameTimer : MonoBehaviour
{
    [Saveable] public IngameTimer ingameTimer;
    [Saveable] public ConstructionSiteModel csm;
    [SerializeField, BoxGroup("References"), Required] private TEST_IngameTimer2 otherScript;
    
    [Button]
    public void TEST_NewTimer(float _duration, float _updateFrequency)
    {
        if(ingameTimer == null) ingameTimer = new IngameTimer();
        ingameTimer.RunTimer(_duration, _updateFrequency);

        otherScript.ingameTimer = ingameTimer;
    }

    private void Start()
    {
        csm = ScriptableObject.CreateInstance<ConstructionSiteModel>();
    }

    [Button]
    public void Pause()
    {
        ingameTimer.Pause();
    }
    
    [Button]
    public void Cancel()
    {
        ingameTimer.CancelTimer();
    }

    [Button]
    public void Resume()
    {
        ingameTimer.Resume();
    }
}
}