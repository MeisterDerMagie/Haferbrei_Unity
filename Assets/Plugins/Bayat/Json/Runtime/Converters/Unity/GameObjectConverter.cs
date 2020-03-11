using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using UnityEngine;

using Bayat.Core;

using Bayat.Json.Linq;
using Bayat.Json.Serialization;
using Bayat.Json.Utilities;
using Debug = UnityEngine.Debug;

namespace Bayat.Json.Converters
{

    public class GameObjectConverter : UnityObjectConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return typeof(UnityEngine.GameObject).IsAssignableFrom(objectType) && base.CanConvert(objectType);
        }

        public GameObjectConverter()
        {
        }

        public override string[] GetObjectProperties()
        {
            return new string[] { "name", "tag", "active", "isStatic", "layer", "hideFlags", "components", "children" };
        }

        public override List<string> GetSerializedProperties()
        {
            var list = new List<string>(base.GetSerializedProperties());
            list.AddRange(GetObjectProperties());
            return list;
        }

        public override void WriteProperties(JsonObjectContract contract, JsonWriter writer, object value, Type objectType, JsonSerializerWriter internalWriter)
        {
            base.WriteProperties(contract, writer, value, objectType, internalWriter);

            Debug.LogError("Nope, das kannste nicht!");
        }

        public override object PopulateMember(string memberName, JsonContract contract, JsonReader reader, Type objectType, object targetObject, JsonSerializerReader internalReader)
        {
            GameObject gameObject = (GameObject)targetObject;
            bool finished;

            Debug.LogError("Nahein, das geht nicht!");
            
            return gameObject;
        }

    }

}