using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Haferbrei.JsonConverters{
public class ScriptableObjectConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        //Start writing
        writer.WriteStartObject();
        writer.WritePropertyName("$type");
        string typeAndAssembly = value.GetType().FullName + ", " + Assembly.GetAssembly(value.GetType()).GetName().Name;
        writer.WriteValue(typeAndAssembly);
        
        //write custom values here:
        var scriptableObjectGuid = ScriptableObjectReferences.Instance.saveableScriptableObjectsReferences.ResolveReference(value as ScriptableObject);
        writer.WritePropertyName("scriptableObjectGuid");
        writer.WriteValue(scriptableObjectGuid.ToString());
        
        //end writing
        writer.WriteEndObject();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);
        string guidAsString = (string)obj["scriptableObjectGuid"];
        Guid scriptableObjectGuid = Guid.Parse(guidAsString);
        
        return ScriptableObjectReferences.Instance.saveableScriptableObjectsReferences.ResolveGuid(scriptableObjectGuid);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType.IsSubclassOf(typeof(ScriptableObject));
    }
}
}