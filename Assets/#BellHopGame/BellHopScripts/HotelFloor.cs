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

    public DwellerMeshComposer FloorDweller;
    public FloorFurnisher floorFurnisherChild;

    public int FloorNumber { get => _floorNumber; set => _floorNumber = value; }
    // public bool DeliveryItemStillOnFloor { get => _deliveryItemStillOnFloor; set => _deliveryItemStillOnFloor = value; }
    public bool ReceivedItem { get => _receivedItem; set => _receivedItem = value; }

    //bool DidthisFire = false;
    private void Start()
    {
        floorFurnisherChild.Build_rand_RoomType();
        // DidthisFire = true;
    }

    public void SetDweller(GameObject argDwellerObj)
    {
        //if (DidthisFire) Debug.Log("was supoosed to be iniited");
        //else
        //    Debug.Log("oh shit");
        argDwellerObj.transform.position = new Vector3(TRAN_DoorStep.position.x, TRAN_DoorStep.position.y, TRAN_DoorStep.position.z);
        argDwellerObj.transform.parent = this.transform;
        FloorDweller = argDwellerObj.GetComponent<DwellerMeshComposer>();

        // print("setting pos  " + FloorDweller.Id + " " + FloorDweller.name);
        //  FloorDweller.WarpAgent(TRAN_DoorStep);
        // argDwellerObj.transform.parent = this.transform;
        m_Text_Billboard.text = FloorDweller.Gender + ". " + FloorDweller.AnimalName + " the " + FloorDweller.AnimalType;
        //     DeliveryItemStillOnFloor = true;
        ReceivedItem = false;
        GameObject nf = Instantiate(NavFloor);
        nf.transform.position = new Vector3(TRAN_516Pos.position.x, TRAN_516Pos.position.y, TRAN_516Pos.position.z);
        InitialFLoor.SetActive(false);
        BuildingMesh.SetActive(false);
        // Instantiate(trmpObstcle, new Vector3(TRAN_MidRoom.position.x, TRAN_MidRoom.position.y, TRAN_MidRoom.position.z), Quaternion.identity);

        //argDwellerObj.GetComponent<NavMeshAgent>().enabled = true;
        //FloorDweller.AgentMustSetTarget(TRAN_DoorStep); ;
        //FloorDweller.WarpAgent(TRAN_DoorStep);
        //nf.transform.parent = this.transform;
    }
    public void InitDwellerAgentNowIGuess()
    {
        //        print("agent init  " + FloorDweller.Id + " " + FloorDweller.name);
        //FloorDweller.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        FloorDweller.Activate_NAvAgent();
        //FloorDweller.Plz_GOTO(floorFurnisherChild.GetGreetingsAction().transform, false);//<false just sets dest , no walking

        FloorDweller.InitSomePoints(floorFurnisherChild.GetGreetingsAction().GetActionPos(), floorFurnisherChild.GetDanceAction().GetActionPos(), floorFurnisherChild.GetMainAction().GetActionPos());
        FloorDweller.WarpAgent(floorFurnisherChild.GetGreetingsAction().GetActionPos());


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
    public void MoveNave_To()
    {


    }

    public void MoveNave_To_MidDoor() { FloorDweller.Go_to_my_MidDoor(); }
    public void MoveNave_To_MainAction() { FloorDweller.Go_to_my_Main(); }
    public void MoveNave_To_Dance() { FloorDweller.Go_to_my_Dance(); }

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
