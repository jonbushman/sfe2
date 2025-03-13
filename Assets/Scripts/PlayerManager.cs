using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public List<Player> Players;
    public Player ActivePlayer;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Player Manager in the scene.");
        }
        Instance = this;
    }

    //needs to check how many players are in the load data, or handle that here somewhere ya know

}
