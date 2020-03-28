using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Haferbrei.JsonConverters{
public class GameObjectConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        Debug.LogError("You can't save GameObject references! Try to save GuidReferences instead.");
        
        //Start writing
        writer.WriteStartObject();
        writer.WritePropertyName("$type");
        string typeAndAssembly = value.GetType().FullName + ", " + Assembly.GetAssembly(value.GetType()).GetName().Name;
        writer.WriteValue(typeAndAssembly);
        
        //write custom values here:
        writer.WritePropertyName("error");
        writer.WriteValue("Can't save GameObject references");
        
        //end writing
        writer.WriteEndObject();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        Debug.LogError("You can't save GameObject references! Try to save GuidReferences instead.");
        
        return null;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(GameObject);
    }
}
}