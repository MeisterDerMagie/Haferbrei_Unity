using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Haferbrei.JsonConverters{
public class VectorConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        //Start writing
        writer.WriteStartObject();
        writer.WritePropertyName("$type");
        string typeAndAssembly = value.GetType().FullName + ", " + Assembly.GetAssembly(value.GetType()).GetName().Name;
        writer.WriteValue(typeAndAssembly);
        
        //write custom values here:
        if (value is Vector2 vector2)
        {
            WriteVector(writer, vector2.x, vector2.y, null, null);
        }
        else if (value is Vector3 vector3)
        {
            WriteVector(writer, vector3.x, vector3.y, vector3.z, null);
        }
        else if (value is Vector4 vector4)
        {
            WriteVector(writer, vector4.x, vector4.y, vector4.z, vector4.w);
        }
        else if (value is Vector2Int vector2Int)
        {
            WriteVectorInt(writer, vector2Int.x, vector2Int.y, null);
        }
        else if (value is Vector3Int vector3Int)
        {
            WriteVectorInt(writer, vector3Int.x, vector3Int.y, vector3Int.z);
        }
        
        //end writing
        writer.WriteEndObject();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);
        return ReadVector(obj, objectType);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Vector2) ||
               objectType == typeof(Vector3) ||
               objectType == typeof(Vector4) ||
               objectType == typeof(Vector2Int) ||
               objectType == typeof(Vector3Int);
    }

    private void WriteVector(JsonWriter writer, float x, float y, float? z, float? w)
    {
        writer.WritePropertyName("x");
        writer.WriteValue(x);
        writer.WritePropertyName("y");
        writer.WriteValue(y);
        
        if (!z.HasValue) return;
        writer.WritePropertyName("z");
        writer.WriteValue(z.Value);

        if (!w.HasValue) return;
        writer.WritePropertyName("w");
        writer.WriteValue(w.Value);
    }
    
    private void WriteVectorInt(JsonWriter writer, int x, int y, int? z)
    {
        writer.WritePropertyName("x");
        writer.WriteValue(x);
        writer.WritePropertyName("y");
        writer.WriteValue(y);
        
        if (!z.HasValue) return;
        writer.WritePropertyName("z");
        writer.WriteValue(z.Value);
    }

    private object ReadVector(JObject obj, Type objectType)
    {
        if (objectType == typeof(Vector2))
        {
            var vector = new Vector2
            {
                x = (float) obj["x"],
                y = (float) obj["y"]
            };
            return vector;
        }
        if(objectType == typeof(Vector3))
        {
            var vector = new Vector3()
            {
                x = (float) obj["x"],
                y = (float) obj["y"],
                z = (float) obj["z"]
            };
            return vector;
        }
        if(objectType == typeof(Vector4))
        {
            var vector = new Vector4()
            {
                x = (float) obj["x"],
                y = (float) obj["y"],
                z = (float) obj["z"],
                w = (float) obj["w"]
            };
            return vector;
        }
        if(objectType == typeof(Vector2Int))
        {
            var vector = new Vector2Int()
            {
                x = (int) obj["x"],
                y = (int) obj["y"]
            };
            return vector;
        }
        if(objectType == typeof(Vector3Int))
        {
            var vector = new Vector3Int()
            {
                x = (int) obj["x"],
                y = (int) obj["y"],
                z = (int) obj["z"]
            };
            return vector;
        }
        
        Debug.LogError("Can't read Vector!");
        return null;
    }
}
}