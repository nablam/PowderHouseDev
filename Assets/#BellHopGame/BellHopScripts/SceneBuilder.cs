using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SceneBuilder : MonoBehaviour
{
    public TextAsset BoyNamescsv;
    public TextAsset GirlNamescsv;

    public GameObject BaseAnimalRef;

    Dictionary<char, List<string>> Dict_BoyNames;
    Dictionary<char, List<string>> Dict_GirlNames;
    Dictionary<char, List<string>> Dict_ItemNames;
    Dictionary<char, List<string>> Dict_AnimalNames;



    List<GameObject> LoadedDeliveryItemObjs;
    public List<GameObject> FloorItem_REFS;
    public List<GameObject> DeliveryItem_Instances;
    List<string> BOYS_AvailableDynamicAnimalNames = new List<string>();
    List<string> GIRLS_AvailableDynamicAnimalNames = new List<string>();
    public List<string> SelectedAnimalNames = new List<string>();
    //  List<GameObject> LoadedAnimalObjs;
    List<GameObject> FloorDweller_REFS;
    public List<GameObject> Dwellers_Instances;
    public GameObject FloorObjRef;
    HotelFloor tempHotelFloor;
    GameObject Hotel;
    public int max = 4;
    const int MAXavailableAnimals = 18;
    int[] path;

    /// <summary>
    /// makes a randome shuffled array making sure no value corresponds to its index
    /// </summary>
    /// <returns></returns>
    bool CreatePath()
    {
        path = new int[max];
        bool success = true;
        int ptr = 0;
        int rptr = 0;
        //int[] path = new int[max];
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


    void UpdateDeliveryItems()
    {

        int x = 0;

    }

    public List<string> TempItemNames = new List<string>();

    public List<string> tl = new List<string>();

    private void Awake()
    {
        if (max > MAXavailableAnimals) max = MAXavailableAnimals;
    }
    // Start is called before the first frame update
    void Start()
    {
        Dict_BoyNames = new Dictionary<char, List<string>>();
        Dict_GirlNames = new Dictionary<char, List<string>>();
        Dict_AnimalNames = new Dictionary<char, List<string>>();
        Dict_ItemNames = new Dictionary<char, List<string>>();
        DeliveryItem_Instances = new List<GameObject>();
        Dwellers_Instances = new List<GameObject>();
        BOYS_AvailableDynamicAnimalNames = Enum.GetNames(typeof(GameEnums.DynAnimal)).ToList();

        GIRLS_AvailableDynamicAnimalNames = Enum.GetNames(typeof(GameEnums.DynAnimal)).ToList();

        for (int x = 0; x < Enum.GetNames(typeof(GameEnums.DynAnimal)).ToList().Count; x++)
        {
            BOYS_AvailableDynamicAnimalNames[x] = "Mr." + BOYS_AvailableDynamicAnimalNames[x];
            GIRLS_AvailableDynamicAnimalNames[x] = "Mrs." + GIRLS_AvailableDynamicAnimalNames[x];
        }

        SelectedAnimalNames.AddRange(BOYS_AvailableDynamicAnimalNames);

        SelectedAnimalNames.AddRange(GIRLS_AvailableDynamicAnimalNames);

        SelectedAnimalNames.Shuffle();

        BoyNamescsv = Resources.Load("Data/BoyNames237") as TextAsset;
        GirlNamescsv = Resources.Load("Data/GirlNames323") as TextAsset;

        MakeDictionaries(BoysSplitCsvGrid, BoyNamescsv, Dict_BoyNames);
        MakeDictionaries(GirlsSplitCsvGrid, GirlNamescsv, Dict_GirlNames);



        CreatePath();

        LoadedDeliveryItemObjs = Resources.LoadAll<GameObject>("Items/NiceConstructedObjects").ToList();


        //  LoadedAnimalObjs = Resources.LoadAll<GameObject>("Animals/PlaceHolders").ToList();
        LoadedDeliveryItemObjs.Shuffle();

        List<string> temp = SelectedAnimalNames.Take<string>(max).ToList();
        SelectedAnimalNames = temp;



        FloorItem_REFS = LoadedDeliveryItemObjs.Take<GameObject>(max).ToList();





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


            // GameObject Dweller = Instantiate(FloorDweller_REFS[i]);
            GameObject DeliveryObj = Instantiate(FloorItem_REFS[i]);

            F.transform.parent = Hotel.transform;
        }
    }


    string[,] BoysSplitCsvGrid;
    string[,] GirlsSplitCsvGrid;

    GameObject BuildDynamicAnimals()
    {
        GameObject Dweller = null;
        FloorDweller_REFS = new List<GameObject>();
        return Dweller;
    }

    void MakeDictionaries(string[,] argSpitGrid, TextAsset argCsv, Dictionary<char, List<string>> argDict)
    {
        // CSVReader.SplitCsvToGrid(CSVtoUse);
        argSpitGrid = CSVReader.SplitCsvGrid(argCsv.text);
        int GridLen = argSpitGrid.GetLength(1) - 2; //first and last i guess

        for (int r = 0; r < GridLen; r++)
        {
            string name = argSpitGrid[0, r];
            char c = name[0];

            if (argDict.ContainsKey(c))
            {
                argDict[c].Add(name);
            }
            else
            {
                argDict.Add(c, new List<string>() { name });
            }
        }





    }



    // Update is called once per frame
    void Update()
    {

    }
}

