//#define MakeAnimalModels
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryManager : MonoBehaviour
{


    List<GameObject> LoadedAnimalObjs;
    List<GameObject> LoadedItemObjs;
    List<GameObject> FloorDweller_REFS;
    List<GameObject> FloorItem_REFS;
    List<GameObject> FloorDweller_GO;
    List<GameObject> FloorItem_GO;

    public List<AnimalDweller> AnimalDwellers;
    List<StoryItem> StoryItems;
    GameManager gm;
    List<StoryNode> StoryNodes;
    StoryNode HeadHop;
    StoryNode EndCheck;
    System.Random _random = new System.Random();

    /// <summary>
    /// 
    /// List of Linked Storynodes. Justpoint the HeaNode to element0 , start game , and only move the headnode when correct object has been given to correct animal
    /// 
    /// </summary>

    public List<StoryNode> GameAnimalDwellers;
    int[] ArraIndexes;
    int[] LowerHalfIndexes;
    private void Start()
    {
        gm = GameManager.Instance;
        GameAnimalDwellers = new List<StoryNode>();
        LoadedAnimalObjs = Resources.LoadAll<GameObject>("Animals/PlaceHolders").ToList();

        //for (int a = 0; a < LoadedAnimalObjs.Count; a++)
        //{
        //    AnimalDweller ad = LoadedAnimalObjs[a].GetComponent<AnimalDweller>();
        //    ad.My_type = (GameEnums.AnimalCharcter)a;
        //}
        LoadedItemObjs = Resources.LoadAll<GameObject>("Items/PlaceHolders").ToList();

        LoadedItemObjs.RemoveAt(0); //remove "None"


        LoadedAnimalObjs.Shuffle();
        LoadedItemObjs.Shuffle();

        FloorDweller_REFS = LoadedAnimalObjs.Take<GameObject>(gm.Master_Number_of_Floors).ToList();
        FloorItem_REFS = LoadedItemObjs.Take<GameObject>(gm.Master_Number_of_Floors).ToList();

        FloorDweller_GO = new List<GameObject>();
        AnimalDwellers = new List<AnimalDweller>();
        FloorItem_GO = new List<GameObject>();
        StoryItems = new List<StoryItem>();

        for (int a = 0; a < gm.Master_Number_of_Floors; a++)
        {
            GameObject TheAnimalGO = Instantiate(FloorDweller_REFS[a]);

            FloorDweller_GO.Add(TheAnimalGO);
            AnimalDweller ad = TheAnimalGO.GetComponent<AnimalDweller>();
            ad.UpdateNameText(ad.My_type.ToString());
            ad.Floor_Number = a;
            AnimalDwellers.Add(ad);
        }
        for (int i = 0; i < gm.Master_Number_of_Floors; i++)
        {
            GameObject TheItemGO = Instantiate(FloorItem_REFS[i]);
            FloorItem_GO.Add(TheItemGO);
            StoryItem si = TheItemGO.GetComponent<StoryItem>();
            StoryItems.Add(si);
        }
        Debug.Log("hey");
        CreateStoryNodes();

        ArraIndexes = new int[gm.Master_Number_of_Floors];
        for (int i = 0; i < ArraIndexes.Length; i++)
        {
            ArraIndexes[i] = i;
        }
        LowerHalfIndexes = new int[gm.Master_Number_of_Floors / 2];
        for (int lhi = 0; lhi < LowerHalfIndexes.Length; lhi++)
        {
            LowerHalfIndexes[lhi] = lhi;
        }

        Fisher_Yates(LowerHalfIndexes);
        SwapLowerAndTopFHalves();


        int temp = ArraIndexes[ArraIndexes.Length - 1];
        ArraIndexes[ArraIndexes.Length - 1] = ArraIndexes[0];
        ArraIndexes[0] = temp;

        //DebugArray(LowerHalfIndexes);
        //DebugArray(ArraIndexes);



    }

    void DebugArray(int[] argarra)
    {
        Debug.Log("contents of array ");
        for (int ai = 0; ai < argarra.Length; ai++)
        {
            Debug.Log("[" + ai + "]=" + argarra[ai]);
        }
        Debug.Log("---endPrint");
    }
    void Fisher_Yates(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            // Use Next on random instance with an argument.
            // ... The argument is an exclusive bound.
            //     So we will not go past the end of the array.
            int r = i + _random.Next(n - i);
            int t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }

    void SwapLowerAndTopFHalves()
    {
        int half_upperbound = LowerHalfIndexes.Length;

        for (int lhi = 0; lhi < LowerHalfIndexes.Length; lhi++)
        {
            int arraIndexes_Lowerhalf = LowerHalfIndexes[lhi];
            int temp = ArraIndexes[half_upperbound + lhi];
            ArraIndexes[half_upperbound + lhi] = ArraIndexes[arraIndexes_Lowerhalf];
            ArraIndexes[arraIndexes_Lowerhalf] = temp;
        }


    }
    void MySwap(int[] argarra)
    {
        //  0  1  2  3
        //        /\
        //ln= 4   ||
        int half_upperbound = argarra.Length / 2;
        //half=2
        Debug.Log(half_upperbound);
        //shuffle lowerhalf does nt matter if we still have same elements 
        half_upperbound = 1 / 2;
        Debug.Log(half_upperbound);
        half_upperbound = 3 / 2;

        Debug.Log(half_upperbound);

    }


    List<Tuple<int, int>> GetPairs(int min, int max, System.Random r)
    {
        var items = new List<Tuple<int, int>>();
        var pickedItems = new HashSet<int>();
        int count = (max - min + 1);

        Func<int> randAndCheck = () =>
        {
            int? candidate = null;

            while (candidate == null || pickedItems.Contains(candidate.Value))
                candidate = r.Next(min, max + 1);

            pickedItems.Add(candidate.Value);
            return candidate.Value;
        };

        while (pickedItems.Count != count)
        {
            int firstItem = randAndCheck();
            int secondItem = randAndCheck();

            items.Add(Tuple.Create(firstItem, secondItem));
        }

        return items;
    }
    void CreateStoryNodes()
    {
        StoryNodes = new List<StoryNode>();

        for (int s = 0; s < gm.Master_Number_of_Floors; s++)
        {
            // Link instantiated Animal script to instantited item script 
            AnimalDwellers[s]._HeldItem = StoryItems[s].MyType;
            StoryNode sn = new StoryNode(AnimalDwellers[s].My_type, StoryItems[s].MyType);
            StoryNodes.Add(sn);
            AnimalDwellers[s].AssignHeldObject(FloorItem_GO[s], sn);
        }
        ///
        //
        // can shufflle storynodes byt keep all Go and CS lists ordered 
        //
        ///
        StoryNodes.Shuffle();

        // Link storynodes

        for (int i = 0; i < gm.Master_Number_of_Floors; i++)
        {

            //   Debug.Log(i);
            if (i < gm.Master_Number_of_Floors - 1)
            {
                if (i == 0)
                {
                    //link node0 woth lastnode 
                    StoryNodes[0].Prev_OwedToMe1 = StoryNodes[gm.Master_Number_of_Floors - 1];

                }
                StoryNodes[i].Next_giveto1 = StoryNodes[i + 1];
                StoryNodes[i + 1].Prev_OwedToMe1 = StoryNodes[i];


            }
            //last node
            else
                StoryNodes[i].Next_giveto1 = StoryNodes[0];


        }


        GameAnimalDwellers = StoryNodes.ToList();

        //foreach (StoryNode sn in GameAnimalDwellers)
        //{
        //    // Debug.Log(sn.TheAnimal1);
        //}
        //  Debug.Log("----------");

        GameAnimalDwellers.Shuffle();


        gm.PlaceDwellersOnFloors(FloorDweller_GO);
    }

    void MatchDwellersWithStoryNodeAndObjrctInHand()
    {

    }

    public int GetFloorNumberofForItem(GameEnums.StoryObjects toDeliver)
    {
        for (int a = 0; a < gm.Master_Number_of_Floors; a++)
        {
            AnimalDweller ad = AnimalDwellers[a];
            if (ad.GetStoryNode().Prev_OwedToMe1.ObjectInHand1 == toDeliver)
            {
                Debug.Log("found floor " + a.ToString());
                return ad.Floor_Number;
            }

        }
        return 0;
    }


}
