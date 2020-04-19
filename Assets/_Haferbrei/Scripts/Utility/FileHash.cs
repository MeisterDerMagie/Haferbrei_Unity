using System.IO;
using System.Security.Cryptography;

namespace Haferbrei{

//based on: http://csharphelper.com/blog/2018/04/calculate-hash-codes-for-a-file-in-c/
public class FileHash
{
    // The cryptographic service provider.
    private static SHA256 Sha256 = SHA256.Create();

    // Compute the file's hash.
    public static byte[] GetHashSha256(string filename)
    {
        using (FileStream stream = File.OpenRead(filename))
        {
            return Sha256.ComputeHash(stream);
        }
    }

    // Return a byte array as a sequence of hex values.
    public static string GetHashSha256AsString(string filename)
    {
        string result = "";
        foreach (byte b in GetHashSha256(filename)) result += b.ToString("x2");
        return result;
    }
}
}