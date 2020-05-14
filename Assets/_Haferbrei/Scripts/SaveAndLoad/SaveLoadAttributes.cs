using System;

namespace Haferbrei{

[AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
public class SaveableAttribute : Attribute
{
    
}

[AttributeUsage(AttributeTargets.Class)]
public class SaveableClassAttribute : Attribute
{
    
}

[AttributeUsage(AttributeTargets.Struct)]
public class SaveableStructAttribute : Attribute
{
    
}
}