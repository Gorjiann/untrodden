using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveLevelData(PlayerStats sets)
    {
        DataStorager data = LoadLevelData() ?? new DataStorager(sets);
        data.donebools[sets.currentlevel] = true;

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/levels.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static DataStorager LoadLevelData()
    {
        string path = Application.persistentDataPath + "/levels.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DataStorager data = formatter.Deserialize(stream) as DataStorager;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

}
