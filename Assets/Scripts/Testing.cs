using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.IO;
using Unity.VisualScripting;


public class Testing : MonoBehaviour
{
    public TMP_InputField TurnInput;
    public TMP_InputField PlayerNameInput;

    private GameData _allData;
    private PlayerData _currentPlayer;

    private DataTable _dataTable;

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
                if (objCell == null || string.IsNullOrWhiteSpace(objCell.ToString())) continue;
                
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
                    if (objRow.GetCell(j) !=  null)
                    {
                        if (!string.IsNullOrEmpty(objRow.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(objRow.GetCell(j).ToString()))
                        {
                            rowList.Add(objRow.GetCell(j).ToString());
                        }
                    }
                }
                if (rowList.Count > 0)
                {
                    dTable.Rows.Add(rowList.ToArray());
                }
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
            PlayerData thisPlayer = new PlayerData();
            Fleet thisFleet = new Fleet("");
            foreach (DataColumn column in _dataTable.Columns)
            {
                var data = row[column];
                switch (column.ColumnName)
                {
                    case "Empire":
                        CreatePlayerData(turn, data.ToString());
                        thisPlayer = SFEManager.Instance.Data.Data[turn][data.ToString()];
                        break;
                    case "Fleet Name":
                        CreateFleet(thisPlayer, data.ToString());
                        thisFleet = thisPlayer.Navy.Fleets.Where(x => x.Name == data.ToString()).FirstOrDefault();
                        break;
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
        Debug.Log("imported data pushed to Player Datas for Turn " + turn.ToString());
    }

}
