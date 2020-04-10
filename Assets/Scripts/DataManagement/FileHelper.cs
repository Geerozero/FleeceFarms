using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileHelper
{
    public static string GetPath()
    {
        /*---Gets path for data to be saved---*/
        return Application.persistentDataPath + "/Ver" + Application.version + "/Save/";
    }

    public static void SaveDataFile(object data, string file)
    {
        string path = GetPath();
        string filePath = path + file + ".dat";

        /*---Creates directory if direcctory does not exist---*/
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        if (!File.Exists(filePath))
        {
            FileStream newFile = File.Create(filePath);
            newFile.Close();
        }

        /*---Serializes data for the file---*/
        File.WriteAllBytes(filePath, SerializeHelper.Serialize(data));
    }

    public static T LoadDataFile<T>(string file) where T : class, new()
    {
        string path = GetPath();
        string filePath = path + file + ".dat";

        if (!Directory.Exists(path))
        {
            return null;
        }

        if (!File.Exists(filePath))
        {
            return null;
        }

        /*---Gets data from file and DeSerializes it---*/
        byte[] data = File.ReadAllBytes(filePath);
        if (data == null || data.Length <= 0)
            return null;
        T returnVal = (T)SerializeHelper.DeSerialize(data);
        if (returnVal == null)
            return new T();

        return returnVal;
    }
}