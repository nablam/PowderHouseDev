using UnityEngine;

public class FloorsManager : MonoBehaviour
{
    /// <summary>
    /// This will start with floor_0 being the curent active floor
    /// </summary>

    public GameObject[] Floors;
    private GameObject CurFLoor;
    private int CurFloorNumber = 0;
    void Start()
    {
        int NumberofFloors = transform.childCount;
        Floors = new GameObject[NumberofFloors];
        for (int f = 0; f < NumberofFloors; f++)
        {
            Floors[f] = transform.GetChild(f).gameObject;
            if (f > 0)
            {
                Floors[f].SetActive(false);
            }
        }
    }

    public void SetCurFloor(int argFloorNumber)
    {
        Floors[CurFloorNumber].SetActive(false);
        Floors[argFloorNumber].SetActive(true);
        CurFloorNumber = argFloorNumber;
    }


}
