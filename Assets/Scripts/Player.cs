using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Objects
    public Navy Navy;
    private List<Colony> _colonies;
    //map knowledge
    #endregion

    #region Properties
    private int _techLevel;
    private List<string> _intelAdvancements;
    private Dictionary<string, string> _diplomacy;
    private Dictionary<string, string> _traits;
    private Dictionary<string, string> _racialBonuses;
    private Dictionary<string, string> _technologies;
    private int _turnNumber;
    #endregion


}
