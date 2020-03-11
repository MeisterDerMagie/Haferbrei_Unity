using System;
using System.Collections;
using System.Collections.Generic;
using Bayat.Json.Serialization;
using UnityEngine;

namespace Bayat.Json.Converters
{

    public class UnityEngineScriptableObject_Generated_Converter : ObjectJsonConverter
    {

        public override string[] GetObjectProperties()
        {
	        Debug.Log("ScriptableObjectConverter GetProperties");
            return new string[] { "name", "hideFlags" };
        }

        public override bool CanConvert(Type objectType)
        {
	        Debug.Log("Is type: " + objectType.ToString());
	        Debug.Log("CanConvert ScriptableObject: " + (objectType == typeof(UnityEngine.ScriptableObject)));
            return objectType == typeof(UnityEngine.ScriptableObject);
        }

        public override void WriteProperties(JsonObjectContract contract, JsonWriter writer, object value, Type objectType, JsonSerializerWriter internalWriter)
		{
			Debug.Log("ScriptableObjectConverter WriteProperties");

			var instance = (UnityEngine.ScriptableObject)value;
            writer.WriteProperty("name", instance.name);
            internalWriter.SerializeProperty(writer, "hideFlags", instance.hideFlags);
		}

		public override object PopulateMember(string memberName, JsonContract contract, JsonReader reader, Type objectType, object targetObject, JsonSerializerReader internalReader)
		{
			Debug.Log("ScriptableObjectConverter PopulateMember");

			
			var instance = (UnityEngine.ScriptableObject)targetObject;
			switch (memberName)
			{
				case "name":
				    instance.name = reader.ReadProperty<System.String>();
                    break;
				case "hideFlags":
				    instance.hideFlags = internalReader.DeserializeProperty<UnityEngine.HideFlags>(reader);
				    break;
				default:
					reader.Skip();
					break;
			}
			return instance;
        }

    }

}