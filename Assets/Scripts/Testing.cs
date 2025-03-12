using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public Navy MyNavy;

    private void Start()
    {
    }


    public void CreateShips()
    {
        var ship1 = MyNavy.CreateShip("Hot Topic", "Perdition Beam Titan");
        var ship2 = MyNavy.CreateShip("McDonalds", "Super Space Control Ship");
        var ship3 = MyNavy.CreateShip("New York", "Battleship");
        var ship4 = MyNavy.CreateShip("Milan", "Battleship");
        var ship5 = MyNavy.CreateShip("Tokyo", "Battleship");
        var ship6 = MyNavy.CreateShip("Dicks Sporting Goods", "Heavy Cruiser Improved");
        var ship7 = MyNavy.CreateShip("Big Five", "Heavy Cruiser Improved");
        var ship8 = MyNavy.CreateShip("Peter Piper Pizza", "Heavy Cruiser Improved");

        MyNavy.Test = true;

        MyNavy.RenameFleet();

    }

    public void MoveShipsIntoFleet()
    {
        MyNavy.CreateFleet("Avenger");

        MyNavy.ChangeFleet(MyNavy.UnassignedShips, "Avenger");
    }

    public void PrintFleet()
    {
        Debug.Log("Printing Fleet...");

        var fleet = MyNavy.Fleets["Avenger"];

        foreach (var ship in fleet)
        {
            Debug.Log(ship.Name);
        }
    }
}
