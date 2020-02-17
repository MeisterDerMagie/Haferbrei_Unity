namespace Haferbrei{
public class FloatModifier
{
    public readonly float Value;
    public readonly StatModType Type;
    public readonly int Order;
    public readonly IFloatModifierSource Source;
 
    // "Main" constructor. Requires all variables.
    public FloatModifier(float value, StatModType type, int order, IFloatModifierSource source) // Added "source" input parameter
    {
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }
 
    // Requires Value and Type. Calls the "Main" constructor and sets Order and Source to their default values: (int)type and null, respectively.
    public FloatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }
 
    // Requires Value, Type and Order. Sets Source to its default value: null
    public FloatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }
 
    // Requires Value, Type and Source. Sets Order to its default value: (int)Type
    public FloatModifier(float value, StatModType type, IFloatModifierSource source) : this(value, type, (int)type, source) { }
}
}