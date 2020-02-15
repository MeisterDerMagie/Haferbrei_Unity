using System;
using System.Collections;
using System.Collections.Generic;
using Bayat.Json.Serialization;
using UnityEngine;

namespace Bayat.Json.Converters
{

    public class HaferbreiRessource_Generated_Converter : ObjectJsonConverter
    {

        public override string[] GetObjectProperties()
        {
            return new string[] { "identifier", "goldwert", "category", "name", "hideFlags" };
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Haferbrei.Ressource);
        }

        public override void WriteProperties(JsonObjectContract contract, JsonWriter writer, object value, Type objectType, JsonSerializerWriter internalWriter)
		{
			var instance = (Haferbrei.Ressource)value;
            writer.WriteProperty("identifier", instance.identifier);
            writer.WriteProperty("goldwert", instance.goldwert);
            internalWriter.SerializeProperty(writer, "category", instance.category);
            writer.WriteProperty("name", instance.name);
            internalWriter.SerializeProperty(writer, "hideFlags", instance.hideFlags);
		}

		public override object PopulateMember(string memberName, JsonContract contract, JsonReader reader, Type objectType, object targetObject, JsonSerializerReader internalReader)
		{
			var instance = (Haferbrei.Ressource)targetObject;
			switch (memberName)
			{
				case "identifier":
				    instance.identifier = reader.ReadProperty<System.String>();
                    break;
				case "goldwert":
				    instance.goldwert = reader.ReadProperty<System.Single>();
                    break;
				case "category":
				    instance.category = internalReader.DeserializeProperty<Haferbrei.RessourceCategory>(reader);
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