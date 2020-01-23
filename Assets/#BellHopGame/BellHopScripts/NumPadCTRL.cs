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
        if (!_flowMngr.IsAllowKeypad)
        {
            Debug.LogWarning("Flowmanager NO KEYPADallowed");
            return;
        }
        if (_flowMngr.GameEnded)
        {
            Debug.LogWarning("Flowmanager gameneded");
            return;
        }
        if (argnum >= _gs.Master_Number_of_Floors) return;

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


    public void SetButtonColor(Color argColor)
    {

        foreach (GameObject Go in AvailableButtonsObjs)
        {

            //  Transform TranChild = Go.transform.GetChild(0);
            TMPro.TextMeshProUGUI ButtonTExt = Go.gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            ButtonTExt.color = argColor;
        }

    }

    public void SetFloorNumberOnDisplay(int argArrivedOnFloorZeroBased)
    {
        argArrivedOnFloorZeroBased++;
        m_Text_FloorNumber.text = argArrivedOnFloorZeroBased.ToString();
    }

    public void Set_GoingUP()
    {
        UpArrow.SetActive(true); DownArrowArrow.SetActive(false);
    }

    public void Set_GoingDown()
    {
        UpArrow.SetActive(false); DownArrowArrow.SetActive(true);
    }

    public void Set_cleararrows()
    {
        UpArrow.SetActive(false); DownArrowArrow.SetActive(false);
    }

}
