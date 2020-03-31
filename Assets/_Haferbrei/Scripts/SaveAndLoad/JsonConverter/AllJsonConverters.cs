﻿using System;
using System.Collections.Generic;
using System.Linq;
using FullSerializer;
//using Newtonsoft.Json;
using UnityEngine;

namespace Haferbrei.JsonConverter{
public static class AllJsonConverters
{
    public static fsConverter[] GetAllJsonConverters()
    {
        var converters = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(fsConverter)) && !t.IsAbstract && t.Namespace == "Haferbrei.JsonConverter")
            .Select(t => (fsConverter)Activator.CreateInstance(t));
        
        return converters.ToArray();
    }
}
}