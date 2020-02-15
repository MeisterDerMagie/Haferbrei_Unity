using System;
using System.Collections;
using System.Collections.Generic;
using Bayat.Json.Serialization;
using UnityEngine;

namespace Bayat.Json.Converters
{

    public class HaferbreiRessourceContainer_Generated_Converter : ObjectJsonConverter
    {

        public override string[] GetObjectProperties()
        {
            return new string[] { "ressources", "name", "hideFlags" };
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Haferbrei.RessourceContainer);
        }

        public override void WriteProperties(JsonObjectContract contract, JsonWriter writer, object value, Type objectType, JsonSerializerWriter internalWriter)
		{
			var instance = (Haferbrei.RessourceContainer)value;
            internalWriter.SerializeProperty(writer, "ressources", instance.ressources);
            writer.WriteProperty("name", instance.name);
            internalWriter.SerializeProperty(writer, "hideFlags", instance.hideFlags);
		}

		public override object PopulateMember(string memberName, JsonContract contract, JsonReader reader, Type objectType, object targetObject, JsonSerializerReader internalReader)
		{
			var instance = (Haferbrei.RessourceContainer)targetObject;
			switch (memberName)
			{
				case "ressources":
				    instance.ressources = internalReader.DeserializeProperty<System.Collections.Generic.Dictionary<Haferbrei.Ressource, System.Int32>>(reader);
				    break;
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