using System;
using UnityEngine;

namespace Haferbrei{
public static class HaferbreiVersion
{
    public static Version currentVersion => new Version(Application.version);
    public static Version lowestCompatibleVersion = new Version(0,0,1);

    public static bool IsCompatible(string _version)
    {
        var version = new Version(_version);
        return IsCompatible(version);
    }
    
    public static bool IsCompatible(Version _version)
    {
        int comparison = _version.CompareTo(lowestCompatibleVersion);
        return (comparison >= 0);
    }
}
}