using System.Collections;
using System.Collections.Generic;
using Haferbrei;
using Sirenix.OdinInspector;
using UnityEngine;

public class TEST_FloatModifier : SerializedMonoBehaviour
{
    public ModFloat modFloat;
    
    public Dictionary<string, ModFloat> testDictionary = new Dictionary<string, ModFloat>();

    [Button]
    public void AddModifier(FloatModifier _modifier)
    {
        foreach (var modifiableFloat in testDictionary)
        {
            modifiableFloat.Value.AddModifier(_modifier);
        }
    }
}
