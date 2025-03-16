using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public int Width;
    public int Height;
    public float Scale;
    public float xPadding;
    public float yPadding;

    [SerializeField] private bool _topLeftRaised;
    [SerializeField] private GameObject _hexPrefab;
    [SerializeField] private ScriptableObject _textureDictionary;

    [SerializeField] private SerializableDictionaryBase<string, Color> _playerColorDictionary;
    [SerializeField] private GameObject _fleetLabelPrefab;


    private Dictionary<Fleet, bool> _fleetLabelToggles = new Dictionary<Fleet, bool>();

    public void CreateMap()
    {
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                var newHex = Instantiate(_hexPrefab);
                newHex.transform.parent = transform;

                float extraY = 0;
                if ((j%2 != 0 && _topLeftRaised) || (j % 2 == 0 && !_topLeftRaised))
                {
                    extraY = Scale * Mathf.Cos(Mathf.PI / 6);
                }

                newHex.transform.localPosition = new Vector3(j * (xPadding + Scale + Scale * Mathf.Sin(Mathf.PI/6)), -1 * (i * (Scale * Mathf.Cos(Mathf.PI/6) * 2 + yPadding) + extraY), 0);
                
                var hexData = newHex.GetComponent<Hex>();
                hexData.ID = i.ToString("D2") + j.ToString("D2");
                newHex.gameObject.name = hexData.ID;
                hexData.DrawHex(Scale);


            }
        }
    }



    public void CreateFleetLabels()
    {
        var data = SFEManager.Instance.Data.Data[22];
        Fleet fl = new Fleet();

        var container = GameObject.Find("Fleet Labels Container");
        
        foreach (var playerName in data)
        {
            var fleets = playerName.Value.Navy.Fleets;
            foreach (var fleet in fleets)
            {
                if (_fleetLabelToggles.ContainsKey(fleet))
                {
                    if (_fleetLabelToggles[fleet] != true) continue;
                }

                var fLabel = Instantiate(_fleetLabelPrefab);
                fLabel.name = playerName.Key + " <> " + fleet.Name;
                fLabel.transform.parent = container.transform;
                fLabel.GetComponentInChildren<Image>().color = _playerColorDictionary[playerName.Key];
            }
        }
    }

    public void MoveFleetLabels(int segment)
    {

    }
}
