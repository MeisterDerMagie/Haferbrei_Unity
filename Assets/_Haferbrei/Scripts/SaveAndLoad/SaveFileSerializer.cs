using System;
using System.Collections.Generic;
using FullSerializer;
using Haferbrei.JsonConverter;
using Wichtel.Extensions;

namespace Haferbrei{
public static class SaveFileSerializer
{
    private static string encryptionPassword = "Good job, you managed to hack the password! Have fun reading the SaveFile. :)";
    
    public static string Serialize(string identifier, Type storageType, object instance, bool encryptSaveFile)
    {
        var serializer = new fsSerializer();
        fsData data;
        GetJsonConverters(ref serializer);

        //serialize data
        serializer.TrySerialize(typeof(SaveFile_BodyData), instance, out data).AssertSuccessWithoutWarnings();                      // 5.1 Serialize data
        string serializedData = (encryptSaveFile) ? fsJsonPrinter.CompressedJson(data) : fsJsonPrinter.PrettyJson(data);            // 5.2 Create Json string
        if(encryptSaveFile) serializedData = StringCompression.Compress(serializedData);                                            // 5.3 Compress Json string
        if(encryptSaveFile) serializedData = StringCipher.Encrypt(serializedData, encryptionPassword);          // 5.4 Encrpyt Json string

        //write identifier
        string identifierStart = "<" + identifier + ">\n";
        string identifierEnd = "\n</" + identifier + ">\n";
        serializedData = serializedData.Insert(0, identifierStart);
        serializedData += identifierEnd;
        
        return serializedData;
    }

    public static void Deserialize<T>(string identifier, string fileContent, bool saveFileIsEncrypted, ref T instance)
    {
        var serializer = new fsSerializer();
        GetJsonConverters(ref serializer);
        
        string serializedData = fileContent.Substring("<" + identifier + ">", "</" + identifier + ">");

        if (saveFileIsEncrypted) serializedData = StringCipher.Decrypt(serializedData, encryptionPassword);    // 0.2 Decrypt json string
        if (saveFileIsEncrypted) serializedData = StringCompression.Decompress(serializedData);                                     // 0.3 Decompress json string
        fsData parsedData = fsJsonParser.Parse(serializedData);                                                                     // 0.4 Create data from json string
        serializer.TryDeserialize(parsedData, ref instance);                                                                        // 0.5 Deserialize json
    }
    
    private static void GetJsonConverters(ref fsSerializer _serializer)
    {
        var converters = new List<fsConverter>(AllJsonConverters.GetAllJsonConverters());
        foreach (var converter in converters)
        {
            _serializer.AddConverter(converter);
        }
    }
}
}