using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void Save(GameData data)
    {
        using (FileStream fileStream = new FileStream(GetPath(), FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, data);
        }
    }

    public static GameData Load()
    {
        if (!File.Exists(GetPath()))
        {
            GameData emptyData = new GameData();
            Save(emptyData);  // Create a new file if it doesn't exist
            return emptyData;
        }

        using (FileStream fileStream = new FileStream(GetPath(), FileMode.Open))
        {
            if (fileStream.Length == 0)
            {
                Debug.LogError("Save file is empty!");
                return new GameData(); // Return a new instance to avoid further errors
            }

            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(fileStream) as GameData;
        }
    }

    public static void ResetData()
    {
        if (File.Exists(GetPath()))
        {
            File.Delete(GetPath());
            Debug.Log("Existing save file deleted.");
        }

        GameData newData = new GameData();
        Save(newData);
        Debug.Log("New save file created.");
    }

    private static string GetPath()
    {
        return Application.persistentDataPath + "/data.bat";
    }
}
