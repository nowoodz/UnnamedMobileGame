using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void Save(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(GetPath(), FileMode.Create);

        formatter.Serialize(fileStream, data);
        fileStream.Close();

    }

    public static GameData Load()
    {
        if (!File.Exists(GetPath()))
        {
            GameData emptyData = new GameData();
            Save(emptyData);
            return emptyData;
        }
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(GetPath(), FileMode.Open);
        GameData data = formatter.Deserialize(fileStream) as GameData;
        fileStream.Close();

        return data;
    }

    public static void ResetData()
    {
        // Delete the existing save file if it exists
        if (File.Exists(GetPath()))
        {
            File.Delete(GetPath());
        }

        // Create and save a new, empty GameData file
        GameData newData = new GameData();
        Save(newData);
    }

    private static string GetPath()
    {
        return Application.persistentDataPath + "/data.qnd";
    }
}
