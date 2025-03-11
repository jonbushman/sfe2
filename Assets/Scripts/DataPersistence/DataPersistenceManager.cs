using UnityEngine;
using System.Linq;

using System.Collections.Generic;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Data Persistance Manager in the scene.");
        }
        Instance = this;
    }

    private void Start()
    {
        this._dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();

    }

    public void NewGame()
    {
        this._gameData = new GameData();
    }

    public void LoadGame()
    {
        if (this._gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();
        }
    }

    public void SaveGame()
    {

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> _dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>( _dataPersistenceObjects );
    }
}
