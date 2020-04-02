using System;
using System.Collections;
using System.Collections.Generic;
using Haferbrei;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

public class TEST_SimpleComponent : SerializedMonoBehaviour
{
    [Title("GuidReferences")]
    [Saveable] public GuidReference guidReference;
    [Saveable] public List<GuidReference> guidReferenceList;
    
    [Button, DisableInEditorMode]
    public void SetDummyValues()
    {
        //just set any dummy values to check if save/load works
        
        guidReference = new GuidReference(FindObjectOfType<GuidComponent>().GetGuid());
        guidReferenceList.Add(new GuidReference(FindObjectsOfType<GuidComponent>()[0].GetGuid()));
        guidReferenceList.Add(new GuidReference(FindObjectsOfType<GuidComponent>()[1].GetGuid()));
    }
}
