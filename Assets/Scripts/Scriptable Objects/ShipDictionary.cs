using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dict", menuName = "ScriptableObjects/SpawnShipDictionarySO", order = 1)]
public class ShipDictionary : ScriptableObject
{
    public List<ScriptableObject> scripts = new List<ScriptableObject>();

}
