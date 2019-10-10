using UnityEngine;

public class DwellerTarget : MonoBehaviour
{

    int _targId;
    bool _isNavTarget;
    string _animToPlay;

    public int TargId { get => _targId; set => _targId = value; }
    public bool IsNavTarget { get => _isNavTarget; set => _isNavTarget = value; }
    public string AnimToPlay { get => _animToPlay; set => _animToPlay = value; }



    // Start is called before the first frame update
    void Start()
    {
        _targId = 0;
        _isNavTarget = false;
        _animToPlay = GameSettings.Instance.Shaving;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
