using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoad 
{
    public static void SaveData(DataBase character)
    {
        //Making binary format for saving
        BinaryFormatter formatter = new BinaryFormatter();
        // Path for saving  (if you want to find Application.persistentDataPath address in your computer
        // Use Debug.Log(Application.persistentDataPath);
        string path = Application.persistentDataPath + "/Pokemon.game";

        FileStream stream = new FileStream(path, FileMode.Create);

        //Store the data
        Data charData = new Data(character);
        // Saving with Serializing
        formatter.Serialize(stream, charData);
        stream.Close();
    }

    public static Data LoadData()
    {
        //Get Data from the Saving file
        string path = Application.persistentDataPath + "/Pokemon.game";
        // if Saving file exist
        if (File.Exists(path))
        {            
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //Deserialize the saving file -> making as readable file
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
