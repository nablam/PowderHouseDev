using UnityEngine;

public class HotelFloor : MonoBehaviour
{
    public GameObject Barrier;
    public Transform DwellerPos;
    public GameObject BackWall;
    public GameObject BaseCamPos;
    public TMPro.TextMeshPro m_Text_Billboard;

    int _floorNumber;
    // bool _deliveryItemStillOnFloor;
    bool _receivedItem;

    public DwellerMeshComposer FloorDweller;

    public int FloorNumber { get => _floorNumber; set => _floorNumber = value; }
    // public bool DeliveryItemStillOnFloor { get => _deliveryItemStillOnFloor; set => _deliveryItemStillOnFloor = value; }
    public bool ReceivedItem { get => _receivedItem; set => _receivedItem = value; }



    public void SetDweller(GameObject argDwellerObj)
    {

        argDwellerObj.transform.position = new Vector3(DwellerPos.position.x, DwellerPos.position.y, DwellerPos.position.z);
        argDwellerObj.transform.parent = this.transform;
        FloorDweller = argDwellerObj.GetComponent<DwellerMeshComposer>();
        m_Text_Billboard.text = FloorDweller.Gender + ". " + FloorDweller.AnimalName + " the " + FloorDweller.AnimalType;
        //     DeliveryItemStillOnFloor = true;
        ReceivedItem = false;
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
