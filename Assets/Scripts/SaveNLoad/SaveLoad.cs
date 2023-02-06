using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoad
{


    public static void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameData.save";

        FileStream stream = new FileStream(path, FileMode.Create);

        SavedData savedData = new SavedData();

        formatter.Serialize(stream, savedData);

        stream.Close();
    }

    public static SavedData LoadData()
    {
        string path = Application.persistentDataPath + "/gameData.save";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavedData data = formatter.Deserialize(stream) as SavedData;

            stream.Close();
            
            return data;

        } else
        {
            Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }
}
