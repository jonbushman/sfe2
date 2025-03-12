using UnityEngine;

public class Testing : MonoBehaviour
{
    public Player Player1;

    private void Start()
    {
    }


    public void CreateShips()
    {
        var ship1 = Player1.Navy.CreateShip("Hot Topic", "Perdition Beam Titan");
        var ship2 = Player1.Navy.CreateShip("McDonalds", "Super Space Control Ship");
        var ship3 = Player1.Navy.CreateShip("New York", "Battleship");
        var ship4 = Player1.Navy.CreateShip("Milan", "Battleship");
        var ship5 = Player1.Navy.CreateShip("Tokyo", "Battleship");
        var ship6 = Player1.Navy.CreateShip("Dicks Sporting Goods", "Heavy Cruiser Improved");
        var ship7 = Player1.Navy.CreateShip("Big Five", "Heavy Cruiser Improved");
        var ship8 = Player1.Navy.CreateShip("Peter Piper Pizza", "Heavy Cruiser Improved");

        Player1.Navy.RenameFleet();

    }

    public void MoveShipsIntoFleet()
    {
        Player1.Navy.CreateFleet("Avenger");

        Player1.Navy.ChangeFleet(Player1.Navy.UnassignedShips, "Avenger");
    }

    public void PrintFleet()
    {
        Debug.Log("Printing Fleet...");

        var fleet = Player1.Navy.Fleets["Avenger"];

        foreach (var ship in fleet)
        {
            Debug.Log(ship.Name);
        }
    }
}
