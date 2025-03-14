using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public Dictionary<int, Dictionary<string, PlayerData>> Data;

    public GameData()
    {
        Data = new Dictionary<int, Dictionary<string, PlayerData>>();
    }

}
