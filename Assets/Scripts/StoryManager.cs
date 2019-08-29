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


    GameManager gm;
    public FloorsManager MyFloorManager;
    bool[] SwapItemUniqueValidation;
    void Start()
    {
        gm = GameManager.Instance;
        LoadedAnimalObjs = Resources.LoadAll<GameObject>("Animals/PlaceHolders").ToList();
        LoadedItemObjs = Resources.LoadAll<GameObject>("Items/PlaceHolders").ToList();
        SelectedShuffeledItemObjs = new List<GameObject>();
        AllAnimals = Enum.GetNames(typeof(GameEnums.AnimalCharcter)).ToList();
        AllObjects = Enum.GetNames(typeof(GameEnums.StoryObjects)).ToList();
        AllObjects.RemoveAt(0);
        SwapItemUniqueValidation = new bool[gm.Master_Number_of_Floors];

        RandomizeAnimalsList();
        RandomizeObjectsList();
        FloorDwellersAndObjectNeeded = new Dictionary<GameEnums.AnimalCharcter, GameEnums.StoryObjects>();

        CreateFloorDwellersDictionary();

        CreateShuffledListOfItems();

        BuildPacketList();

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
        for (int f = 0; f < gm.Master_Number_of_Floors; f++)
        {
            GameEnums.AnimalCharcter TheAnimal = (GameEnums.AnimalCharcter)Enum.Parse(typeof(GameEnums.AnimalCharcter), AllAnimals[f], true);
            GameEnums.StoryObjects TheObject = (GameEnums.StoryObjects)Enum.Parse(typeof(GameEnums.StoryObjects), AllObjects[f], true);
            FloorDwellersAndObjectNeeded.Add(TheAnimal, TheObject);
        }
    }
    public List<string> TempListOfFloorItemNames = new List<string>();

    //also gameobject reflist
    void CreateShuffledListOfItems()
    {
        // List<string> TempListOfFloorItemNames = new List<string>();
        for (int x = 0; x < gm.Master_Number_of_Floors; x++)
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

    void BuildPacketList()
    {

        for (int i = 0; i < FloorDwellersAndObjectNeeded.Count; i++)
        {

            int AnimalCharcterFloorNumber = i;
            GameEnums.AnimalCharcter FloorAnimalCharacter = FloorDwellersAndObjectNeeded.ElementAt(i).Key;
            GameEnums.StoryObjects ObjectNeededByFloorAnimal = FloorDwellersAndObjectNeeded.ElementAt(i).Value;
            GameEnums.StoryObjects ObjectToSendToRecipient = SelectedShuffeledItemObjs[i].GetComponent<StoryItem>().MyType;
            int RecipentAnimalCharcterFloorNumber = CalcRecipientFloor(ObjectToSendToRecipient);
            GameEnums.AnimalCharcter RecipientFloorAnimalCharacter = FloorDwellersAndObjectNeeded.ElementAt(RecipentAnimalCharcterFloorNumber).Key;
            int FloofOfAnimalWhoHasWhatINeed = GetFloorOFNeededGameObject(ObjectNeededByFloorAnimal);
            GameEnums.AnimalCharcter WhoHasMyStuff = FloorDwellersAndObjectNeeded.ElementAt(FloofOfAnimalWhoHasWhatINeed).Key;

            StoryPacket AStoryPacket = new StoryPacket(AnimalCharcterFloorNumber, FloorAnimalCharacter, ObjectNeededByFloorAnimal, RecipentAnimalCharcterFloorNumber, RecipientFloorAnimalCharacter, ObjectToSendToRecipient, FloofOfAnimalWhoHasWhatINeed, WhoHasMyStuff);

            AllPackets.Add(AStoryPacket);

        }

    }
    void SetWhoIsOnWhatFloor()
    {

        for (int f = 0; f < gm.Master_Number_of_Floors; f++)
        {
            GameObject FloorDwellerPrefab = FindLoadedAnimalByname_InstantiateGO(FloorDwellersAndObjectNeeded.ElementAt(f).Key.ToString());
            AnimalDweller AD = FloorDwellerPrefab.GetComponent<AnimalDweller>();
            if (AD == null)
            {
                AD = FloorDwellerPrefab.AddComponent<AnimalDweller>();
            }
            AD.InitMyPacket(AllPackets.ElementAt(f));
            FloorDwellerPrefab.transform.GetChild(1).GetComponent<TextMesh>().text = FloorDwellersAndObjectNeeded.ElementAt(f).Value.ToString();
            GameObject ShuffledItemAnimalIsHolding = SelectedShuffeledItemObjs[f];
            StoryItem si = ShuffledItemAnimalIsHolding.GetComponent<StoryItem>();
            AD.UpdateHeldObject(ShuffledItemAnimalIsHolding, si.MyType);

            ShuffledItemAnimalIsHolding.transform.position = FloorDwellerPrefab.transform.GetChild(2).position;//theHand transform
            ShuffledItemAnimalIsHolding.transform.parent = FloorDwellerPrefab.transform.GetChild(2);

            MyFloorManager.SetFloorAimalObj(f, FloorDwellerPrefab);
            //  Debug.Log("floor + " + f + " -> " + FloorDwellersAndObjectNeeded.ElementAt(f).Key.ToString() + " who needs " + FloorDwellersAndObjectNeeded.ElementAt(f).Value.ToString());
        }

        MyFloorManager.InitializeFloor_0_Active();

    }

    public string GetFloorDwellerAsInfo(int argFloor)
    {
        return "fl_" + argFloor + "        The " + FloorDwellersAndObjectNeeded.ElementAt(argFloor).Key.ToString() + " needs -> " + FloorDwellersAndObjectNeeded.ElementAt(argFloor).Value.ToString();
    }

    int GetDwellerFloor(GameEnums.AnimalCharcter argDweller)
    {
        int i = 0;
        for (int x = 0; x < FloorDwellersAndObjectNeeded.Count; x++)
        {
            if (FloorDwellersAndObjectNeeded.ElementAt(x).Key == argDweller)
            {
                i = x;
                break;
            }

        }

        return i;
    }

    int GetFloorOFNeededGameObject(GameEnums.StoryObjects argobj)
    {
        int floorOfGO = 0;
        for (int x = 0; x < gm.Master_Number_of_Floors; x++)
        {

            StoryItem si = SelectedShuffeledItemObjs[x].GetComponent<StoryItem>();
            if (si.MyType == argobj)
            {
                floorOfGO = x;
                break;
            }
        }
        return floorOfGO;
    }

    int CalcRecipientFloor(GameEnums.StoryObjects argMyObj)
    {
        int i = 0;
        for (int x = 0; x < FloorDwellersAndObjectNeeded.Count; x++)
        {
            if (FloorDwellersAndObjectNeeded.ElementAt(x).Value == argMyObj)
            {
                i = x;
                break;
            }

        }
        return i;
    }
}
