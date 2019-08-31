using UnityEngine;

public class NumPadCTRL : MonoBehaviour
{
    GameManager _gm;

    public GameObject UpArrow;

    public GameObject DownArrowArrow;

    public TMPro.TextMeshProUGUI m_Text_FloorNumber;

    private void Start()
    {
        _gm = GameManager.Instance;
        UpArrow.SetActive(false);
        DownArrowArrow.SetActive(false);
    }
    public void ReadButtonTouch(int argnum)
    {
        if (_gm.GameEnded) return;
        if (argnum >= _gm.Master_Number_of_Floors) return;

        if (_gm.Master_CurentFloorNumber < argnum) { UpArrow.SetActive(true); DownArrowArrow.SetActive(false); }
        else

        if (_gm.Master_CurentFloorNumber > argnum) { UpArrow.SetActive(false); DownArrowArrow.SetActive(true); }

        _gm.StartGoingToFloorOnButtonClicked(argnum);
        m_Text_FloorNumber.text = argnum.ToString();
        Debug.Log("pressed " + argnum);
    }
    public void UnTouch(int argnum)
    {
        Debug.Log("UNpressed " + argnum);

    }
    public void ClearArrows()
    {
        UpArrow.SetActive(false);
        DownArrowArrow.SetActive(false);
    }
}
