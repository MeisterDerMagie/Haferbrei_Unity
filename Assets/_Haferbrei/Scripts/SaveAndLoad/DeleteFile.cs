using System.IO;

namespace Haferbrei{
public static class DeleteFile
{
    public static void Delete(string _filePath)
    {
        if (!File.Exists(_filePath)) return;
        
        File.Delete(_filePath);
    }
}
}