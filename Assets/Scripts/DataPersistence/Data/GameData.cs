using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public List<PlayerData> Players;

    public GameData()
    {
        this.Players = new List<PlayerData>();
    }
}
