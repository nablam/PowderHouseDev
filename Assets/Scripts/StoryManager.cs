using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public Material m0;
    public Material m1;
    public Material m2;
    public Material m3;
    public Material m4;
    public Material m5;
    public Material m6;
    public Material m7;
    public Material m8;


    public List<string> AllAnimals;
    public List<string> AllObjects;
    public List<GameObject> LoadedAnimalObjs;
    List<StoryPacket> AllPackets = new List<StoryPacket>();

    Dictionary<GameEnums.AnimalCharcter, GameEnums.StoryObjects> FloorDwellersAndObjectNeeded;
    const int NumberofFloors = 9;
    //temp gameobject ref to make quick prefabs
    // public GameObject RefAnimal;

    public FloorsManager MyFloorManager;

    void Start()
    {
        LoadedAnimalObjs = Resources.LoadAll<GameObject>("Animals/PlaceHolders").ToList();
        AllAnimals = Enum.GetNames(typeof(GameEnums.AnimalCharcter)).ToList();
        AllObjects = Enum.GetNames(typeof(GameEnums.StoryObjects)).ToList();
        //TempMakeAnimals();
        RandomizeAnimalsList();
        RandomizeObjectsList();
        FloorDwellersAndObjectNeeded = new Dictionary<GameEnums.AnimalCharcter, GameEnums.StoryObjects>();
        CreateFloorDwellers();

        SetWhoIsOnWhatFloor();
    }
    GameObject FindLoadedAnimalByname(string argAnimalName)
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

    //void TempMakeAnimals()
    //{

    //    for (int a = 0; a < AllAnimals.Count; a++)
    //    {

    //        GameObject animal = Instantiate(RefAnimal);
    //        animal.name = AllAnimals[a] + "_Obj";
    //        animal.transform.GetChild(0).GetComponent<TextMesh>().text = AllAnimals[a];
    //        animal.transform.GetChild(1).GetComponent<TextMesh>().text = "";

    //        //Color RandColor = new Color(
    //        //UnityEngine.Random.Range(0f, 1f),
    //        //UnityEngine.Random.Range(0f, 1f),
    //        //UnityEngine.Random.Range(0f, 1f)
    //        //);

    //        if (a % 8 == 0)
    //            animal.GetComponent<MeshRenderer>().material = m0;
    //        else
    //                        if (a % 9 == 0)
    //            animal.GetComponent<MeshRenderer>().material = m0;
    //        else
    //                        if (a % 9 == 1)
    //            animal.GetComponent<MeshRenderer>().material = m1;
    //        else
    //                        if (a % 9 == 2)
    //            animal.GetComponent<MeshRenderer>().material = m2;
    //        else
    //                        if (a % 9 == 3)
    //            animal.GetComponent<MeshRenderer>().material = m3;
    //        else
    //                        if (a % 9 == 4)
    //            animal.GetComponent<MeshRenderer>().material = m4;
    //        else
    //                        if (a % 9 == 5)
    //            animal.GetComponent<MeshRenderer>().material = m5;
    //        else
    //                        if (a % 9 == 6)
    //            animal.GetComponent<MeshRenderer>().material = m6;
    //        else
    //                        if (a % 9 == 7)
    //            animal.GetComponent<MeshRenderer>().material = m7;
    //        else
    //                        if (a % 9 == 8)
    //            animal.GetComponent<MeshRenderer>().material = m8;


    //    }
    //}

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


    void CreateFloorDwellers()
    {
        for (int f = 0; f < NumberofFloors; f++)
        {
            GameEnums.AnimalCharcter TheAnimal = (GameEnums.AnimalCharcter)Enum.Parse(typeof(GameEnums.AnimalCharcter), AllAnimals[f], true);
            GameEnums.StoryObjects TheObject = (GameEnums.StoryObjects)Enum.Parse(typeof(GameEnums.StoryObjects), AllObjects[f], true);
            FloorDwellersAndObjectNeeded.Add(TheAnimal, TheObject);
        }
    }

    void SetWhoIsOnWhatFloor()
    {

        for (int f = 0; f < NumberofFloors; f++)
        {
            MyFloorManager.SetFloorAimalObj(f, FindLoadedAnimalByname(FloorDwellersAndObjectNeeded.ElementAt(f).Key.ToString()));
            Debug.Log("floor + " + f + " -> " + FloorDwellersAndObjectNeeded.ElementAt(f).Key.ToString() + " who needs " + FloorDwellersAndObjectNeeded.ElementAt(f).Value.ToString());
        }

        MyFloorManager.InitializeFloor_0_Active();

    }
}
