using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Base", menuName = "ScriptableObjects/SpawnBaseSO", order = 1)]
public class BaseScriptableObject : ScriptableObject
{
    public string Name;
    public List<float> Scan = new List<float>();
    public int TechLevel;
    public int BPV;
    public int Size;
    public int Command;
    public int Attack;
    public int AttackCrippled;
    public int Defense;
    public int DefenseCrippled;
    public int Cargo;
    public int Fighters;
    public float Launch;
    public float Bombard;
    public float PointDefense;
    public int BaySize;
    public int BayCount;
    public int BayCountCrippled;
    public int Production;
    public int Supply;
}
