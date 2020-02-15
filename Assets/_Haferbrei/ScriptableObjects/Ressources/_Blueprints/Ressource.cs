//(c) copyright by HolyHammer Entertainment
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Bayat.Core;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "Ressource", menuName = "Scriptable Objects/Ressources/Ressource", order = 0)]
[TypeConverter(typeof (RessourceTypeConverter))]
public class Ressource : SerializedScriptableObject
{
    public string identifier;
    public float goldwert;
    public RessourceCategory category;
}

//wenn ein komplexer Typ (wie Ressource) als Key eines Dictionaries gespeichert wird, benötigt er einen TypeConverter, der festlegt,
//wie der Typ gespeichert und wieder geladen wird. Hier wird die Guid vom Bayat Save System gespeichert und hinterher wieder zurückverwandelt.
public class RessourceTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string))
        {
            return AssetReferenceResolver.Current.ResolveGuid(value as Ressource); //save the Guid
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string)
        {
            return AssetReferenceResolver.Current.ResolveReference(value as string); //restore the Guid
        }
        return base.ConvertFrom(context, culture, value);
    }
}
}