using System;
using UnityEngine;

namespace Haferbrei {
public class PressPlusMinusToControlGameSpeed : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus))
        {
            GameTime.SetTimeScale(2f);
        }
        else if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
        {
            GameTime.SetTimeScale(0.5f);
        }
        else
        {
            GameTime.SetTimeScale(1f);
        }
    }
}
}