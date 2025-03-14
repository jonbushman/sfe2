using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public TMP_InputField TurnInput;
    public TMP_InputField PlayerNameInput;

    private GameData _allData;
    private PlayerData _currentPlayer;

    private void Start()
    {
        _allData = SFEManager.Instance.Data;
    }

    public void InitializePlayer(int turnNumber, string playerName)
    {
        if (!_allData.Data.ContainsKey(turnNumber))
        {
            _allData.Data.Add(turnNumber, new Dictionary<string, PlayerData>());
        }
        if (!_allData.Data[turnNumber].ContainsKey(playerName))
        {
            _allData.Data[turnNumber].Add(playerName, new PlayerData());
        }
    }

    public void ChoosePlayer()
    {
        if (string.IsNullOrEmpty(PlayerNameInput.text))
        {
            Debug.LogError("must enter a player name");
        }
        if (string.IsNullOrEmpty(TurnInput.text))
        {
            Debug.LogError("must enter a turn number");
        }
        var turnNumber = int.Parse(TurnInput.text);
        var playerName = PlayerNameInput.text;

        InitializePlayer(turnNumber, playerName);
        
        _currentPlayer = SFEManager.Instance.Data.Data[turnNumber][playerName];
    }

    public void CreateShips()
    {
        var ship1 = _currentPlayer.Navy.CreateShip("Hot Topic", "Perdition Beam Titan");
        var ship2 = _currentPlayer.Navy.CreateShip("McDonalds", "Super Space Control Ship");
        var ship3 = _currentPlayer.Navy.CreateShip("New York", "Battleship");
        var ship4 = _currentPlayer.Navy.CreateShip("Milan", "Battleship");
        var ship5 = _currentPlayer.Navy.CreateShip("Tokyo", "Battleship");
        var ship6 = _currentPlayer.Navy.CreateShip("Dicks Sporting Goods", "Heavy Cruiser Improved");
        var ship7 = _currentPlayer.Navy.CreateShip("Big Five", "Heavy Cruiser Improved");
        var ship8 = _currentPlayer.Navy.CreateShip("Peter Piper Pizza", "Heavy Cruiser Improved");

        _currentPlayer.Navy.RenameFleet();

    }

    public void MoveShipsIntoFleet()
    {
        var newFleetName = "Avenger";
        _currentPlayer.Navy.CreateFleet(newFleetName);
        var unAss = _currentPlayer.Navy.Unassigned;
        var fleetToMove = new Fleet();
        fleetToMove.Ships = unAss.Ships;
        for (int i = 0; i < fleetToMove.Ships.Count; i++)
        {
            _currentPlayer.Navy.ChangeFleet(fleetToMove.Ships[i], newFleetName);
        }
        //Player1.Navy.ChangeFleet(Player1.Navy.Unassigned, "Avenger");
    }

    public void PrintFleet(string fleetName)
    {
        Debug.Log("Printing Fleet...");

        var fleet = _currentPlayer.Navy.Fleets.Where(x => x.Name == fleetName).ToList().FirstOrDefault();

        foreach (var ship in fleet.Ships)
        {
            Debug.Log(ship.Name);
        }
    }
}
