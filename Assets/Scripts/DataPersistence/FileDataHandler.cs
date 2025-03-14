using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this._dataDirPath = dataDirPath;
        this._dataFileName = dataFileName;
    }

    public GameData Load()
    {
        var fullPath = Path.Combine(_dataDirPath, _dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occurred when trying to load data from file: " + fullPath + "\n" + e);
            }

        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        var fullPath = Path.Combine(_dataDirPath, _dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //var dataToStore = JsonUtility.ToJson(data, true);
            var dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occurred when trying to save date to file: " + fullPath + "\n" + e);
        }

    }

}
