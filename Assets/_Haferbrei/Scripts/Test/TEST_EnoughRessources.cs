//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class TEST_EnoughRessources : MonoBehaviour
{
    [SerializeField, BoxGroup("Values"), Required] public bool playerHasEnoughRessources;
    
    
}
}