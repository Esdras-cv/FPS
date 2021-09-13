using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System;

public static class FileManager
{
    public static bool WriteToFile(string fileName, string fileContent)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            File.WriteAllText(fullPath, fileContent);
            return true;
        }
        catch(Exception e)
        {
            Debug.LogError($"Falha ao escrever arquivo {fullPath} com exceção {e}");
            return false;
        }
    }

    public static bool LoadFromFile(string fileName, out string result)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            result = File.ReadAllText(fullPath);
            return true;
        }
        catch(Exception e)
        {
            Debug.LogError($"Falha ao ler o arquivo {fullPath} com a exceção {e}");
            result = "";
            return false;
        }
    }
}
