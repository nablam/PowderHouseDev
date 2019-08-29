//#define MakeAnimalModels
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryManager : MonoBehaviour
{



    public List<string> AllAnimals;
    public List<string> AllObjects;
    List<GameObject> LoadedAnimalObjs;
    public List<GameObject> SelectedShuffeledItemObjs;
    List<GameObject> LoadedItemObjs;
    List<StoryPacket> AllPackets = new List<StoryPacket>();

    Dictionary<GameEnums.AnimalCharcter, GameEnums.StoryObjects> FloorDwellersAndObjectNeeded;
    const int NumberofFloors = 9;


    public FloorsManager MyFloorManager;
    bool[] SwapItemUniqueValidation;
    void Start()
    {
        LoadedAnimalObjs = Resources.LoadAll<GameObject>("Animals/PlaceHolders").ToList();
        LoadedItemObjs = Resources.LoadAll<GameObject>("Items/PlaceHolders").ToList();
        SelectedShuffeledItemObjs = new List<GameObject>();
        AllAnimals = Enum.GetNames(typeof(GameEnums.AnimalCharcter)).ToList();
        AllObjects = Enum.GetNames(typeof(GameEnums.StoryObjects)).ToList();
        SwapItemUniqueValidation = new bool[NumberofFloors];

        RandomizeAnimalsList();
        RandomizeObjectsList();
        FloorDwellersAndObjectNeeded = new Dictionary<GameEnums.AnimalCharcter, GameEnums.StoryObjects>();

        CreateFloorDwellersDictionary();

        CreateShuffledListOfItems();
        SetWhoIsOnWhatFloor();
    }
    GameObject FindLoadedAnimalByname_InstantiateGO(string argAnimalName)
    {
        GameObject foundAimal = null;
        foreach (GameObject animalobj in LoadedAnimalObjs)
        {
            if (animalobj.name.Contains(argAnimalName))
            {

                foundAimal = Instantiate(animalobj);
                foundAimal.name = argAnimalName;
            }
        }
        return foundAimal;
    }

    GameObject FindLoadedItemByname_InstantiateGO(string argItemName)
    {
        GameObject foundItem = null;
        foreach (GameObject LdedItm in LoadedItemObjs)
        {
            if (LdedItm.name.Contains(argItemName))
            {

                foundItem = Instantiate(LdedItm);
                foundItem.name = argItemName;
            }
        }
        return foundItem;
    }



    void RandomizeAnimalsList()
    {
        for (int x = 0; x < 100; x++)
        {
            int curItemIndex = x % AllAnimals.Count;
            int randomSwapItem = UnityEngine.Random.Range(0, AllAnimals.Count - 1);
            string Temp_curItem = AllAnimals[curItemIndex];
            AllAnimals[curItemIndex] = AllAnimals[randomSwapItem];
            AllAnimals[randomSwapItem] = Temp_curItem;
        }
    }


    void RandomizeObjectsList()
    {
        for (int x = 0; x < 100; x++)
        {
            int curItemIndex = x % AllObjects.Count;
            int randomSwapItem = UnityEngine.Random.Range(0, AllObjects.Count - 1);
            string Temp_curItem = AllObjects[curItemIndex];
            AllObjects[curItemIndex] = AllObjects[randomSwapItem];
            AllObjects[randomSwapItem] = Temp_curItem;
        }
    }


    void CreateFloorDwellersDictionary()
    {
        for (int f = 0; f < NumberofFloors; f++)
        {
            GameEnums.AnimalCharcter TheAnimal = (GameEnums.AnimalCharcter)Enum.Parse(typeof(GameEnums.AnimalCharcter), AllAnimals[f], true);
            GameEnums.StoryObjects TheObject = (GameEnums.StoryObjects)Enum.Parse(typeof(GameEnums.StoryObjects), AllObjects[f], true);
            FloorDwellersAndObjectNeeded.Add(TheAnimal, TheObject);
        }
    }

    //also gameobject reflist
    void CreateShuffledListOfItems()
    {
        List<string> TempListOfFloorItemNames = new List<string>();
        for (int x = 0; x < NumberofFloors; x++)
        {
            TempListOfFloorItemNames.Add(FloorDwellersAndObjectNeeded.ElementAt(x).Value.ToString());
        }
        TempListOfFloorItemNames.Shuffle();

        foreach (string str in TempListOfFloorItemNames)
        {
            Debug.Log(str);
            SelectedShuffeledItemObjs.Add(FindLoadedItemByname_InstantiateGO(str));
        }
    }


    void SetWhoIsOnWhatFloor()
    {

        for (int f = 0; f < NumberofFloors; f++)
        {
            GameObject FloorDwellerPrefab = FindLoadedAnimalByname_InstantiateGO(FloorDwellersAndObjectNeeded.ElementAt(f).Key.ToString());
            FloorDwellerPrefab.transform.GetChild(1).GetComponent<TextMesh>().text = FloorDwellersAndObjectNeeded.ElementAt(f).Value.ToString();
            GameObject ShuffledItemAnimalIsHolding = SelectedShuffeledItemObjs[f];
            ShuffledItemAnimalIsHolding.transform.position = FloorDwellerPrefab.transform.GetChild(2).position;//theHand transform
            ShuffledItemAnimalIsHolding.transform.parent = FloorDwellerPrefab.transform.GetChild(2);

            MyFloorManager.SetFloorAimalObj(f, FloorDwellerPrefab);
            Debug.Log("floor + " + f + " -> " + FloorDwellersAndObjectNeeded.ElementAt(f).Key.ToString() + " who needs " + FloorDwellersAndObjectNeeded.ElementAt(f).Value.ToString());
        }

        MyFloorManager.InitializeFloor_0_Active();

    }

    public string GetFloorDwellerAsInfo(int argFloor)
    {
        return "fl_" + argFloor + "        The " + FloorDwellersAndObjectNeeded.ElementAt(argFloor).Key.ToString() + " needs -> " + FloorDwellersAndObjectNeeded.ElementAt(argFloor).Value.ToString();
    }
}
