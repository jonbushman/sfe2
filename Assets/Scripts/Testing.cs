using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.IO;
using System;


public class Testing : MonoBehaviour
{
    public TMP_InputField TurnInput;
    public TMP_InputField PlayerNameInput;

    private GameData _allData;
    private PlayerData _currentPlayer;

    private DataTable _dataTable;

    private Dictionary<string, string> _hexResourceDictionary;

    [Header("Diplomacy Sim")]
    [SerializeField] private List<int> _attemptsTilDiplomacyComplete;
    [SerializeField] private List<int> _diplomacySimAnalysis;
    [SerializeField] private int _iterations;
    [SerializeField] private int _maxAttempts;
    [SerializeField] private int _initialBonus;
    [SerializeField] private int _staticBonus;
    [SerializeField] private int _initialRank;

    private void Start()
    {
        _allData = SFEManager.Instance.Data;
    }

    public void CreatePlayerData(int turnNumber, string playerName)
    {
        if (!_allData.Data.ContainsKey(turnNumber))
        {
            _allData.Data.Add(turnNumber, new Dictionary<string, PlayerData>());
        }
        if (!_allData.Data[turnNumber].ContainsKey(playerName))
        {
            _allData.Data[turnNumber].Add(playerName, new PlayerData());
        }
    }

    public void CreateFleet(PlayerData player, string name)
    {
        if (player.Navy.Fleets.Where(x=> x.Name == name).Count() == 0)
        {
            player.Navy.Fleets.Add(new Fleet(name));
        }

    }    

    public void CreateBase(PlayerData player, string name, string type, string location)
    {
        if (player.Navy.Bases.Where(x=> x.Name == name).Count() == 0)
        {
            var station = new Base(name, type);
            station.Location = location;
            player.Navy.Bases.Add(station);
        }
    }

    public void ChoosePlayer()
    {
        if (string.IsNullOrEmpty(PlayerNameInput.text))
        {
            Debug.LogError("must enter a player name");
        }
        if (string.IsNullOrEmpty(TurnInput.text))
        {
            Debug.LogError("must enter a turn number");
        }
        var turnNumber = int.Parse(TurnInput.text);
        var playerName = PlayerNameInput.text;

        CreatePlayerData(turnNumber, playerName);
        
        _currentPlayer = SFEManager.Instance.Data.Data[turnNumber][playerName];
    }

    public void CreateShips()
    {
        var ship1 = _currentPlayer.Navy.CreateShip("Hot Topic", "Perdition Beam Titan");
        var ship2 = _currentPlayer.Navy.CreateShip("McDonalds", "Super Space Control Ship");
        var ship3 = _currentPlayer.Navy.CreateShip("New York", "Battleship");
        var ship4 = _currentPlayer.Navy.CreateShip("Milan", "Battleship");
        var ship5 = _currentPlayer.Navy.CreateShip("Tokyo", "Battleship");
        var ship6 = _currentPlayer.Navy.CreateShip("Dicks Sporting Goods", "Heavy Cruiser Improved");
        var ship7 = _currentPlayer.Navy.CreateShip("Big Five", "Heavy Cruiser Improved");
        var ship8 = _currentPlayer.Navy.CreateShip("Peter Piper Pizza", "Heavy Cruiser Improved");

        _currentPlayer.Navy.RenameFleet();

    }

    public void MoveShipsIntoFleet()
    {
        var newFleetName = "Avenger";
        _currentPlayer.Navy.CreateFleet(newFleetName);
        var unAss = _currentPlayer.Navy.Unassigned;
        var fleetToMove = new Fleet();
        fleetToMove.Ships = unAss.Ships;
        for (int i = 0; i < fleetToMove.Ships.Count; i++)
        {
            _currentPlayer.Navy.ChangeFleet(fleetToMove.Ships[i], newFleetName);
        }
        //Player1.Navy.ChangeFleet(Player1.Navy.Unassigned, "Avenger");
    }

    public void PrintFleet(string fleetName)
    {
        Debug.Log("Printing Fleet...");

        var fleet = _currentPlayer.Navy.Fleets.Where(x => x.Name == fleetName).ToList().FirstOrDefault();

        foreach (var ship in fleet.Ships)
        {
            Debug.Log(ship.Name);
        }
    }

