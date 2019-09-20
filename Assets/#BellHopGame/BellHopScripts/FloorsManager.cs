using UnityEngine;

public class FloorsManager : MonoBehaviour
{
    /// <summary>
    /// This will start with floor_0 being the curent active floor
    /// </summary>
    /// 
    GameManager gm;
    public GameObject[] Floors;
    private GameObject CurFLoor;
    public int CurFloorNumber = 0;

    void Awake()
    {
        gm = GameManager.Instance;

        // NumberofFloorsObjectsAlreadyInScene = transform.childCount;
        Floors = new GameObject[gm.Master_Number_of_Floors];
        for (int f = 0; f < gm.Master_Number_of_Floors; f++)
        {
            Floors[f] = transform.GetChild(f).gameObject;
        }
    }

    public void InitializeFloor_0_Active()
    {
        for (int f = 0; f < gm.Master_Number_of_Floors; f++)
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
        gm.Master_CurentFloorNumber = CurFloorNumber;
    }

    public void SetFloorAimalObj(int argFloorNumber, GameObject argAnimal)
    {
        argAnimal.transform.parent = Floors[argFloorNumber].transform;

    }

    public int GEtCurrFloorNumber() { return this.CurFloorNumber; }


}
