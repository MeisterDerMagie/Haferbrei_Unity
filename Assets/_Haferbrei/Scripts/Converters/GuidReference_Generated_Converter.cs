using System;
using System.Collections;
using System.Collections.Generic;
using Bayat.Json.Serialization;
using UnityEngine;

namespace Bayat.Json.Converters
{

    public class GuidReference_Generated_Converter : ObjectJsonConverter
    {

        public override string[] GetObjectProperties()
        {
            return new string[] { "serializedGuid", "cachedName", "cachedScene", "gameObject" };
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(GuidReference);
        }

        public override void WriteProperties(JsonObjectContract contract, JsonWriter writer, object value, Type objectType, JsonSerializerWriter internalWriter)
		{
			var instance = (GuidReference)value;
			
            writer.WriteProperty("referenceGuid", instance.GetReferenceGuid());
		}

		public override object PopulateMember(string memberName, JsonContract contract, JsonReader reader, Type objectType, object targetObject, JsonSerializerReader internalReader)
		{
			var instance = (GuidReference)targetObject;
			switch (memberName)
			{
				case "referenceGuid":
					var referenceGuid = reader.ReadProperty<System.Guid>();
				    instance = new GuidReference(referenceGuid);
                    break;
				default:
					reader.Skip();
					break;
			}
			return instance;
        }

    }

}