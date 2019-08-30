//#define MakeAnimalModels
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryManager : MonoBehaviour
{


    List<GameObject> LoadedAnimalObjs;
    List<GameObject> LoadedItemObjs;
    List<GameObject> FloorDweller_REFS;
    List<GameObject> FloorItem_REFS;
    public List<GameObject> FloorDweller_GO;
    List<GameObject> FloorItem_GO;

    List<AnimalDweller> AnimalDwellers;
    List<StoryItem> StoryItems;
    GameManager gm;
    List<StoryNode> StoryNodes;
    StoryNode HeadHop;
    StoryNode EndCheck;
    /// <summary>
    /// 
    /// List of Linked Storynodes. Justpoint the HeaNode to element0 , start game , and only move the headnode when correct object has been given to correct animal
    /// 
    /// </summary>

    public List<StoryNode> GameAnimalDwellers;

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

            Debug.Log(i);
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
        Debug.Log("----------");

        GameAnimalDwellers.Shuffle();


        foreach (AnimalDweller sn in AnimalDwellers)
        {
            Debug.Log(sn.My_type);

        }

        //StoryNodes[gm.Master_Number_of_Floors - 1].Next_giveto1 = null;
        // HeadHop = StoryNodes[0];

        HeadHop = EndCheck = AnimalDwellers[0].GetStoryNode();
        //  EndCheck = HeadHop.Prev_OwedToMe1;
        bool firstrstcheck = false;
        while (HeadHop.Next_giveto1 != EndCheck)
        {
            if (HeadHop.Next_giveto1 == null) return;
            Debug.Log(HeadHop.TheAnimal1.ToString() + " give " + HeadHop.ObjectInHand1.ToString() + " to " + HeadHop.Next_giveto1.TheAnimal1.ToString());
            HeadHop = HeadHop.Next_giveto1;
        }
        HeadHop = EndCheck;
        Debug.Log(HeadHop.TheAnimal1.ToString() + " give " + HeadHop.ObjectInHand1.ToString() + " to " + HeadHop.Next_giveto1.TheAnimal1.ToString());
        //Debug.Log("endq " + RandomizeTheNeedRelations.Count);

        gm.PlaceDwellersOnFloors(FloorDweller_GO);
    }

    void MatchDwellersWithStoryNodeAndObjrctInHand()
    {

    }
}
