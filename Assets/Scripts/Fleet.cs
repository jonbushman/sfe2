using System.Collections.Generic;

[System.Serializable]
public class Fleet
{
    public string Name;
    public List<Ship> Ships;
    public string[] Location;
    public List<string> Actions;

    public Fleet()
    {
        Ships = new List<Ship>();
        Actions = new List<string>();
        Location = new string[7];
    }
    public Fleet(string name)
    {
        Name = name;
        Ships = new List<Ship>();
        Actions = new List<string>();
        Location = new string[7];
    }
}
