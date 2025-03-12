using System.Collections.Generic;
using UnityEngine;

public class Navy : MonoBehaviour, IDataPersistence
{
    public Dictionary<string, List<Ship>> Fleets;
    public Dictionary<string, string> FleetLocations;
    public List<Ship> UnassignedShips;
    public bool Test;

    public Navy()
    {
        Fleets = new Dictionary<string, List<Ship>>();
        FleetLocations = new Dictionary<string, string>();
        UnassignedShips = new List<Ship>();
    }

    private void Start()
    {

    }

    public Ship CreateShip(string name, string type)
    {
        if (!ShipDictionary.AllShipTraits.ContainsKey(type)) return null;
        if (!ShipNameUnique(name)) return null;

        var newShip = new Ship() { Name = name, Type = type, Officers = new Officers() };

        newShip.Traits = ShipDictionary.AllShipTraits[type];
        UnassignedShips.Add(newShip);

        return newShip;
    }

    public void DestroyShip(Ship ship)
    {
        foreach (var fleet in Fleets)
        {
            if (fleet.Value.Contains(ship))
            {
                fleet.Value.Remove(ship);
            }
        }
        if (UnassignedShips.Contains(ship))
        {
            UnassignedShips.Remove(ship);
        }
    }

    public bool ChangeFleet(Ship ship, string newFleet)
    {
        if (!Fleets.ContainsKey(newFleet)) return false;

        if (UnassignedShips.Contains(ship))
        {
            UnassignedShips.Remove(ship);
            Fleets[newFleet].Add(ship);
        }
        else
        {
            foreach (var kvp in Fleets)
            {
                if (kvp.Value.Contains(ship))
                {
                    Fleets[kvp.Key].Remove(ship);
                    Fleets[newFleet].Add(ship);
                    return true;
                }
            }
        }

        return true;
    }

    public bool ChangeFleet(List<Ship> ships, string newFleet)
    {
        if (!Fleets.ContainsKey(newFleet)) return false;

        var shipsToMove = new List<Ship>(ships);

        foreach (var ship in shipsToMove)
        {
            if (UnassignedShips.Contains(ship))
            {
                UnassignedShips.Remove(ship);
                Fleets[newFleet].Add(ship);
            }
            else
            {
                foreach (var kvp in Fleets)
                {
                    if (kvp.Value.Contains(ship))
                    {
                        Fleets[kvp.Key].Remove(ship);
                        Fleets[newFleet].Add(ship);
                        return true;
                    }
                }
            }
        }

        return true;
    }

    public bool CreateFleet(string name)
    {
        if (Fleets.ContainsKey(name)) return false;

        Fleets.Add(name, new List<Ship>());

        return true;
    }

    public bool DisbandFleet(string name)
    {
        if (!Fleets.ContainsKey(name)) return false;

        foreach (var ship in Fleets[name])
        {
            UnassignedShips.Add(ship);
        }
        Fleets.Remove(name);

        return true;
    }

    public void RenameFleet()
    {
        Debug.Log("renaming");
    }

    public bool ShipNameUnique(string name)
    {
        foreach (var ship in UnassignedShips)
        {
            if (ship.Name == name) return false;
        }

        foreach (var kvp in Fleets)
        {
            foreach (var ship in kvp.Value)
            {
                if (ship.Name == name) return false;
            }
        }

        return true;
    }

    public void SaveData(ref GameData data)
    {
        data.FleetLocations = FleetLocations;
        data.Fleets = Fleets;
        data.UnassignedShips = UnassignedShips;
        data.Test = Test;
    }

    public void LoadData(GameData data)
    {
        FleetLocations = data.FleetLocations;
        Fleets = data.Fleets;
        UnassignedShips = data.UnassignedShips;
    }
}
