using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    // In SAVE_PATH there must be a slash / than name, EXAMPLE /save_rotkvica
    private const string SAVE_PATH = "/save_files";
    
    public static void SaveData<T>(T data, string save_key)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        // gives you the data path
        string path = Application.persistentDataPath + SAVE_PATH;
        
        // makes a folder for data if it already exists than it just skips itself
        Directory.CreateDirectory(path);
        
        // WATCH OUT
        // In save key there must be a slash / than name EXAMPLE /rotkvica
        // ^^^^ NEMORA VISE ^^^^ -> moze ili "rotkvica" ili "/rotkvica"
        if (!save_key.StartsWith('/')) save_key = "/" + save_key;
        FileStream stream = new FileStream(path + save_key, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static T LoadData<T>(string save_key)
    {
        string path = Application.persistentDataPath + SAVE_PATH;
        
        // makes the default (empty) data of said type
        T data = default;
        
        // Checks if there exists said file
        if (!save_key.StartsWith('/')) save_key = "/" + save_key;
        if(File.Exists(path + save_key))
        {
            // returns (deserializes) the file at location
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path + save_key, FileMode.Open);
            data = (T)formatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            // Basically will always happen on start of the game
            Debug.LogError("Error: Save file not found in " + path + save_key +"\nreturned empty");
        }
        
        // (if possible) returns the filed out data, else its just empty
        return data;
    }
}