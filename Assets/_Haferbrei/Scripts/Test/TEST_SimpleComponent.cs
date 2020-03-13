using System;
using System.Collections;
using System.Collections.Generic;
using Bayat.Json;
using Haferbrei;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

public class TEST_SimpleComponent : MonoBehaviour
{
    public RessourceValueList allRessources;
    
    //[Saveable] public int testInt;
    //[Saveable] public List<int> testIntList;

    //[Saveable] public float testFloat;
    //[Saveable] public List<float> testFloatList;

    //[Saveable] public GameObject testGameObject;
    //[Saveable] public List<GameObject> testGameObjectList;

    [Saveable] public GuidReference guidReference;
    [Saveable] public List<GuidReference> guidReferenceList;

    //[Saveable] public Ressource ressource;
    //[Saveable] public List<Ressource> ressourceList;

    //[Saveable, InlineEditor] public RessourceContainer ressourceContainer;

    [Button, DisableInEditorMode]
    public void SetValues()
    {
        //testInt = 5;
        //testIntList.Add(6);
        //testIntList.Add(7);
        
        //testFloat = 8.9f;
        //testFloatList.Add(1.2f);
        //testFloatList.Add(3.4f);

        
        //testGameObject = Camera.main.gameObject;
        //testGameObjectList.Add(FindObjectOfType<GameObject>());
        //testGameObjectList.Add(FindObjectOfType<SaveableGameObject>().gameObject);

        guidReference = new GuidReference(FindObjectOfType<GuidComponent>().GetGuid());
        guidReferenceList.Add(new GuidReference(FindObjectOfType<GuidComponent>().GetGuid()));

        //ressource = allRessources[1];
        //ressourceList.Add(allRessources[2]);
        //ressourceList.Add(allRessources[3]);
        
        //ressourceContainer = ScriptableObject.CreateInstance<RessourceContainer>();
        //ressourceContainer.AddRessource(allRessources[4], 10);
        //ressourceContainer.AddRessource(allRessources[5], 20);
        
    }
}
