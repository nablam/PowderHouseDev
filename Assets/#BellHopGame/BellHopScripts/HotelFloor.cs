using UnityEngine;

public class HotelFloor : MonoBehaviour
{
    public GameObject Barrier;
    public Transform TRAN_DoorStep;
    public Transform TRAN_SpawnPos;
    public Transform TRAN_SecondaryPos;
    public Transform TRAN_MidRoom;
    public Transform TRAN_InteractibleMainPos;
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

    public int FloorNumber { get => _floorNumber; set => _floorNumber = value; }
    // public bool DeliveryItemStillOnFloor { get => _deliveryItemStillOnFloor; set => _deliveryItemStillOnFloor = value; }
    public bool ReceivedItem { get => _receivedItem; set => _receivedItem = value; }



    public void SetDweller(GameObject argDwellerObj)
    {
        argDwellerObj.transform.position = new Vector3(TRAN_DoorStep.position.x, TRAN_DoorStep.position.y, TRAN_DoorStep.position.z);
        argDwellerObj.transform.parent = this.transform;
        FloorDweller = argDwellerObj.GetComponent<DwellerMeshComposer>();

        // print("setting pos  " + FloorDweller.Id + " " + FloorDweller.name);
        FloorDweller.WarpAgent(TRAN_DoorStep);
        // argDwellerObj.transform.parent = this.transform;
        m_Text_Billboard.text = FloorDweller.Gender + ". " + FloorDweller.AnimalName + " the " + FloorDweller.AnimalType;
        //     DeliveryItemStillOnFloor = true;
        ReceivedItem = false;
        GameObject nf = Instantiate(NavFloor);
        nf.transform.position = new Vector3(TRAN_516Pos.position.x, TRAN_516Pos.position.y, TRAN_516Pos.position.z);
        //   InitialFLoor.SetActive(false);
        BuildingMesh.SetActive(false);
        Instantiate(trmpObstcle, new Vector3(TRAN_MidRoom.position.x, TRAN_MidRoom.position.y, TRAN_MidRoom.position.z), Quaternion.identity);

        //argDwellerObj.GetComponent<NavMeshAgent>().enabled = true;
        //FloorDweller.AgentMustSetTarget(TRAN_DoorStep); ;
        //FloorDweller.WarpAgent(TRAN_DoorStep);
        //nf.transform.parent = this.transform;
    }
    public void InitDwellerAgentNowIGuess()
    {
        //        print("agent init  " + FloorDweller.Id + " " + FloorDweller.name);
        //FloorDweller.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        FloorDweller.Start_Agent();
        FloorDweller.AgentMustSetTarget(TRAN_DoorStep);
        FloorDweller.WarpAgent(TRAN_DoorStep);


    }

    public void SetInitDest()
    {
        FloorDweller.AgentMustSetTarget(TRAN_DoorStep);
    }

    public void WarpInit()
    {
        // FloorDweller.WarpAgent(TRAN_DoorStep);
    }

    int cnt = 0;
    public void MoveNave_To()
    {

        if (cnt == 0)
        {
            FloorDweller.MoveAgentTo(TRAN_SecondaryPos);
        }
        else
            if (cnt == 1) { FloorDweller.MoveAgentTo(TRAN_SpawnPos); }
        else
        if (cnt == 2) { FloorDweller.MoveAgentTo(TRAN_InteractibleMainPos); }
        else
            if (cnt == 3) { FloorDweller.MoveAgentTo(TRAN_DoorStep); }

        cnt++;

        if (cnt > 3) cnt = 0;

    }

    public void ShowHideBArrier(bool argShow)
    {
        Barrier.SetActive(argShow);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
