using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveAndLoad
{
    public static void Save(DataToSave data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "SaveData";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static DataToSave Load()
    {
        string path = Application.persistentDataPath + "SaveData";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataToSave data = formatter.Deserialize(stream) as DataToSave;
            return data;

        }
        else
        {
            Debug.Log("nothing found");
            return null;
        }
    }
}
