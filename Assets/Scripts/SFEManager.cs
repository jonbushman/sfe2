using UnityEngine;

public class SFEManager : MonoBehaviour
{
    private void Awake()
    {
        ShipDictionary.LoadFromJson();
    }


}
