using UnityEngine;

public class SFEManager : MonoBehaviour
{
    private Player _player;
    private WindowManager _windowManager;

    private void Start()
    {
        _player = GetComponent<Player>();
        _player.Navy = GetComponent<Navy>();
        _windowManager = GetComponent<WindowManager>();

        ShipDictionary.LoadFromJson();

        CreateDummyFleet();
        PrintFleet();
        Save();
        //Load();
    }

    private void Save()
    {

    }

    private void Load()
    {
        
    }


    #region Debug Methods
    private void CreateDummyFleet()
    {
        _player.Navy.CreateFleet("Avenger");

        var ship = _player.Navy.CreateShip("Hot Topic", "Perdition Beam Titan");
        _player.Navy.ChangeFleet(ship, "Avenger");

        ship = _player.Navy.CreateShip("McDonalds", "Super Space Control Ship");
        _player.Navy.ChangeFleet(ship, "Avenger");

        ship = _player.Navy.CreateShip("New York", "Battleship");
        _player.Navy.ChangeFleet(ship, "Avenger");

        ship = _player.Navy.CreateShip("Milan", "Battleship");
        _player.Navy.ChangeFleet(ship, "Avenger");

        ship = _player.Navy.CreateShip("Tokyo", "Battleship");
        _player.Navy.ChangeFleet(ship, "Avenger");

        ship = _player.Navy.CreateShip("Dicks Sporting Goods", "Heavy Cruiser Improved");
        _player.Navy.ChangeFleet(ship, "Avenger");

        ship = _player.Navy.CreateShip("Big Five", "Heavy Cruiser Improved");
        _player.Navy.ChangeFleet(ship, "Avenger");

        ship = _player.Navy.CreateShip("Peter Piper Pizza", "Heavy Cruiser Improved");
        _player.Navy.ChangeFleet(ship, "Avenger");
    }

    private void PrintFleet()
    {
        Debug.Log("Printing Fleet...");

        var fleet = _player.Navy.Fleets["Avenger"];

        int shipCount = 1;

        foreach (var ship in fleet)
        {
            Debug.Log("Ship " + shipCount.ToString());
            Debug.Log(ship.Name);

            shipCount++;
        }
    }

    #endregion
}
