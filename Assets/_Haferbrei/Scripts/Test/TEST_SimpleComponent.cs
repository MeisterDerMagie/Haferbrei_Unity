using System;
using System.Collections;
using System.Collections.Generic;
using Haferbrei;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

public class TEST_SimpleComponent : SerializedMonoBehaviour
{
    [Saveable] public BuildingReference zuBauendesGebaude;
    
    [Button, DisableInEditorMode]
    public void SetDummyValues()
    {
        //just set any dummy values to check if save/load works
    }
}
