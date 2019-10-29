using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NumPadStarter : MonoBehaviour
{
    GameSettings _gs;


    public TMPro.TextMeshProUGUI m_Text_FloorNumber;

    public List<GameObject> AllButtonsObjs = new List<GameObject>();

    public List<GameObject> AvailableButtonsObjs = new List<GameObject>();

    bool hasselected = false;

    private void Start()
    {


        _gs = GameSettings.Instance;

        if (_gs == null)
        {
            Debug.LogWarning("NumPadCTRL: no gm in scene!");
        }



        for (int x = 0; x < transform.childCount; x++)
        {
            AllButtonsObjs.Add(transform.GetChild(x).gameObject);
            if (x > 1)
            {
                AvailableButtonsObjs.Add(transform.GetChild(x).gameObject);
            }
            else
            {
                transform.GetChild(x).gameObject.SetActive(false);
            }
        }


        SetButtonColor(Color.green);
    }


    public void ReadButtonTouch(int argnum)
    {

        if (!hasselected)
        {
            SetButtonColor(Color.red);
            m_Text_FloorNumber.text = argnum.ToString();
            Debug.Log("pressed " + argnum);
            _gs.Set_safely_MAsterFloorNumber(argnum);
            hasselected = true;
            StartCoroutine(GoToDeliveryGAme());
        }

    }


    public void UnTouch(int argnum)
    {
        Debug.Log("UNpressed " + argnum);

    }



    void SetButtonColor(Color argColor)
    {
        foreach (GameObject Go in AvailableButtonsObjs)
        {
            TMPro.TextMeshProUGUI ButtonTExt = Go.gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            ButtonTExt.color = argColor;
        }
    }



    private IEnumerator GoToDeliveryGAme()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("DeliveryGame");
    }
}
