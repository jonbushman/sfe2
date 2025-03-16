using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Org.BouncyCastle.Utilities.Encoders;
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

    [Space(5)]
    public Testing TestingScript;

    [SerializeField] private bool _topLeftRaised;
    [SerializeField] private GameObject _hexPrefab;
    [SerializeField] private TextureDictionaryScriptableObject _textureDictionary;

    [SerializeField] private SerializableDictionaryBase<string, Color> _playerColorDictionary;
    [SerializeField] private GameObject _fleetLabelPrefab;
    [SerializeField] private GameObject _fleetLabelContainer;

    [SerializeField] private Slider _segmentSlider;

    private Dictionary<Fleet, bool> _fleetLabelToggles = new Dictionary<Fleet, bool>();
    private List<Hex> _hexes = new List<Hex>();
    private Dictionary<Fleet, Transform> _fleetToLabelDict = new Dictionary<Fleet, Transform>();

    [Space(5)]
    [Header("Label Movement Settings")]
    [SerializeField] private bool _animateMovement;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _zAdjustment;
    [SerializeField] private float _destinationReachedThreshold;
    [SerializeField] private float _timeBetweenSegments;

    private void Start()
    {
        _segmentSlider.onValueChanged.AddListener(MoveFleetLabels);
    }

    public void CreateMap()
    {
        var resourceMap = TestingScript.ParseHextmlMap();
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
                hexData.ID = (j+1).ToString("D2") + (i+1).ToString("D2");
                newHex.gameObject.name = hexData.ID;
                hexData.DrawHex(Scale);

                var resourceName = resourceMap[hexData.ID];
                hexData.UpdateTexture(_textureDictionary.TextureDict[resourceName]);

                _hexes.Add(hexData);
            }
        }
    }

    public void CreateFleetLabels() //spawns them in starting hex location
    {
        var data = SFEManager.Instance.Data.Data[22];
        Fleet fl = new Fleet();
        
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
                fLabel.transform.parent = _fleetLabelContainer.transform;
                fLabel.GetComponentInChildren<Image>().color = _playerColorDictionary[playerName.Key];
                _fleetToLabelDict.Add(fleet, fLabel.transform );

                var fleetLoc = fleet.Location[0];
                //temp fix cause late night. fix at root later
                if (fleetLoc.Count() == 3) fleetLoc = "0" + fleetLoc; 

                var hex = _hexes.Where(x => x.ID == fleetLoc).ToList().FirstOrDefault();
                try
                {
                    fLabel.transform.position = new Vector3(hex.transform.position.x, hex.transform.position.y, hex.transform.position.z + _zAdjustment);
                }
                catch (NullReferenceException e)
                {
                    Debug.Log("check here");
                    fLabel.SetActive(false);
                }
            }
        }
    }



    public void MoveFleetLabels(float segmentFloat)
    {
        var segment = (int)segmentFloat;
        foreach (var label in _fleetToLabelDict)
        {
            var hexID = label.Key.Location[segment];
            //bad location checks
            if (hexID.Count() == 3) hexID = "0" + hexID;
            int t;
            if(!int.TryParse(hexID, out t))
            {
                label.Value.gameObject.SetActive(false);
                continue;
            }

            var hexLoc = _hexes.Where(x => x.ID == hexID).ToList().FirstOrDefault().transform.position; //better not fail
            var destination = new Vector3(hexLoc.x, hexLoc.y, hexLoc.z + _zAdjustment);

            if (label.Value.gameObject.activeSelf)
            {
                if (_animateMovement)
                {
                    StartCoroutine(LabelMovementCoroutine(label.Value, destination));
                }
                else
                {
                    label.Value.position = destination;
                }
            }
            else
            {
                try
                {
                    label.Value.gameObject.SetActive(true);
                    label.Value.position = destination;
                }
                catch (NullReferenceException e)
                {
                    Debug.Log("check here");
                    label.Value.gameObject.SetActive(false);
                }
            }
        }
    }

    private IEnumerator LabelMovementCoroutine(Transform tMoving, Vector3 destination)
    {
        while (Vector3.Distance(tMoving.position, destination) > _destinationReachedThreshold)
        {
            tMoving.position = Vector3.Lerp(tMoving.position, destination, Time.deltaTime * _movementSpeed);

            yield return null; ;
        }
    }

    public void AnimateFullTurnMovement()
    {
        StartCoroutine(AnimateFullTurnMovementCoroutine());
    }

    private IEnumerator AnimateFullTurnMovementCoroutine()
    {
        for (var i = 0; i < 7; i++)
        {
            MoveFleetLabels(i);
            yield return new WaitForSeconds(_timeBetweenSegments);
        }
    }
}
