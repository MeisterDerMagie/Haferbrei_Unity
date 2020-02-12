//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "RessourcePackage", menuName = "Scriptable Objects/Ressources/RessourcePackage", order = 0)]
public class RessourcePackage : ScriptableObjectWithGuid
{
    [OdinSerialize] private Dictionary<Ressource, int> ressources = new Dictionary<Ressource, int>();
    
    
}
}