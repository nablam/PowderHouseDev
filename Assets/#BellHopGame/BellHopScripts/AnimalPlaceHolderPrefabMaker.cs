using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPlaceHolderPrefabMaker : MonoBehaviour
{

    public GameObject RefAnimal;
    public GameObject RefItem;

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
    public List<string> AllItems;
    public List<GameObject> LoadedAnimalObjs;

    void TempMakeAnimals()
    {

        for (int a = 0; a < AllAnimals.Count; a++)
        {

            GameObject animal = Instantiate(RefAnimal);
            animal.name = AllAnimals[a] + "_Obj";
            animal.transform.GetChild(0).GetComponent<TextMesh>().text = AllAnimals[a];
            animal.transform.GetChild(1).GetComponent<TextMesh>().text = "";


            animal.GetComponent<AnimalDweller>().My_type = (GameEnums.AnimalCharcter)a;


            if (a % 8 == 0)
                animal.GetComponent<MeshRenderer>().material = m0;
            else
                            if (a % 9 == 0)
                animal.GetComponent<MeshRenderer>().material = m0;
            else
                            if (a % 9 == 1)
                animal.GetComponent<MeshRenderer>().material = m1;
            else
                            if (a % 9 == 2)
                animal.GetComponent<MeshRenderer>().material = m2;
            else
                            if (a % 9 == 3)
                animal.GetComponent<MeshRenderer>().material = m3;
            else
                            if (a % 9 == 4)
                animal.GetComponent<MeshRenderer>().material = m4;
            else
                            if (a % 9 == 5)
                animal.GetComponent<MeshRenderer>().material = m5;
            else
                            if (a % 9 == 6)
                animal.GetComponent<MeshRenderer>().material = m6;
            else
                            if (a % 9 == 7)
                animal.GetComponent<MeshRenderer>().material = m7;
            else
                            if (a % 9 == 8)
                animal.GetComponent<MeshRenderer>().material = m8;


        }
    }


    void MakeItemsold()
    {


        for (int i = 0; i < AllItems.Count; i++)
        {

            GameObject ItemObj = Instantiate(RefItem);
            ItemObj.name = AllItems[i] + "_Obj";
            ItemObj.transform.GetChild(0).GetComponent<TextMesh>().text = AllItems[i];
            StoryItem _storiItem = ItemObj.GetComponent<StoryItem>();

            GameEnums.StoryObjects ItemType;
            if (Enum.TryParse(AllItems[i], true, out ItemType))
            {
                if (Enum.IsDefined(typeof(GameEnums.StoryObjects), ItemType) | ItemType.ToString().Contains(","))
                {
                    Debug.LogFormat("Converted '{0}' to {1}.", AllItems[i], ItemType.ToString());

                }
                else
                {
                    Debug.LogFormat("{0} is not an underlying value of the StoryObjects enumeration.", AllItems[i]);
                }
            }
            else
            {
                Debug.LogFormat("{0} is not a member of the StoryObjects enumeration.", AllItems[i]);
            }
            // _HeldObject = (GameEnums.StoryObjects)Enum.TryParse(typeof(GameEnums.StoryObjects), argObjName, true); //true ->  case insensitive

            _storiItem.MyType = ItemType;



            if (i % 8 == 0)
                ItemObj.GetComponent<MeshRenderer>().material = m0;
            else
                            if (i % 9 == 0)
                ItemObj.GetComponent<MeshRenderer>().material = m0;
            else
                            if (i % 9 == 1)
                ItemObj.GetComponent<MeshRenderer>().material = m1;
            else
                            if (i % 9 == 2)
                ItemObj.GetComponent<MeshRenderer>().material = m2;
            else
                            if (i % 9 == 3)
                ItemObj.GetComponent<MeshRenderer>().material = m3;
            else
                            if (i % 9 == 4)
                ItemObj.GetComponent<MeshRenderer>().material = m4;
            else
                            if (i % 9 == 5)
                ItemObj.GetComponent<MeshRenderer>().material = m5;
            else
                            if (i % 9 == 6)
                ItemObj.GetComponent<MeshRenderer>().material = m6;
            else
                            if (i % 9 == 7)
                ItemObj.GetComponent<MeshRenderer>().material = m7;
            else
                            if (i % 9 == 8)
                ItemObj.GetComponent<MeshRenderer>().material = m8;


        }
    }



    void MakeItems()
    {


        for (int i = 0; i < AllItems.Count; i++)
        {

            GameObject ItemObj = Instantiate(RefItem);
            ItemObj.name = AllItems[i] + "_Obj";
            ItemObj.transform.GetChild(0).GetComponent<TextMesh>().text = AllItems[i];
            StoryItem _storiItem = ItemObj.GetComponent<StoryItem>();

            // _HeldObject = (GameEnums.StoryObjects)Enum.TryParse(typeof(GameEnums.StoryObjects), argObjName, true); //true ->  case insensitive




        }
    }
    // Start is called before the first frame update
    void Start()
    {




        ///

        /// THIS WILL BUILD PREFABS ! JUST QUICK HACK < 

        ///
        // LoadedDeliveryItemObjs = Resources.LoadAll<GameObject>("Items/NiceConstructedObjects").ToList();
        //foreach (GameObject go in LoadedDeliveryItemObjs)
        //{
        //    GameObject NewGO = new GameObject();
        //    NewGO.transform.position = new Vector3(0, 0, 0);
        //    NewGO.name = go.name + "_root";

        //    GameObject NewGOChild = Instantiate(go);
        //    NewGOChild.name = go.name;
        //    NewGO.AddComponent<DeliveryItem>();
        //    DeliveryItem di = NewGO.GetComponent<DeliveryItem>();
        //    di.SetItemName(go.name);
        //    NewGOChild.transform.parent = NewGO.transform;
        //    print(go.name);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
