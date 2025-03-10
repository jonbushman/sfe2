using UnityEngine;

public class SFEManager : MonoBehaviour
{
    private Player _player;
    private WindowManager _windowManager;

    private void Start()
    {
        _player = GetComponent<Player>();
        _windowManager = GetComponent<WindowManager>();
    }

    private void Save()
    {

    }

    private void Load()
    {
        
    }

}
