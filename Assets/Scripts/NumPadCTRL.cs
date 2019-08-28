using UnityEngine;

public class NumPadCTRL : MonoBehaviour
{
    GameManager _gm;
    private void Start()
    {
        _gm = GameManager.Instance;
    }
    public void ReadButtonTouch(int argnum)
    {
        _gm.StartGoingToFloorOnButtonClicked(argnum);
        Debug.Log("pressed " + argnum);
    }
    public void UnTouch(int argnum)
    {
        Debug.Log("UNpressed " + argnum);

    }
}
