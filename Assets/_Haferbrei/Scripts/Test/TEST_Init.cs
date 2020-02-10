using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Init : MonoBehaviour, IInitSelf
{
    public void InitSelf()
    {
        Debug.Log("Beep");
    }
}
