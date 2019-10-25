using UnityEngine;

public class HotelFloor : MonoBehaviour
{
    public GameObject Barrier;
    public Transform TRAN_DoorStep;
    //  public Transform TRAN_SpawnPos;
    //  public Transform TRAN_SecondaryPos;
    //   public Transform TRAN_MidRoom;
    //public Transform TRAN_InteractibleMainPos;
    public GameObject BackWall;
    public Transform TRAN_WindowPos;
    public Transform TRAN_CeilingLightPos;
    public GameObject BuildingMesh;
    public GameObject BaseCamPos;
    public TMPro.TextMeshPro m_Text_Billboard;


    public GameObject NavFloor;

    //--------------------
    public GameObject InitialFLoor;
    public Transform TRAN_516Pos;
    //---------------

    public GameObject trmpObstcle;

    int _floorNumber;
    // bool _deliveryItemStillOnFloor;
    bool _receivedItem;

    public AnimalCentralCommand FloorDweller;
    public FloorFurnisher floorFurnisherChild;

    public int FloorNumber { get => _floorNumber; set => _floorNumber = value; }
    // public bool DeliveryItemStillOnFloor { get => _deliveryItemStillOnFloor; set => _deliveryItemStillOnFloor = value; }
    public bool ReceivedItem { get => _receivedItem; set => _receivedItem = value; }
    public InteractionCentral Greetings { get => _greetings; set => _greetings = value; }
    public InteractionCentral Dance { get => _dance; set => _dance = value; }
    public InteractionCentral Mainaction { get => _mainaction; set => _mainaction = value; }

    InteractionCentral _greetings;
    InteractionCentral _dance;
    InteractionCentral _mainaction;

    //bool DidthisFire = false;
    private void Awake()
    {

        // DidthisFire = true;
    }

    public void EarlyBuildFurniture()
    {
        floorFurnisherChild.Build_rand_RoomType();

        Greetings = floorFurnisherChild.GetGreetingsAction();
        Dance = floorFurnisherChild.GetDanceAction();
        Mainaction = floorFurnisherChild.GetMainAction();
    }

    public void SetDweller(GameObject argDwellerObj)
    {


        argDwellerObj.transform.position = new Vector3(InitialFLoor.transform.position.x, InitialFLoor.transform.position.y, InitialFLoor.transform.position.z);
        argDwellerObj.transform.parent = this.transform;
        FloorDweller = argDwellerObj.GetComponent<AnimalCentralCommand>();

        m_Text_Billboard.text = argDwellerObj.GetComponent<DwellerMeshComposer>().Gender + ". " + argDwellerObj.GetComponent<DwellerMeshComposer>().AnimalName + " the " + argDwellerObj.GetComponent<DwellerMeshComposer>().AnimalType;


        ReceivedItem = false;
        //GameObject nf = Instantiate(NavFloor);
        //nf.transform.position = new Vector3(TRAN_516Pos.position.x, TRAN_516Pos.position.y, TRAN_516Pos.position.z);
        InitialFLoor.SetActive(true);
        BuildingMesh.SetActive(true);








    }
    public void InitDwellerAgentNowIGuess()
    {




        FloorDweller.ActivateAgent();

        FloorDweller.Warp(Greetings.GetActionPos());
    }

    public void SetInitDest()
    {
        //  FloorDweller.Plz_GOTO(TRAN_DoorStep, false);//<false just sets dest , no walking
    }

    public void WarpInit()
    {
        // FloorDweller.WarpAgent(TRAN_DoorStep);
    }

    int cnt = 0;
    public void InitFloor()
    {

    }


    public void ShowHideBArrier(bool argShow)
    {
        Barrier.SetActive(argShow);

    }


    public void CoordinateTossCatch()
    {

        //bunny toss 

        // anime Apex -> bunnyhandls -> 

    }

    void InitialFloorVisited()
    {

    }
    void visitingCorrectFloor() { }

    void visitingWrongFloor() { }
}
