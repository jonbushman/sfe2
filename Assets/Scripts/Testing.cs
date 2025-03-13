using System.Linq;
using UnityEngine;

public class Testing : MonoBehaviour
{

    private void Start()
    {
    }

    public void CreateShips()
    {
        var ship1 = PlayerManager.Instance.ActivePlayer.Navy.CreateShip("Hot Topic", "Perdition Beam Titan");
        var ship2 = PlayerManager.Instance.ActivePlayer.Navy.CreateShip("McDonalds", "Super Space Control Ship");
        var ship3 = PlayerManager.Instance.ActivePlayer.Navy.CreateShip("New York", "Battleship");
        var ship4 = PlayerManager.Instance.ActivePlayer.Navy.CreateShip("Milan", "Battleship");
        var ship5 = PlayerManager.Instance.ActivePlayer.Navy.CreateShip("Tokyo", "Battleship");
        var ship6 = PlayerManager.Instance.ActivePlayer.Navy.CreateShip("Dicks Sporting Goods", "Heavy Cruiser Improved");
        var ship7 = PlayerManager.Instance.ActivePlayer.Navy.CreateShip("Big Five", "Heavy Cruiser Improved");
        var ship8 = PlayerManager.Instance.ActivePlayer.Navy.CreateShip("Peter Piper Pizza", "Heavy Cruiser Improved");

        PlayerManager.Instance.ActivePlayer.Navy.RenameFleet();

    }

    public void MoveShipsIntoFleet()
    {
        var newFleetName = PlayerManager.Instance.ActivePlayer.Name + " - Avenger";
        PlayerManager.Instance.ActivePlayer.Navy.CreateFleet(newFleetName);
        var unAss = PlayerManager.Instance.ActivePlayer.Navy.Unassigned;
        var fleetToMove = new Fleet();
        fleetToMove.Ships = unAss.Ships;
        for (int i = 0; i < fleetToMove.Ships.Count; i++)
        {
            PlayerManager.Instance.ActivePlayer.Navy.ChangeFleet(fleetToMove.Ships[i], newFleetName);
        }
        //Player1.Navy.ChangeFleet(Player1.Navy.Unassigned, "Avenger");
    }

    public void PrintFleet()
    {
        Debug.Log("Printing Fleet...");

        var fleet = PlayerManager.Instance.ActivePlayer.Navy.Fleets.Where(x => x.Name == "Avenger").ToList().FirstOrDefault();

        foreach (var ship in fleet.Ships)
        {
            Debug.Log(ship.Name);
        }
    }
}
