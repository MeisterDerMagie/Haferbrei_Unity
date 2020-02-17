using System;
using System.Collections;
using System.Collections.Generic;
using Bayat.Json.Serialization;
using UnityEngine;

namespace Bayat.Json.Converters
{

    public class HaferbreiHasRessourceContainer_Generated_Converter : ObjectJsonConverter
    {

        public override string[] GetObjectProperties()
        {
            return new string[] { "template", "ressourceContainer", "useGUILayout", "enabled", "hideFlags" };
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Haferbrei.HasRessourceContainer);
        }

        public override void WriteProperties(JsonObjectContract contract, JsonWriter writer, object value, Type objectType, JsonSerializerWriter internalWriter)
		{
			var instance = (Haferbrei.HasRessourceContainer)value;
            internalWriter.SerializeProperty(writer, "template", instance.template);
            internalWriter.SerializeProperty(writer, "ressourceContainer", instance.ressourceContainer);
            writer.WriteProperty("useGUILayout", instance.useGUILayout);
            writer.WriteProperty("enabled", instance.enabled);
            internalWriter.SerializeProperty(writer, "hideFlags", instance.hideFlags);
		}

		public override object PopulateMember(string memberName, JsonContract contract, JsonReader reader, Type objectType, object targetObject, JsonSerializerReader internalReader)
		{
			var instance = (Haferbrei.HasRessourceContainer)targetObject;
			switch (memberName)
			{
				case "template":
				    instance.template = internalReader.DeserializeProperty<Haferbrei.RessourceContainer>(reader);
				    break;
				case "ressourceContainer":
				    instance.ressourceContainer = internalReader.DeserializeProperty<Haferbrei.RessourceContainer>(reader);
				    break;
				case "useGUILayout":
				    instance.useGUILayout = reader.ReadProperty<System.Boolean>();
                    break;
				case "enabled":
				    instance.enabled = reader.ReadProperty<System.Boolean>();
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