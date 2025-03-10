//keep ship names unique

using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public string Name;
    public string Type;
    public Officers Officers;

    private ShipTraits _traits;

    public ShipTraits GetTraits()
    {
        return _traits;
    }

    public void SetTraits(ShipTraits traits)
    {
        _traits = traits;
    }

    public bool AddOfficer(string office, string name)
    {
        switch(office)
        {
            case "captain":
                Officers.Captain = name;
                break;
            case "engineer":
                Officers.Engineer = name;
                break;
            case "weapons":
                Officers.Weapons = name;
                break;
            case "science":
                Officers.Science = name;
                break;
            case "navigator":
                Officers.Navigator = name;
                break;
            case "crew":
                Officers.Crew = name;
                break;
            case "medical":
                Officers.Medical = name;
                break;
            case "communications":
                Officers.Communications = name;
                break;
            default:
                return false;
        }
        return true;
    }

    public bool RemoveOfficer(string office)
    {
        switch (office)
        {
            case "captain":
                Officers.Captain = "";
                break;
            case "engineer":
                Officers.Engineer = "";
                break;
            case "weapons":
                Officers.Weapons = "";
                break;
            case "science":
                Officers.Science = "";
                break;
            case "navigator":
                Officers.Navigator = "";
                break;
            case "crew":
                Officers.Crew = "";
                break;
            case "medical":
                Officers.Medical = "";
                break;
            case "communications":
                Officers.Communications = "";
                break;
            default:
                return false;
        }
        return true;
    }
}

public class ShipTraits
{
    public string Abbreviation;
    public string Type;
    public string SubType;
    public int TechLevel;
    public int Speed;
    public int Size;
    public int Bpv;
    public int Command;
    public int CommandCrippled;
    public int ScanningStrength;
    public int Attack;
    public int AttackCripplped;
    public int Defense;
    public int DefenseCripplped;
    public int Fighters;
    public float Launch;
    public float Bombard;
    public float PointDefense;
    public int Cargo;
    public List<string> Traits;
    public List<string> VersionHistory;

    public ShipTraits()
    {
    }

    public ShipTraits(string abbreviation, string type, string subType, int techLevel, int speed, int size, int bpv,
        int command, int commandCrippled, int scanningStrength, int attack, int attackCrippled, int defense, int defenseCrippled,
        int fighter, float launch, float bombard, float pointDefense, int cargo, List<string> traits, List<string> versionHistory)
    {
        Abbreviation = abbreviation;
        Type = type;
        SubType = subType;
        TechLevel = techLevel;
        Speed = speed;
        Size = size;
        Bpv = bpv;
        Command = command;
        CommandCrippled = commandCrippled;
        ScanningStrength = scanningStrength;
        Attack = attack;
        AttackCripplped = attackCrippled;
        Defense = defense;
        DefenseCripplped = defenseCrippled;
        Fighters = fighter;
        Launch = launch;
        Bombard = bombard;
        PointDefense = pointDefense;
        Cargo = cargo;
        Traits = traits;
        VersionHistory = versionHistory;
    }
}

public class Officers
{
    public string Captain;
    public string Engineer;
    public string Weapons;
    public string Science;
    public string Navigator;
    public string Crew;
    public string Medical;
    public string Communications;

    public Officers(string captain = "", string engineer = "", string weapons = "", string science = "", string navigator = "", string crew = "", 
        string medical = "", string communications = "")
    {
        Captain = captain;
        Engineer = engineer;
        Weapons = weapons;
        Science = science;
        Navigator = navigator;
        Crew = crew;
        Medical = medical;
        Communications = communications;
    }

}