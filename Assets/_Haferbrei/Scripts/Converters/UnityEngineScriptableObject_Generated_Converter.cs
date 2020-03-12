using System;
using System.Collections;
using System.Collections.Generic;
using Bayat.Json.Serialization;
using Haferbrei;
using UnityEngine;

namespace Bayat.Json.Converters
{

    public class UnityEngineScriptableObject_Generated_Converter : ObjectJsonConverter
    {

        public override string[] GetObjectProperties()
        {
	        return new string[] { "name", "hideFlags" };
        }

        public override bool CanConvert(Type objectType)
        {
	        return objectType.IsSubclassOf(typeof(ScriptableObject));
        }

        public override void WriteProperties(JsonObjectContract contract, JsonWriter writer, object value, Type objectType, JsonSerializerWriter internalWriter)
		{
			var scriptableObjectGuid = ScriptableObjectReferences.Instance.saveableScriptableObjectsReferences.ResolveReference(value as ScriptableObject);
			writer.WriteProperty("scriptableObjectGuid", scriptableObjectGuid.ToString());
		}

		public override object PopulateMember(string memberName, JsonContract contract, JsonReader reader, Type objectType, object targetObject, JsonSerializerReader internalReader)
		{
			Debug.Log("ScriptableObjectConverter PopulateMember");
			Debug.Log(memberName);

			Guid scriptableObjectGuid = Guid.Empty;
			switch (memberName)
			{
				case "scriptableObjectGuid":
					var guidAsString = reader.ReadProperty<System.String>();
					scriptableObjectGuid = Guid.Parse(guidAsString);
					break;
				default:
					Debug.Log("Skip");
					reader.Skip();
					break;
			}

			//ScriptableObject instance = null;
			targetObject = (scriptableObjectGuid == Guid.Empty) ? null : ScriptableObjectReferences.Instance.saveableScriptableObjectsReferences.ResolveGuid(scriptableObjectGuid);
			return targetObject;
		}

    }

}