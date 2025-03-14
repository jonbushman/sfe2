using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SFEManager : MonoBehaviour
{
    [SerializeField] private string _fileName;
    public GameData Data;
    private FileDataHandler _dataHandler;

    public PlayerData CurrentPlayerData;
    
    public static SFEManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one SFEManager in the scene.");
        }
        Instance = this;
    
        ShipDictionaryDep.LoadFromJson();
    }

    #region Save/Load Methods
    private void Start()
    {
        this._dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
        LoadGame();
    }

    public void NewGame()
    {
        this.Data = new GameData();
    }

    public void LoadGame()
    {
        this.Data = _dataHandler.Load();

        if (this.Data == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();
        }

        //foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        //{
        //    dataPersistenceObj.LoadData(ref _gameData);
        //}

        Debug.Log("Loaded");
    }

    public void SaveGame()
    {
        //foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        //{
        //    dataPersistenceObj.SaveData(ref _gameData);
        //}
        Debug.Log("Saved");

        _dataHandler.Save(Data);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    #endregion

    #region Data Accessor Methods
    public Dictionary<int, PlayerData> GetPlayerData(string player)
    {
        var data = new Dictionary<int, PlayerData>();
        foreach (var turn in Data.Data)
        {
            if (turn.Value.ContainsKey(player))
            {
                data.Add(turn.Key, turn.Value[player]);
            }
        }
        return data;
    }

    public PlayerData GetPlayerData(string player, int turn)
    {
        if (Data.Data.ContainsKey(turn))
        {
            if (Data.Data[turn].ContainsKey(player))
            {
                return Data.Data[turn][player];
            }
        }
        return null;
    }

    public Dictionary<string, PlayerData> GetTurnData(int turn)
    {
        return null;
    }

    public List<string> GetPlayerNames()
    {
        var playerNames = new List<string>();
        foreach (var turn in Data.Data)
        {
            foreach (var player in turn.Value)
            {
                if (!playerNames.Contains(player.Key)) playerNames.Add(player.Key);
            }
        }
        return playerNames;
    }
    public List<string> GetPlayerNames(int turnNumber)
    {
        var playerNames = new List<string>();
        
        foreach (var player in Data.Data[turnNumber])
        {
            if (!playerNames.Contains(player.Key)) playerNames.Add(player.Key);
        }
        
        return playerNames;
    }
    #endregion


}
