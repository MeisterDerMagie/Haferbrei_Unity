//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "RessourcePool", menuName = "Scriptable Objects/Ressourcen/Ressource Pool", order = 0)]
public class RessourcePoolScriptableObject : SerializedScriptableObject, IResettable
{
    public string identifier;
    [HideInPlayMode]   public RessourcePackage initialRessourcePackage;
    [HideInEditorMode] public RessourcePackage ressourcePackage;
    
    public void ResetSelf()
    {
        ressourcePackage = new RessourcePackage(initialRessourcePackage);
    }
}
}