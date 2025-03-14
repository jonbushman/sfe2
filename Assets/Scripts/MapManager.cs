using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int Width;
    public int Height;
    public float Scale;

    [SerializeField] private bool _topLeftRaised;
    [SerializeField] private GameObject _hexPrefab;

    public void CreateMap()
    {
        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Height; j++)
            {
                var newHex = Instantiate(_hexPrefab);
                newHex.transform.parent = transform;

                float extraY = 0;
                if ((j%2 != 0 && _topLeftRaised) || (j % 2 == 0 && !_topLeftRaised))
                {
                    extraY = Scale * Mathf.Cos(Mathf.PI / 6);
                }

                newHex.transform.localPosition = new Vector3(j * (Scale + Scale * Mathf.Sin(Mathf.PI/6)), -1 * (i * Scale * Mathf.Cos(Mathf.PI/6) * 2 + extraY), 0);
                
                var hexData = newHex.GetComponent<Hex>();
                hexData.ID = i.ToString("D2") + j.ToString("D2");
                newHex.gameObject.name = hexData.ID;
                hexData.DrawHex(Scale);
            }
        }
    }
}
