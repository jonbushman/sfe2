//should add protection against name duplicates

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Objects
    public Navy Navy;
    public List<Colony> Colonies;
    #endregion

    #region Properties
    public int TechLevel;
    public List<string> IntelAdvancements;
    public Dictionary<string, string> Diplomacy;
    public Dictionary<string, string> Traits;
    public Dictionary<string, string> RacialBonuses;
    public Dictionary<string, string> Technologies;
    #endregion

    public Player()
    {
        Navy = new Navy();
        Colonies = new List<Colony>();
        IntelAdvancements = new List<string>();
        Diplomacy = new Dictionary<string, string>();
        Traits = new Dictionary<string, string>();
        RacialBonuses = new Dictionary<string, string>();
        Technologies = new Dictionary<string, string>();
    }

    //public void SaveData(ref GameData data)
    //{
    //    var savePlayer = new PlayerData();
    //    savePlayer.Navy = Navy;
    //    savePlayer.Colonies = Colonies;
    //    savePlayer.Name = Name;
    //    savePlayer.TechLevel = TechLevel;
    //    savePlayer.IntelAdvancements = IntelAdvancements;
    //    savePlayer.Diplomacy = Diplomacy;
    //    savePlayer.Traits = Traits;
    //    savePlayer.RacialBonuses = RacialBonuses;
    //    savePlayer.Technologies = Technologies;
    //    savePlayer.TurnNumber = TurnNumber;

    //    if(!data.Data.ContainsKey(TurnNumber))
    //    {
    //        data.Data.Add(TurnNumber, new Dictionary<string, PlayerData>());
    //    }
    //    if (!data.Data[TurnNumber].ContainsKey(Name))
    //    {
    //        data.Data[TurnNumber].Add(Name, savePlayer);
    //    }
    //    else
    //    {
    //        data.Data[TurnNumber][Name] = savePlayer;
    //    }
    //}

    //public void LoadData(GameData data)
    //{
    //    var loadPlayer = data.Players.Where(x => x.Name == Name).FirstOrDefault();
    //    if (loadPlayer == null) return;
        
    //    Navy = loadPlayer.Navy;
    //    Colonies = loadPlayer.Colonies;
    //    Name = loadPlayer.Name;
    //    TechLevel = loadPlayer.TechLevel;
    //    IntelAdvancements = loadPlayer.IntelAdvancements;
    //    Diplomacy = loadPlayer.Diplomacy;
    //    Traits = loadPlayer.Traits;
    //    RacialBonuses = loadPlayer.RacialBonuses;
    //    Technologies = loadPlayer.Technologies;
    //    TurnNumber = loadPlayer.TurnNumber;

    //    data.Players.Remove(loadPlayer);
    //}
}

[System.Serializable]
public class PlayerData
{
    #region Objects
    public Navy Navy;
    public List<Colony> Colonies;
    //map knowledge
    #endregion

    #region Properties
    public int TechLevel;
    public List<string> IntelAdvancements;
    public Dictionary<string, string> Diplomacy;
    public Dictionary<string, string> Traits;
    public Dictionary<string, string> RacialBonuses;
    public Dictionary<string, string> Technologies;
    #endregion
}