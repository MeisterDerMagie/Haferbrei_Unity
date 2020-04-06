using System;
using UnityEngine;

namespace Haferbrei{
public static class Version
{
    public static string currentVersion => Application.version;
    public static string lowestCompatibleVersion = "0.1";
}
}