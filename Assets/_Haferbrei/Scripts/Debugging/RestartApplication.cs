using UnityEngine;

namespace Haferbrei
{
public static class RestartApplication
{
    public static void Restart()
    {
        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
        Application.Quit();
    }
}
}