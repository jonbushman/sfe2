using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class ShipDictionary
{
    public static Dictionary<string, ShipTraits> AllShipTraits;


    public static void PrintShipTraits(string shipName)
    {
        if (!AllShipTraits.ContainsKey(shipName)) return;

        var str = "";
        foreach (PropertyInfo prop in AllShipTraits[shipName].GetType().GetProperties())
        {
            str += prop.Name;
            str += ": ";
            str += prop.GetValue(AllShipTraits[shipName]).ToString();
            str += "\n";
        }
    }

    public static void PrintAllShipTraits()
    {
        foreach (string ship in AllShipTraits.Keys)
        {
            PrintShipTraits(AllShipTraits[ship].ToString());
        }
    }

    public static void LoadFromJson()
    {
        JObject o1 = new JObject();
        AllShipTraits = new Dictionary<string, ShipTraits>();

        using (StreamReader file = File.OpenText(Application.dataPath + "/Constants/ShipData.json"))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
            o1 = (JObject)JToken.ReadFrom(reader);
        }


        foreach (var x in o1)
        {
            string ship = x.Key.ToString();

            AllShipTraits[ship] = new ShipTraits();

            foreach (var y in x.Value)
            {
                var jsonLine = y.ToString().Split(":");
                var attr = jsonLine[0].Replace("\"", string.Empty);
                var val = jsonLine[1].Replace("\"", string.Empty).Trim();

                switch (attr)
                {
                    case "Type":
                        AllShipTraits[ship].Type = val;
                        break;
                    case "Abbreviation":
                        AllShipTraits[ship].Abbreviation = val;
                        break;
                    case "SubType":
                        AllShipTraits[ship].SubType = val;
                        break;
                    case "ScanningStrength":
                        AllShipTraits[ship].ScanningStrength = Int32.Parse(val);
                        break;
                    case "TechLevel":
                        AllShipTraits[ship].TechLevel = Int32.Parse(val);
                        break;
                    case "Bpv":
                        AllShipTraits[ship].Bpv = Int32.Parse(val);
                        break;
                    case "Size":
                        AllShipTraits[ship].Size = Int32.Parse(val);
                        break;
                    case "Command":
                        AllShipTraits[ship].Command = Int32.Parse(val);
                        break;
                    case "Attack":
                        AllShipTraits[ship].Attack = Int32.Parse(val);
                        break;
                    case "Defense":
                        AllShipTraits[ship].Defense = Int32.Parse(val);
                        break;
                    case "AttackCrippled":
                        AllShipTraits[ship].AttackCripplped = Int32.Parse(val);
                        break;
                    case "DefenseCrippled":
                        AllShipTraits[ship].DefenseCripplped = Int32.Parse(val);
                        break;
                    case "Cargo":
                        AllShipTraits[ship].Cargo = Int32.Parse(val);
                        break;
                    case "Speed":
                        AllShipTraits[ship].Speed = Int32.Parse(val);
                        break;
                    case "Fighters":
                        AllShipTraits[ship].Fighters = Int32.Parse(val);
                        break;
                    case "Launch":
                        AllShipTraits[ship].Launch = float.Parse(val);
                        break;
                    case "Bombard":
                        AllShipTraits[ship].Bombard = float.Parse(val);
                        break;
                    case "PointDefense":
                        AllShipTraits[ship].PointDefense = float.Parse(val);
                        break;
                    case "Traits":
                        var l = new List<string>();
                        foreach (var v in val)
                        {
                            l.Add(v.ToString());
                        }
                        break;
                    default:
                        break;
                }


            }

        }




    }

}