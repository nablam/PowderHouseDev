using UnityEngine;

public class FloorsManager : MonoBehaviour
{
    /// <summary>
    /// This will start with floor_0 being the curent active floor
    /// </summary>

    public GameObject[] Floors;
    private GameObject CurFLoor;
    private int CurFloorNumber = 0;
    int NumberofFloorsObjectsAlreadyInScene;
    void Awake()
    {
        NumberofFloorsObjectsAlreadyInScene = transform.childCount;
        Floors = new GameObject[NumberofFloorsObjectsAlreadyInScene];
        for (int f = 0; f < NumberofFloorsObjectsAlreadyInScene; f++)
        {
            Floors[f] = transform.GetChild(f).gameObject;
        }
    }

    public void InitializeFloor_0_Active()
    {
        for (int f = 0; f < NumberofFloorsObjectsAlreadyInScene; f++)
        {

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

    public void SetFloorAimalObj(int argFloorNumber, GameObject argAnimal)
    {
        argAnimal.transform.parent = Floors[argFloorNumber].transform;

    }

    public int GEtCurrFloorNumber() { return this.CurFloorNumber; }


}
