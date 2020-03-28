using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Haferbrei.JsonConverters{
public class QuaternionConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        //Start writing
        writer.WriteStartObject();
        writer.WritePropertyName("$type");
        string typeAndAssembly = value.GetType().FullName + ", " + Assembly.GetAssembly(value.GetType()).GetName().Name;
        writer.WriteValue(typeAndAssembly);
        
        //write custom values here:
        var quaternion = (Quaternion) value;
        writer.WritePropertyName("x");
        writer.WriteValue(quaternion.x);
        writer.WritePropertyName("y");
        writer.WriteValue(quaternion.y);
        writer.WritePropertyName("z");
        writer.WriteValue(quaternion.z);
        writer.WritePropertyName("w");
        writer.WriteValue(quaternion.w);
        
        //end writing
        writer.WriteEndObject();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);
        var quaternion = new Quaternion()
        {
            x = (float) obj["x"],
            y = (float) obj["y"],
            z = (float) obj["z"],
            w = (float) obj["w"]
        };
        return quaternion;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Quaternion);
    }
}
}