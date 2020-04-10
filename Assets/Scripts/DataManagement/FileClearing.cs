using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class FileClearing : Editor
{
    [MenuItem("Tools/Clear Saves")]
    public static void Clean()
    {
        string path = FileHelper.GetPath().Replace("/Save/", "");
        System.IO.DirectoryInfo di = new DirectoryInfo(path);

        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            dir.Delete(true);
        }
        Debug.Log("Cleared saves");
    }
}
