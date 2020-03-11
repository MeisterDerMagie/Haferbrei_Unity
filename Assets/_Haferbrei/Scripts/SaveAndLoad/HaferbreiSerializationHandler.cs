using System;
using UnityEngine;

namespace Haferbrei{
public static class HaferbreiSerializationHandler
{
    
    //ACHTUNG: !! Das funktioniert so nicht, weil z.B. eine List<float> trotzdem nicht deserialisiert werden kann.
    
    public static object HandleSpecialSerialization(object _objectToSerialize)
    {
        //-- Error for non serializable types --
        if(_objectToSerialize is GameObject) Debug.LogError("Achtung: Du versuchst eine GameObject Referenz zu speichern, das ist nicht möglich. Speichere stattdessen eine guid-based-reference!");
        //-- --

        //-- Convert to other type --
        if (_objectToSerialize is ScriptableObject scriptableObject)
        {
            var scriptableObjectGuid = ScriptableObjectReferences.Instance.saveableScriptableObjectsReferences.ResolveReference(scriptableObject);
            return new SerializedScriptableObjectReference(scriptableObjectGuid);
        }

        if (_objectToSerialize is GuidReference)
        {
            //HIER DEN CODE, MIT DEM MAN GUIDREFERENCES SPEICHERN KANN
        }
        //-- --
        
        

        return _objectToSerialize;
    }

    public static object HandleSpecialDeserialization(object _objectToDeserialize)
    {
        
        //-- Convert from other type --
        if (_objectToDeserialize is double) return Convert.ToSingle(_objectToDeserialize);
        if (_objectToDeserialize is long)   return Convert.ToInt32(_objectToDeserialize);

        if (_objectToDeserialize is SerializedScriptableObjectReference serializedScriptableObjectReference)
        {
            return ScriptableObjectReferences.Instance.saveableScriptableObjectsReferences.ResolveGuid(serializedScriptableObjectReference.guid);
        }
        //-- --

        return _objectToDeserialize;
    }
    
    public struct SerializedScriptableObjectReference
    {
        public Guid guid;

        public SerializedScriptableObjectReference(Guid _guid) => guid = _guid;
    }
}
}