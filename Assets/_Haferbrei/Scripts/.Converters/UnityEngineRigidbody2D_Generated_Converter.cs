using System;
using System.Collections;
using System.Collections.Generic;
using Bayat.Json.Serialization;
using UnityEngine;

namespace Bayat.Json.Converters
{

    public class UnityEngineRigidbody2D_Generated_Converter : ObjectJsonConverter
    {

        public override string[] GetObjectProperties()
        {
            return new string[] { "position", "rotation", "velocity", "angularVelocity", "useAutoMass", "mass", "sharedMaterial", "centerOfMass", "inertia", "drag", "angularDrag", "gravityScale", "useFullKinematicContacts", "isKinematic", "freezeRotation", "constraints", "simulated", "interpolation", "sleepMode", "collisionDetectionMode", "hideFlags", "bodyType" };
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(UnityEngine.Rigidbody2D);
        }

        public override void WriteProperties(JsonObjectContract contract, JsonWriter writer, object value, Type objectType, JsonSerializerWriter internalWriter)
		{
			var instance = (UnityEngine.Rigidbody2D)value;
            internalWriter.SerializeProperty(writer, "position", instance.position);
            writer.WriteProperty("rotation", instance.rotation);
            internalWriter.SerializeProperty(writer, "velocity", instance.velocity);
            writer.WriteProperty("angularVelocity", instance.angularVelocity);
            writer.WriteProperty("useAutoMass", instance.useAutoMass);
            writer.WriteProperty("mass", instance.mass);
            internalWriter.SerializeProperty(writer, "sharedMaterial", instance.sharedMaterial);
            internalWriter.SerializeProperty(writer, "centerOfMass", instance.centerOfMass);
            writer.WriteProperty("inertia", instance.inertia);
            writer.WriteProperty("drag", instance.drag);
            writer.WriteProperty("angularDrag", instance.angularDrag);
            writer.WriteProperty("gravityScale", instance.gravityScale);
            writer.WriteProperty("useFullKinematicContacts", instance.useFullKinematicContacts);
            writer.WriteProperty("isKinematic", instance.isKinematic);
            writer.WriteProperty("freezeRotation", instance.freezeRotation);
            internalWriter.SerializeProperty(writer, "constraints", instance.constraints);
            writer.WriteProperty("simulated", instance.simulated);
            internalWriter.SerializeProperty(writer, "interpolation", instance.interpolation);
            internalWriter.SerializeProperty(writer, "sleepMode", instance.sleepMode);
            internalWriter.SerializeProperty(writer, "collisionDetectionMode", instance.collisionDetectionMode);
            internalWriter.SerializeProperty(writer, "hideFlags", instance.hideFlags);
            internalWriter.SerializeProperty(writer, "bodyType", instance.bodyType);
		}

		public override object PopulateMember(string memberName, JsonContract contract, JsonReader reader, Type objectType, object targetObject, JsonSerializerReader internalReader)
		{
			var instance = (UnityEngine.Rigidbody2D)targetObject;
			switch (memberName)
			{
				case "bodyType":
					instance.bodyType = internalReader.DeserializeProperty<UnityEngine.RigidbodyType2D>(reader);
					break;
				case "position":
				    instance.position = internalReader.DeserializeProperty<UnityEngine.Vector2>(reader);
				    break;
				case "rotation":
				    instance.rotation = reader.ReadProperty<System.Single>();
                    break;
				case "velocity":
				    instance.velocity = internalReader.DeserializeProperty<UnityEngine.Vector2>(reader);
				    break;
				case "angularVelocity":
				    instance.angularVelocity = reader.ReadProperty<System.Single>();
                    break;
				case "useAutoMass":
				    instance.useAutoMass = reader.ReadProperty<System.Boolean>();
                    break;
				case "mass":
				    instance.mass = reader.ReadProperty<System.Single>();
                    break;
				case "sharedMaterial":
				    instance.sharedMaterial = internalReader.DeserializeProperty<UnityEngine.PhysicsMaterial2D>(reader);
				    break;
				case "centerOfMass":
				    instance.centerOfMass = internalReader.DeserializeProperty<UnityEngine.Vector2>(reader);
				    break;
				case "inertia":
				    instance.inertia = reader.ReadProperty<System.Single>();
                    break;
				case "drag":
				    instance.drag = reader.ReadProperty<System.Single>();
                    break;
				case "angularDrag":
				    instance.angularDrag = reader.ReadProperty<System.Single>();
                    break;
				case "gravityScale":
				    instance.gravityScale = reader.ReadProperty<System.Single>();
                    break;
				case "useFullKinematicContacts":
				    instance.useFullKinematicContacts = reader.ReadProperty<System.Boolean>();
                    break;
				case "isKinematic":
				    instance.isKinematic = reader.ReadProperty<System.Boolean>();
                    break;
				case "freezeRotation":
				    instance.freezeRotation = reader.ReadProperty<System.Boolean>();
                    break;
				case "constraints":
				    instance.constraints = internalReader.DeserializeProperty<UnityEngine.RigidbodyConstraints2D>(reader);
				    break;
				case "simulated":
				    instance.simulated = reader.ReadProperty<System.Boolean>();
                    break;
				case "interpolation":
				    instance.interpolation = internalReader.DeserializeProperty<UnityEngine.RigidbodyInterpolation2D>(reader);
				    break;
				case "sleepMode":
				    instance.sleepMode = internalReader.DeserializeProperty<UnityEngine.RigidbodySleepMode2D>(reader);
				    break;
				case "collisionDetectionMode":
				    instance.collisionDetectionMode = internalReader.DeserializeProperty<UnityEngine.CollisionDetectionMode2D>(reader);
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