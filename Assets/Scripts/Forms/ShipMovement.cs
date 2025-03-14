using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public TMP_Dropdown PlayerDropDown;
    public TMP_Dropdown FleetDropDown;
    public List<TMP_InputField> SegmentInputs;


    private Player _currentPlayer;
    [SerializeField]private int _currentTurn;
    private Fleet _fleet;

    private void Start()
    {

        var playerNames = SFEManager.Instance.GetPlayerNames();
        PlayerDropDown.ClearOptions();
        foreach (var player in playerNames)
        {
            PlayerDropDown.options.Add(new TMP_Dropdown.OptionData(player));
        }
    }

    private void OnEnable()
    {
        PlayerDropDown.onValueChanged.AddListener(OnPlayerSelection);
        FleetDropDown.onValueChanged.AddListener(OnFleetSelection);

        for (int i = 0; i <  SegmentInputs.Count; i++)
        {
            var segment = i;
            SegmentInputs[i].onEndEdit.AddListener((value) => { OnSegmentLocationChanged(value, segment); });
        }
    }

    private void OnPlayerSelection(int arg0)
    {
        var playerName = PlayerDropDown.options[arg0].text;
        var data = SFEManager.Instance.GetPlayerData(playerName, _currentTurn);

        if (_currentPlayer == null)
        {
            FleetDropDown.enabled = false;
        }
        else
        {
            FleetDropDown.enabled = true;
        }

        FleetDropDown.ClearOptions();
        foreach (var fleet in _currentPlayer.Navy.Fleets)
        {
            FleetDropDown.options.Add(new TMP_Dropdown.OptionData(fleet.Name));
        }
    }

    private void OnFleetSelection(int arg0)
    {
        var fleetName = FleetDropDown.options[arg0].text;
        _fleet = _currentPlayer.Navy.Fleets.Where(x => x.Name == fleetName).FirstOrDefault();
    }

    private void OnSegmentLocationChanged(string arg0, int segment)
    {
        Debug.Log("Segment: " + segment.ToString() + " move to " + arg0);
        _fleet.Location[segment] = arg0;
    }

}
