using System;
using System.IO;
using Doozy.Engine.UI;

namespace Haferbrei{
public static class DeleteFile
{
    public static void Delete(string _filePath)
    {
        if (!File.Exists(_filePath)) return;

        try
        {
            File.Delete(_filePath);
        }
        catch (Exception e)
        {
            UIPopup popup = UIPopup.GetPopup("FehlerBeimLoeschenDesSpielstands");
            popup.Show();
        }
    }
}
}