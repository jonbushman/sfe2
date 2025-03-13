using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string _fileName;

    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;
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
        this._dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
        this._dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();

    }

    public void NewGame()
    {
        this._gameData = new GameData();
    }

    public void LoadGame()
    {
        this._gameData = _dataHandler.Load();

        if (this._gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(ref _gameData);
        }
        Debug.Log("Loaded");
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref _gameData);
        }
        Debug.Log("Saved");

        _dataHandler.Save(_gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        //IEnumerable<IDataPersistence> _dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(PlayerManager.Instance.Players);
    }
}
