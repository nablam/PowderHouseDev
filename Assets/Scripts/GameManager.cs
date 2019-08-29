using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);

        Master_Number_of_Floors = NumKeypad_AvailableButtons.childCount;
    }

    public BellHopCharacter TheBellHop;

    public StoryManager m_StoryMngr;

    public FloorsManager MyFloorManager;

    public GameObject NumpadObj;

    public TMPro.TextMeshProUGUI m_Text;

    public Transform NumKeypad_AvailableButtons;

    public int Master_Number_of_Floors;

    public int _master_CurentFloorNumber;
    public int Master_CurentFloorNumber { get => _master_CurentFloorNumber; set => _master_CurentFloorNumber = value; }


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
    //called from elevatordoorsMAsterCTRL when doorOpen animation complets
    public void ReachedFloor()
    {
        // StartCoroutine(WaitTurnKeypadOnDoors(4));
        Debug.Log("floorReached");
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
        string Sentence = m_StoryMngr.GetFloorDwellerAsInfo(curfloor);
        //if bellhop held object is what floordweller needs -> get next obj, else "wrong floor buddy, the blah animal needs another blah"
        m_Text.text = Sentence;
    }

}
