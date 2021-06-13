using UnityEngine;
using System.IO;
using System;

/* When open file directly in directory,
 * open it with visual studio
 * ctrl + k ctrl + f to indent the file.
 * 
 * File Location : C:/Users/%User%/AppData/LocalLow/<company>/<game>/
 */

public class JSONManager : MonoBehaviour
{
    public GameData data;

    //private string directory = "/StreamingAssets/JSON/";
    private string directory = "/saves/";
    private string fileName = "file_data.json";

    public static JSONManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        LoadData();
    }

    public void SaveData()
    {
        //string pathDirectory = Utils.GetDirectory(directory);
        //if (!Directory.Exists(pathDirectory)) { Debug.LogWarning(pathDirectory + " does not exits ! "); return; }
        string pathDirectory = Utils.GetPersistentDirectory(directory);
        if (!Directory.Exists(pathDirectory))
        {
            Directory.CreateDirectory(pathDirectory);
        }

        // string path = Utils.GetFile(directory, fileName);
        string path = Utils.GetPersistentFile(directory, fileName);
        FileStream stream = new FileStream(path, FileMode.Create);
        StreamWriter writer = new StreamWriter(stream);
        string json = JsonUtility.ToJson(data);
        writer.Write(json);
        writer.Flush();
        writer.Close();
        stream.Close();
    }

    public void LoadData()
    {
        // string path = Utils.GetFile(directory, fileName);
        string path = Utils.GetPersistentFile(directory, fileName);
        if (!File.Exists(path)) { Debug.LogWarning("File does not exist"); return; }

        FileStream stream = new FileStream(path, FileMode.Open);
        StreamReader reader = new StreamReader(stream);
        string json = reader.ReadToEnd();
        data = JsonUtility.FromJson<GameData>(json);
        reader.Close();
        stream.Close();
    }
}

[System.Serializable]
public class GameData
{
    public bool isTutoFinished = false;
    public float score;
}