    public void ImportMovementSpreadsheet()
    {
        var filePath = "C:\\Users\\Jon\\Downloads\\Copy of Movement Computation 4.0.xlsx";

        var dTable = new DataTable();
        ISheet objWorksheet;

        using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            fileStream.Position = 0;
            var objWorkbook = new XSSFWorkbook(fileStream);
            objWorksheet = objWorkbook.GetSheet("Turn 22");
            var objHeader = objWorksheet.GetRow(0);
            var countCells = objHeader.LastCellNum;
            for (int j=0; j< countCells; j++)
            {
                var objCell = objHeader.GetCell(j);
                //if (objCell == null || string.IsNullOrWhiteSpace(objCell.ToString())) continue;
                
                dTable.Columns.Add(objCell.ToString());
            }
            for (int i = (objWorksheet.FirstRowNum + 1); i <= objWorksheet.LastRowNum; i++)
            {
                var rowList = new List<string>();
                var objRow = objWorksheet.GetRow(i);
                if (objRow == null) continue;
                if (objRow.Cells.All(d => d.CellType == CellType.Blank)) continue;
                for (int j = objRow.FirstCellNum; j < countCells; j++)
                {
                    rowList.Add(objRow.GetCell(j).ToString());
                    //if (objRow.GetCell(j) !=  null)
                    //{
                    //    //if (!string.IsNullOrEmpty(objRow.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(objRow.GetCell(j).ToString()))
                    //    //{
                    //    //}
                    //}
                }
                dTable.Rows.Add(rowList.ToArray());
                //if (rowList.Count > 0)
                //{
                //}
            }
        }
        _dataTable = dTable;

