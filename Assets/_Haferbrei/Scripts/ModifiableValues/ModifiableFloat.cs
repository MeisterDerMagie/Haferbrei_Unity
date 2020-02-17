﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sirenix.OdinInspector;

namespace Haferbrei{
[Serializable]
public class ModifiableFloat
{
    [OnValueChanged("CalculateFinalValue")]
    public float BaseValue;
    public readonly ReadOnlyCollection<FloatModifier> FloatModifiers;
    [ShowInInspector, ReadOnly] private readonly List<FloatModifier> floatModifiers;
    
    private bool isDirty = true;
    [ShowInInspector, ReadOnly] private float value;
    private float lastBaseValue = float.MinValue;
 
    //--- Constructors ---
    public ModifiableFloat(float baseValue) : this()
    {
        BaseValue = baseValue;
    }
    public ModifiableFloat()
    {
        floatModifiers = new List<FloatModifier>();
        FloatModifiers = floatModifiers.AsReadOnly();
    }
    //--- ---

    public float Value
    {
        get {
            if (!isDirty && lastBaseValue == BaseValue) return value;
            CalculateFinalValue();
            return value;
        }
    }

    public void AddModifier(FloatModifier mod)
    {
        isDirty = true;
        floatModifiers.Add(mod);
        floatModifiers.Sort(CompareModifierOrder);
        CalculateFinalValue();
    }
     
    public bool RemoveModifier(FloatModifier mod)
    {
        if (floatModifiers.Remove(mod))
        {
            isDirty = true;
            CalculateFinalValue();
            return true;
        }
        return false;
    }

    public bool RemoveAllModifiersFromSource(IFloatModifierSource source)
    {
        bool didRemove = false;
     
        for (int i = floatModifiers.Count - 1; i >= 0; i--)
        {
            if (floatModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                floatModifiers.RemoveAt(i);
            }
        }
        CalculateFinalValue();
        return didRemove;
    }
     
    private void CalculateFinalValue()
    {
        lastBaseValue = BaseValue;
        
        float finalValue = BaseValue;
        float sumPercentAdd = 0; // This will hold the sum of our "PercentAdd" modifiers
 
        for (int i = 0; i < floatModifiers.Count; i++)
        {
            FloatModifier mod = floatModifiers[i];
 
            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd) // When we encounter a "PercentAdd" modifier
            {
                sumPercentAdd += mod.Value; // Start adding together all modifiers of this type
 
                // If we're at the end of the list OR the next modifer isn't of this type
                if (i + 1 >= floatModifiers.Count || floatModifiers[i + 1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                    sumPercentAdd = 0; // Reset the sum back to 0
                }
            }
            else if (mod.Type == StatModType.PercentMult) // Percent renamed to PercentMult
            {
                finalValue *= 1 + mod.Value;
            }
        }
        
        // Rounding gets around dumb float calculation errors (like getting 12.0001f, instead of 12f)
        // 4 significant digits is usually precise enough, but feel free to change this to fit your needs
        value = (float)Math.Round(finalValue, 4);
        
        isDirty = false;
    }

    private int CompareModifierOrder(FloatModifier a, FloatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        if (a.Order > b.Order)
            return 1;
        return 0; // if (a.Order == b.Order)
    }
}
}