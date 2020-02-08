using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei{
public class TEST_SaveLoadTest : SerializedMonoBehaviour
{
    [SerializeField] private Vector3 testVector1;
    [SerializeField] public Vector3 testVector2;
    [SerializeField] private float testFloat;

    [SerializeField] private RessourceRecipe testScriptableObjectReference;

    [SerializeField] private Dictionary<Ressource, int> testRessourcesDictionary;

    [SerializeField] private GuidReference guidReferenceTest;

}
}