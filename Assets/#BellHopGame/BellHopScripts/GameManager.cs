using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("on" + gameObject.name);
        }
        else
            Destroy(this.gameObject);

        Master_Number_of_Floors = 3;
        if (Master_Number_of_Floors > NumKeypad_AvailableButtons.childCount)
            Master_Number_of_Floors = NumKeypad_AvailableButtons.childCount;
    }

    public BellHopCharacter TheBellHop;
    public Transform AirBornObj;

    public StoryManager m_StoryMngr;

    public FloorsManager MyFloorManager;

    public GameObject NumpadObj;
    NumPadCTRL _numPad;

    public TMPro.TextMeshProUGUI m_Text;

    public Transform NumKeypad_AvailableButtons;

    public int Master_Number_of_Floors;

    public int _master_CurentFloorNumber;
    public int Master_CurentFloorNumber { get => _master_CurentFloorNumber; set => _master_CurentFloorNumber = value; }
    public bool GameEnded = false;
    public bool IsAllowKeypad = false;
    int maxTimesAskedBeforeHint = 2;

    public void PlaceDwellersOnFloors(List<GameObject> argDwellers)
    {
        for (int x = 0; x < Master_Number_of_Floors; x++)
        {
            // argDwellers[x].transform.parent = MyFloorManager.Floors[x].transform;
            MyFloorManager.SetFloorAimalObj(x, argDwellers[x]);
        }
        MyFloorManager.InitializeFloor_0_Active();
    }
    /// <summary>
    /// Curentlevel story
    /// CharacterAnimation
    /// get Player input
    /// 
    /// 
    /// </summary>


    // Start is called before the first frame update
    void Start()
    {
        print("I shall never see thi");
        _numPad = NumpadObj.GetComponent<NumPadCTRL>();
        StartCoroutine(WaitOpenDoors(1));

    }



    // Update is called once per frame
    void Update()
    {

    }

    int DestinationFloor = 0;


    public void StartGoingToFloorOnButtonClicked(int argFloorNum)
    {
        DestinationFloor = argFloorNum;
        ElevatorDoorsMasterControl.Instance.CloseDoors();
        m_Text.text = "";
    }

    //called from elevatordoorsMAsterCTRL when doorclosed animation complets
    public void SwitchActiveFloorWhileDoorsAreClosed()
    {
        MyFloorManager.SetCurFloor(DestinationFloor);
        StartCoroutine(WaitOpenDoors(3));
    }
    public void SetCurDwellerReceivedObject(GameObject argtossedObj)
    {
        m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].Set_ItemReachedDwellr(argtossedObj);
        if (MyFloorManager.GEtCurrFloorNumber() == 0) EndGame();

    }

    public AnimalDweller GetCurDweller()
    {

        return m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()];

    }
    public void CurDwellerTossToBEllHop()
    {
        m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].TossObjectToBellhop();
        //  string theNeighbor = m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].GetStoryNode().Next_giveto1.TheAnimal1.ToString();
        //  m_Text.text = "take this " + m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].GetStoryNode().ObjectInHand1 + "to " + theNeighbor + " plz";
    }


    //called from elevatordoorsMAsterCTRL when doorOpen animation complets
    bool firstTime = true;
    public void ReachedFloor()
    {
        /*
        IsAllowKeypad = false;
        Debug.Log("floorReached");
        _numPad.ClearArrows();
        if (firstTime)
        {
           // string theNeighbor = m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].GetStoryNode().Next_giveto1.TheAnimal1.ToString();
          //  m_Text.text = "hello bellhop: the" + theNeighbor + "needs this " + m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].GetStoryNode().ObjectInHand1;

            m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].TossObjectToBellhop();
            firstTime = false;

        }
        else
        {

            if (TheBellHop._HeldObject == m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].GetStoryNode().Prev_OwedToMe1.ObjectInHand1)
            {
                m_Text.text = "thank you bellhop ";

                //RightHandHoldPos TheBellHop.TossToDwellerHand(m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].transform.GetChild(1));
                TheBellHop.TossToDwellerHand();
            }
            else
            {
                if (m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].GEt_AskedTimes() < maxTimesAskedBeforeHint)
                {

                    m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].IncrementAskTimes();

                    m_Text.text = "I dont need that , but ";
                    string theNeighbor = m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].GetStoryNode().Next_giveto1.TheAnimal1.ToString();
                    m_Text.text += theNeighbor + " needs this " + m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].GetStoryNode().ObjectInHand1;
                }
                else
                {
                    m_Text.text = "Hint Goto Floor ";
                    // GameEnums.AnimalCharcter Animal = m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].GetStoryNode().Next_giveto1.TheAnimal1;

                    int Floortogoto = m_StoryMngr.GetFloorNumberofForItem(TheBellHop._HeldObject);
                    m_Text.text += Floortogoto;// + " needs this " + m_StoryMngr.AnimalDwellers[MyFloorManager.GEtCurrFloorNumber()].GetStoryNode().ObjectInHand1;

                }
                GameManager.Instance.IsAllowKeypad = true;

            }


        }
        */


    }
    IEnumerator WaitOpenDoors(int argTimeWait)
    {
        yield return new WaitForSeconds(argTimeWait);
        ElevatorDoorsMasterControl.Instance.OpenDoors();
        UpdateStoryTextAccordingToCurrFloor();
    }

    //IEnumerator WaitTurnKeypadOnDoors(int argTimeWait)
    //{
    //    yield return new WaitForSeconds(argTimeWait);
    //    NumpadObj.SetActive(true);
    //}

    //TODO: test this then later use StoryController to generate the correct text, worongfloor ! we need to find blah...
    //this is just a test !!!!
    public void UpdateStoryTextAccordingToCurrFloor()
    {
        int curfloor = MyFloorManager.GEtCurrFloorNumber();
        string Sentence = "";// m_StoryMngr.Get_FloorInfo(curfloor);
                             //if bellhop held object is what floordweller needs -> get next obj, else "wrong floor buddy, the blah animal needs another blah"
        m_Text.text = Sentence;
    }


    public void EndGame()
    {
        GameEnded = true;
        WaitToChangeScenes();
    }

    void WaitToChangeScenes()
    {
        StartCoroutine(Wait5sec());

    }

    IEnumerator Wait5sec()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("PrimGame");
    }
}
