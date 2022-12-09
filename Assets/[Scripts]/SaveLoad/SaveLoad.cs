using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoad 
{
    public static void SaveData(DataBase character)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Pokemon.game";

        FileStream stream = new FileStream(path, FileMode.Create);

        Data charData = new Data(character);

        formatter.Serialize(stream, charData);
        stream.Close();
    }

    public static Data LoadData()
    {
        string path = Application.persistentDataPath + "/Pokemon.game";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data data = formatter.Deserialize(stream) as Data;

            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }
}
