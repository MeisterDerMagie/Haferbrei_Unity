//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
public class TEST_AddWoodOnSpace : MonoBehaviour
{
    public RessourceContainer playerRessources;
    public Ressource ressourceToAdd;
    public int amountToAdd;
    
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        playerRessources.AddRessource(ressourceToAdd, amountToAdd);
    }
}
}