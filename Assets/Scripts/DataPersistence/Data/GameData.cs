using UnityEngine;

[System.Serializable]
public class GameData
{
    public int TechLevel;

    public int TurnNumber;

    public GameData()
    {
        this.TechLevel = 0;
        this.TurnNumber = 1;
    }
}
