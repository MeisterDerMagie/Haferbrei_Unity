using System.Collections;
using System.Collections.Generic;
using Haferbrei;
using UnityEngine;

public class TEST_SimpleComponent : MonoBehaviour
{
    [Saveable] public int testInt;
    [Saveable] public List<GameObject> testGameObject;

    [Saveable] public Ressource ressource;
    [Saveable] public List<Ressource> ressourceList;
}
