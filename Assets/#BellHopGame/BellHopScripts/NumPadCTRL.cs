using System.Collections.Generic;
using UnityEngine;

public class NumPadCTRL : MonoBehaviour
{
    GameSettings _gs;
    GameFlowManager _flowMngr;

    public GameObject UpArrow;

    public GameObject DownArrowArrow;

    public TMPro.TextMeshProUGUI m_Text_FloorNumber;

    public List<GameObject> AllButtonsObjs = new List<GameObject>();

    public List<GameObject> AvailableButtonsObjs = new List<GameObject>();

    private void Start()
    {


        _gs = GameSettings.Instance;

        if (_gs == null)
        {
            Debug.LogWarning("NumPadCTRL: no gm in scene!");
        }
        _flowMngr = GameFlowManager.Instance;

        if (_flowMngr == null)
        {
            Debug.LogWarning("NumPadCTRL: no flowMngr in scene!");
        }


        UpArrow.SetActive(false);
        DownArrowArrow.SetActive(false);

        for (int x = 0; x < transform.childCount; x++)
        {
            AllButtonsObjs.Add(transform.GetChild(x).gameObject);
            if (x < _gs.Master_Number_of_Floors)
            {
                AvailableButtonsObjs.Add(transform.GetChild(x).gameObject);
            }
            else
            {
                transform.GetChild(x).gameObject.SetActive(false);
            }
        }
    }
    public void ReadButtonTouch(int argnum)
    {
        if (!_flowMngr.IsAllowKeypad) return;
        if (_flowMngr.GameEnded) return;
        if (argnum >= _gs.Master_Number_of_Floors) return;

        //if (_gs.Master_CurentFloorNumber < argnum) { UpArrow.SetActive(true); DownArrowArrow.SetActive(false); }
        //else

        //if (_gs.Master_CurentFloorNumber > argnum) { UpArrow.SetActive(false); DownArrowArrow.SetActive(true); }

        // _gs.StartGoingToFloorOnButtonClicked(argnum);
        m_Text_FloorNumber.text = argnum.ToString();
        Debug.Log("pressed " + argnum);
        BellHopGameEventManager.Instance.Call_ButtonPressed(argnum);

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
