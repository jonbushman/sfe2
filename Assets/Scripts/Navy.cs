using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Navy
{
    public List<Fleet> Fleets;
    public Fleet Unassigned;
    public List<Base> Bases;

    public Navy()
    {
        Fleets = new List<Fleet>();
        Unassigned = new Fleet();
        Bases = new List<Base>();
    }

    public Ship CreateShip(string name, string type)
    {
        if (!ShipDictionaryDep.AllShipTraits.ContainsKey(type)) return null;
        if (!ShipNameUnique(name)) return null;

        var newShip = new Ship() { Name = name, Type = type, Officers = new Officers() };

        newShip.Traits = ShipDictionaryDep.AllShipTraits[type];
        Unassigned.Ships.Add(newShip);

        return newShip;
    }

    public void DestroyShip(Ship ship)
    {
        foreach (var fleet in Fleets)
        {
            if (fleet.Ships.Contains(ship))
            {
                fleet.Ships.Remove(ship);
            }
        }
        if (Unassigned.Ships.Contains(ship))
        {
            Unassigned.Ships.Remove(ship);
        }
    }

    public bool ChangeFleet(Ship ship, string newFleetName) //this looks up the fleet name, but can just as easily pass in a Fleet class obj
    {
        var newFleet = Fleets.Where(x => x.Name == newFleetName).ToList().FirstOrDefault();
        if (newFleet == null) return false;

        if (Unassigned.Ships.Contains(ship))
        {
            Unassigned.Ships.Remove(ship);
            newFleet.Ships.Add(ship);
        }
        else
        {
            var oldFLeet = Fleets.Where(x => x.Ships.Contains(ship)).ToList().FirstOrDefault();
            if (oldFLeet == null)
            {
                return false;
            }
            else
            {
                oldFLeet.Ships.Remove(ship);
                newFleet.Ships.Add(ship) ;
                return true;
            }
        }

        return true;
    }

    public bool CreateFleet(string name)
    {
        if (Fleets.Where(x => x.Name == name).Count() > 0) return false; 

        Fleets.Add(new Fleet(name));

        return true;
    }

    public bool DisbandFleet(string name) //same with this. can add or change to use Fleet class param
    {
        var fleetToDisband = Fleets.Where(x => x.Name == name).ToList().FirstOrDefault();
        if (fleetToDisband == null) return false;

        foreach (Ship ship in fleetToDisband.Ships)
        {
            Unassigned.Ships.Add(ship);
        }
        Fleets.Remove(fleetToDisband);

        return true;
    }

    public void RenameFleet()
    {
        Debug.Log("renaming");
    }

    public bool ShipNameUnique(string name)
    {
        foreach (var ship in Unassigned.Ships)
        {
            if (ship.Name == name) return false;
        }

        foreach (var fleet in Fleets)
        {
            foreach (var ship in fleet.Ships)
            {
                if (ship.Name == name) return false;
            }
        }

        return true;
    }
}
