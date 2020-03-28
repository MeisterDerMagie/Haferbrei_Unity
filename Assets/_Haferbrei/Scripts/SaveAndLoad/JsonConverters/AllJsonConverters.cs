using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Haferbrei.JsonConverters{
public static class AllJsonConverters
{
    public static JsonConverter[] GetAllJsonConverters()
    {
        var converters = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(JsonConverter)) && !t.IsAbstract)
            .Select(t => (JsonConverter)Activator.CreateInstance(t));
        
        return converters.ToArray();
    }
}
}