using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneBuilder : MonoBehaviour
{

    List<GameObject> LoadedDeliveryItemObjs;
    List<GameObject> FloorItem_REFS;
    public List<GameObject> DeliveryItem_Instances;
    List<GameObject> LoadedAnimalObjs;
    List<GameObject> FloorDweller_REFS;
    public List<GameObject> Dwellers_Instances;
    public GameObject FloorObjRef;
    HotelFloor tempHotelFloor;
    GameObject Hotel;
    public int max = 4;

    /// <summary>
    /// makes a randome shuffled array making sure no value corresponds to its index
    /// </summary>
    /// <returns></returns>
    bool CreatePath()
    {
        bool success = true;
        int ptr = 0;
        int rptr = 0;
        int[] path = new int[max];
        bool[] used = new bool[max];

        for (int x = 0; x < max; x++)
        {
            path[x] = 0;
            used[x] = false;
        }


        int MAX = max - (max % 2);

        for (int i = 0; i < MAX; i++)
        {
            ptr = i;
            if (used[ptr]) continue;
            used[ptr] = true;
            do
            {
                rptr = Random.Range(ptr, max) % MAX;
            }
            while ((used[rptr] != false) || rptr <= ptr);
            used[rptr] = true;
            path[rptr] = ptr;
            path[ptr] = rptr;
        }


        if (max % 2 == 1)
        {
            rptr = Random.Range(0, MAX);
            path[MAX] = path[rptr];
            path[rptr] = MAX;
        }


        print("____PATH___");
        for (int x = 0; x < max; x++)
        {
            if (path[x] == x) success = false;
            print(path[x]);
        }

        return success;
    }





    // Start is called before the first frame update
    void Start()
    {
        CreatePath();

        LoadedDeliveryItemObjs = Resources.LoadAll<GameObject>("Items/NiceConstructedObjects").ToList();
        LoadedAnimalObjs = Resources.LoadAll<GameObject>("Animals/PlaceHolders").ToList();
        LoadedDeliveryItemObjs.Shuffle();
        LoadedAnimalObjs.Shuffle();
        FloorItem_REFS = LoadedDeliveryItemObjs.Take<GameObject>(max).ToList();
        FloorDweller_REFS = LoadedAnimalObjs.Take<GameObject>(max).ToList();


        Hotel = new GameObject();
        Hotel.transform.position = new Vector3(0, 0, 0);
        Hotel.name = "Hotel";

        for (int i = 0; i < max; i++)
        {
            GameObject F = Instantiate(FloorObjRef);
            F.name = "floor_" + i;
            F.transform.position = new Vector3(F.transform.position.x, i * 7f, F.transform.position.z);
            HotelFloor hf = F.GetComponent<HotelFloor>();
            hf.FloorNumber = i;

            F.transform.parent = Hotel.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

