using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public Navy Navy;

    private void Start()
    {
        Navy = new Navy();
    }


    public void CreateShips()
    {
        var ship1 = Navy.CreateShip("Hot Topic", "Perdition Beam Titan");
        var ship2 = Navy.CreateShip("McDonalds", "Super Space Control Ship");
        var ship3 = Navy.CreateShip("New York", "Battleship");
        var ship4 = Navy.CreateShip("Milan", "Battleship");
        var ship5 = Navy.CreateShip("Tokyo", "Battleship");
        var ship6 = Navy.CreateShip("Dicks Sporting Goods", "Heavy Cruiser Improved");
        var ship7 = Navy.CreateShip("Big Five", "Heavy Cruiser Improved");
        var ship8 = Navy.CreateShip("Peter Piper Pizza", "Heavy Cruiser Improved");

        Navy.Test = true;

    }

    public void MoveShipsIntoFleet()
    {
        Navy.CreateFleet("Avenger");
        //_navy.ChangeFleet(ship1, "Avenger");
        //_navy.ChangeFleet(ship2, "Avenger");
        //_navy.ChangeFleet(ship3, "Avenger");
        //_navy.ChangeFleet(ship4, "Avenger");
        //_navy.ChangeFleet(ship5, "Avenger");
        //_navy.ChangeFleet(ship6, "Avenger");
        //_navy.ChangeFleet(ship7, "Avenger");
        //_navy.ChangeFleet(ship8, "Avenger");
    }

    public void PrintFleet()
    {
        Debug.Log("Printing Fleet...");

        var fleet = Navy.Fleets["Avenger"];

        int shipCount = 1;

        foreach (var ship in fleet)
        {
            Debug.Log("Ship " + shipCount.ToString());
            Debug.Log(ship.Name);

            shipCount++;
        }
    }
}
