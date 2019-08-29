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
        _gm.StartGoingToFloorOnButtonClicked(argnum);
        m_Text_FloorNumber.text = argnum.ToString();
        Debug.Log("pressed " + argnum);
    }
    public void UnTouch(int argnum)
    {
        Debug.Log("UNpressed " + argnum);

    }
}
