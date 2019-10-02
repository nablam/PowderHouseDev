using UnityEngine;

public class HotelFloor : MonoBehaviour
{
    public GameObject Barrier;
    public Transform DwellerPos;
    public GameObject BackWall;
    public GameObject BaseCamPos;
    public TMPro.TextMeshPro m_Text_Billboard;

    int _floorNumber;

    public DwellerMeshComposer FloorDweller;

    public int FloorNumber { get => _floorNumber; set => _floorNumber = value; }


    public void SetDweller(GameObject argDwellerObj)
    {

        argDwellerObj.transform.position = new Vector3(DwellerPos.position.x, DwellerPos.position.y, DwellerPos.position.z);
        argDwellerObj.transform.parent = this.transform;
        FloorDweller = argDwellerObj.GetComponent<DwellerMeshComposer>();
        m_Text_Billboard.text = FloorDweller.Gender + ". " + FloorDweller.AnimalName + " the " + FloorDweller.AnimalType;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
