using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Base
{
    public string Name;
    public string Type;
    public string Location;

    public Base(string name, string type)
    {
        Name = name;
        Type = type;
    }
}

public class BaseTraits
{

}