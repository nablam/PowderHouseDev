using UnityEngine;

public class HotelFloor : MonoBehaviour
{
    public GameObject Barrier;
    public Transform DwellerPos;
    public GameObject BackWall;
    public GameObject BaseCamPos;

    int _floorNumber;

    public int FloorNumber { get => _floorNumber; set => _floorNumber = value; }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