        Debug.Log("Imported");
    }

    public void StoreImportedData()
    {
        int turn = 22;
        foreach(DataRow row in _dataTable.Rows)
        {
            CreatePlayerData(turn, row["Empire"].ToString());
            var thisPlayer = SFEManager.Instance.Data.Data[turn][row["Empire"].ToString()];
            if (row["Speed"].ToString() == "Base")
            {
                //have to count base instances and scanning quality to determine base
                //1 instance: MB
                //7 instances with 25%s, BS
                //7 instances without 25%s, BATS
                //more (19) is SB
                var baseName = row["Fleet Name"].ToString();
                var empireName = row["Empire"].ToString();
                if (thisPlayer.Navy.Bases.Where(x => x.Name == baseName).Count() == 0)
                {
                    int occurences = 0;
                    bool has25 = false;
                    foreach (DataRow r in _dataTable.Rows)
                    {
                        if (r["Empire"].ToString() == empireName && r["Speed"].ToString() == "Base" && r["Fleet Name"].ToString() == baseName)
                        {
                            occurences++;
                            if (r["Sensor Rating"].ToString() == "25%") has25 = true;
                        }
                    }
                    var baseType = "";
                    if (occurences == 1)
                    {
                        baseType = "MB";
                    }
                    else if (occurences > 2 && occurences <= 8 && has25)
                    {
                        baseType = "BS";
                    }
                    else if (occurences > 2 && occurences <= 8 && !has25)
                    {
                        baseType = "BATS";
                    }
                    else if (occurences > 8)
                    {
                        baseType = "SB";
                    }
                    CreateBase(thisPlayer, baseName, baseType, row["Starting Hex"].ToString());
                }
            }
            else
            {
                CreateFleet(thisPlayer, row["Fleet Name"].ToString());
                var thisFleet = thisPlayer.Navy.Fleets.Where(x => x.Name == row["Fleet Name"].ToString()).FirstOrDefault();
                foreach (DataColumn column in _dataTable.Columns)
                {
                    var data = row[column];
                    switch (column.ColumnName)
                    {
                        case "Starting Hex":
                            thisFleet.Location[0] = data.ToString();
                            break;
                        case "Seg 1":
                            thisFleet.Location[1] = data.ToString();
                            break;
                        case "Seg 2":
                            thisFleet.Location[2] = data.ToString();
                            break;
                        case "Seg 3":
                            thisFleet.Location[3] = data.ToString();
                            break;
                        case "Seg 4":
                            thisFleet.Location[4] = data.ToString();
                            break;
                        case "Seg 5":
                            thisFleet.Location[5] = data.ToString();
                            break;
                        case "Seg 6":
                            thisFleet.Location[6] = data.ToString();
                            break;
                        default:
                            break;
                    } 
                }
            }
        }
        Debug.Log("imported data pushed to Player Datas for Turn " + turn.ToString());
    }

    public Dictionary<string, string> ParseHextmlMap()
    {
        var hexResourceDictionary = new Dictionary<string, string>();

        var filePath = "C:\\Users\\Jon\\Downloads\\SFE 3.0 Map.html";

        HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

        // There are various options, set as needed
        htmlDoc.OptionFixNestedTags = true;

        htmlDoc.Load(filePath);

        if (htmlDoc.DocumentNode != null)
        {
            HtmlAgilityPack.HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

            if (bodyNode != null)
            {
                var hexNodes = bodyNode.SelectNodes("//div[contains(@class, 'hexagon-in2')]");
                foreach (var node in hexNodes)
                {
                    if (node != null)
                    {
                        try
                        {
                            var hexNumNode = node.SelectNodes(".//span[contains(@class, 'hexnum')]").FirstOrDefault().InnerText;
                            var hexIDList = hexNumNode.Split(",").ToList();
                            var hexID1 = hexIDList[0].Trim();
                            var hexID2 = hexIDList[1].Trim();
                            
                            String hexID;
                            if (hexID1.Count() == 1) hexID = "0" + hexID1;
                            else hexID = hexID1;

                            if (hexID2.Count() == 1) hexID = hexID + "0" + hexID2;
                            else hexID = hexID + hexID2;

                            var nodeClasses = node.GetClasses().ToList();
                            var resourceClass = nodeClasses[1].ToString();
                            var resourceName = resourceClass.Replace("resource-", "").Replace("_"," ");

                            if (hexResourceDictionary.ContainsKey(hexID))
                            {
                                Debug.Log(hexID);
                            }
                            else
                            {
                                hexResourceDictionary.Add(hexID, resourceName);
                            }
                        }
                        catch (ArgumentNullException)
                        {
                            Debug.Log("what");
                        }
                    }
                }
            }
        }

        return hexResourceDictionary;
    }

    public void DiplomacySim()
    {
        var iterations = _iterations;
        var maxAttempts = _maxAttempts;
        var attemptsTilComplete = new List<int>();

        for (int i = 0; i < iterations; i++)
        {
            var bonus = _initialBonus;
            var staticBonus = _staticBonus;
            var rank = _initialRank; //starting at Goodwill
            var attempts = 0;
            while (rank < 9 && attempts < maxAttempts)
            {
                bonus += staticBonus;
                var diceRoll = UnityEngine.Random.Range(1, 101);
                diceRoll += bonus;
                bonus = 0;

                attempts += 1;

                switch (diceRoll)
                {
                    case 1:
                        rank -= 2;
                        break;
                    case <= 10:
                        rank -= 1;
                        break;
                    case <= 20:
                        bonus = -20;
                        break;
                    case <= 30:
                        bonus = -10;
                        break;
                    case <= 40:
                        bonus = -5;
                        break;
                    case <= 60:
                        bonus = 0;
                        break;
                    case <= 70:
                        bonus = 5;
                        break;
                    case <= 80:
                        bonus = 10;
                        break;
                    case <= 90:
                        bonus = 20;
                        break;
                    case <= 100:
                        bonus = 0;
                        rank += 1;
                        break;
                    case > 100:
                        bonus = 10;
                        rank += 1;
                        break;
                }

            }

            if (rank == 9)
            {
                attemptsTilComplete.Add(attempts);
            }
            else
            {
                attemptsTilComplete.Add(20);
            }
        }
        _attemptsTilDiplomacyComplete = attemptsTilComplete;
        _diplomacySimAnalysis.Clear();
        for (var  i = 0; i < 21; i++)
        {
            _diplomacySimAnalysis.Add(0);
        }
        foreach (var att in _attemptsTilDiplomacyComplete)
        {
            _diplomacySimAnalysis[att]+= 1;
        }
    }


}
