using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    #region Navy
    public Dictionary<string, List<Ship>> Fleets;
    public Dictionary<string, string> FleetLocations;
    public List<Ship> UnassignedShips;
    public bool Test;
    #endregion

    public List<Colony> Colonies;
    //map knowledge

    #region Properties
    public int TechLevel;
    public List<string> IntelAdvancements;
    public Dictionary<string, string> Diplomacy;
    public Dictionary<string, string> Traits;
    public Dictionary<string, string> RacialBonuses;
    public Dictionary<string, string> Technologies;
    public int TurnNumber;
    #endregion

    public GameData()
    {
        this.TechLevel = 0;
        this.TurnNumber = 1;
    }
}
