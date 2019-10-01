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


    //         b.r     list boy names start with r
    //         i.r      list of items that start with i 
    Dictionary<string, ListManager> DICT_MASTER;


    //Dictionary<char, List<string>> Dict_AnimalNames;



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
    const int MAXavailableAnimals = 12;
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
        //Dict_AnimalNames = new Dictionary<char, List<string>>();
        Dict_ItemNames = new Dictionary<char, List<string>>();
        DICT_MASTER = new Dictionary<string, ListManager>();

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


        for (int x = 0; x < Enum.GetNames(typeof(GameEnums.MyItems)).ToList().Count; x++)
        {

            string itemname = ((GameEnums.MyItems)x).ToString().ToLower();
            char c = itemname[0];

            if (Dict_ItemNames.ContainsKey(c))
            {
                Dict_ItemNames[c].Add(itemname);
            }
            else
            {
                Dict_ItemNames.Add(c, new List<string>() { itemname });
            }

        }



        CreatePath();

        LoadedDeliveryItemObjs = Resources.LoadAll<GameObject>("Items/NiceConstructedObjects").ToList();


        //  LoadedAnimalObjs = Resources.LoadAll<GameObject>("Animals/PlaceHolders").ToList();

        //TODO noneed to shuffle we need to get a list of alliteration objects 
        // LoadedDeliveryItemObjs.Shuffle();

        List<string> temp = SelectedAnimalNames.Take<string>(max).ToList();
        SelectedAnimalNames = temp;

        HashSet<char> UniqueCharToUse = new HashSet<char>();




        foreach (string s in SelectedAnimalNames)
        {

            string[] words = s.Split('.');
            UniqueCharToUse.Add(words[1][0]);
        }

        foreach (char c in UniqueCharToUse)
        {
            Dict_BoyNames[c].Shuffle();
            Dict_GirlNames[c].Shuffle();
            Dict_ItemNames[c].Shuffle();
        }





        //create MAster Dictionary based on selected animals 











        for (int M = 0; M < SelectedAnimalNames.Count; M++)
        {

            string name = SelectedAnimalNames[M];
            char c = name.Split('.')[1][0];
            string MasterKEY = (name.Split('.')[0].Length == 3) ? "g." : "b.";  //  Mr.Rabbit  => b.r 

            MasterKEY = MasterKEY + c;

            Debug.Log(SelectedAnimalNames[M] + " " + " " + name.Split('.')[1] + " " + MasterKEY);

            if (!DICT_MASTER.ContainsKey(MasterKEY))
            {


                ListManager Lm = new ListManager();
                if (MasterKEY[0] == 'b')
                {
                    Lm.AssignList(Dict_BoyNames[c]);
                }
                else if (MasterKEY[0] == 'g')
                {
                    Lm.AssignList(Dict_GirlNames[c]);
                }


                DICT_MASTER.Add(MasterKEY, Lm);

            }



            // add item names to master dictionary

            string MasterItemKEY = "i." + c;  //  i.r   




            if (!DICT_MASTER.ContainsKey(MasterItemKEY))
            {

                ListManager Lmi = new ListManager();
                if (Dict_ItemNames[c] != null)
                {
                    Lmi.AssignList(Dict_ItemNames[c]);  // rope, root, roller
                    DICT_MASTER.Add(MasterItemKEY, Lmi);
                }
            }



        }

        //TODO
        // FloorItem_REFS = LoadedDeliveryItemObjs.Take<GameObject>(max).ToList();





        Hotel = new GameObject();
        Hotel.transform.position = new Vector3(0, 0, 0);
        Hotel.name = "Hotel";

        Debug.Log("-----------------------");

        for (int i = 0; i < max; i++)
        {
            GameObject F = Instantiate(FloorObjRef);
            F.name = "floor_" + i;
            F.transform.position = new Vector3(F.transform.position.x, i * 7f, F.transform.position.z);
            HotelFloor hf = F.GetComponent<HotelFloor>();
            hf.FloorNumber = i;


            GameObject Dweller = Instantiate(BaseAnimalRef, new Vector3(-1.37f, -1.12f, 1.08f), Quaternion.Euler(0, 180, 0));
            DwellerMeshComposer dmc = Dweller.GetComponent<DwellerMeshComposer>();




            string searchkey;
            string name1 = SelectedAnimalNames[i];
            char c = name1.Split('.')[1][0];
            searchkey = (name1.Split('.')[0].Length == 3) ? "g." : "b.";  //  Mr.Rabbit  => b.r 

            searchkey = searchkey + c;




            string animalName = SelectedAnimalNames[i].Split('.')[1];




            Debug.Log(SelectedAnimalNames[i] + " " + " " + animalName + " " + searchkey);
            GameEnums.DynAnimal a = (GameEnums.DynAnimal)Enum.Parse(typeof(GameEnums.DynAnimal), animalName);


            dmc.Make(a, DICT_MASTER[searchkey].NextItem());
            Dweller.transform.parent = F.transform;
            //GameObject DeliveryObj = Instantiate(FloorItem_REFS[i]);

            F.transform.parent = Hotel.transform;
        }
    }

    //string RoundRobinOnaList(List<string> argList)
    //{
    //    int i = 0;
    //    int s = argList.Count;
    //    string output = 

    //    i++;
    //    if (i > s) i = 0;
    //}


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
            string name = argSpitGrid[0, r].ToLower();
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

public class ListManager
{

    List<string> _list;
    int i;

    public ListManager()
    {
        i = 0;

    }

    public ListManager(List<string> arglist)
    {
        i = 0;
        _list = arglist;
    }

    public void AssignList(List<string> arglist)
    {
        _list = arglist;
    }


    public void AddToME(string argstr)
    {
        _list.Add(argstr);
    }

    public void ShuffleList()
    {
        _list.Shuffle();
    }


    public string NextItem()
    {

        if (i >= _list.Count)
        {
            i = 0;
        }

        string output = _list[i];
        i++;
        return output;
    }




}


