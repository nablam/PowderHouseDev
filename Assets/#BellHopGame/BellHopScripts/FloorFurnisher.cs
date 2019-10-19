using System.Collections.Generic;
using UnityEngine;

public class FloorFurnisher : MonoBehaviour
{
    public GameObject Greatings;
    public GameObject StarDance;
    public GameObject cornershelf_L;
    public GameObject cornershelf_R;
    public List<GameObject> StandaloneActions2x1 = new List<GameObject>();
    public List<GameObject> StandaloneActions1x1 = new List<GameObject>();

    public List<GameObject> Kitchen2x1Action = new List<GameObject>();
    public List<GameObject> Kitchen2x1 = new List<GameObject>();
    public List<GameObject> Kitchen1x1 = new List<GameObject>();


    public List<GameObject> Bedroom2x1Action = new List<GameObject>();
    public List<GameObject> Bedroom2x1 = new List<GameObject>();
    public List<GameObject> Bedroom1x1 = new List<GameObject>();


    public List<GameObject> Livingroom2x1Action = new List<GameObject>();
    public List<GameObject> Livingroom2x1 = new List<GameObject>();
    public List<GameObject> Livingroom1x1 = new List<GameObject>();


    public List<GameObject> Lab2x1Action = new List<GameObject>();
    public List<GameObject> Lab2x1 = new List<GameObject>();
    public List<GameObject> Lab1x1 = new List<GameObject>();



    //-------------------------------------------
    GameObject ActionObj;
    InteractionCentral AoIC;

    GameObject DanceActionObj;
    InteractionCentral DanceAoIC;

    GameObject GreetingsActionObj;
    InteractionCentral GreetingsAoIC;

    const int Width = 6;
    const int Height = 6;

    FurnitureFurnisher _ff;
    // Start is called before the first frame update
    void Start()
    {
        _ff = GetComponent<FurnitureFurnisher>();
        for (int y = 0; y < Height; y++)
        {

            for (int x = 0; x < Width; x++)
            {
                blueprint[x, y] = false;
            }
        }

        PlaceDance_All();
        PlaceGreetings_All(2);
        PlaceActionObj_Kitchen(Random.Range(0, Width), 5);
        // PlaceActionObj_Kitchen(5, 5);
    }




    public bool[,] blueprint = new bool[Width, Height];



    public GameObject Base00;
    void PlaceActionObj_Kitchen(int x, int y)
    {
        //insure a 2x1 not placed out of bound
        if (x > Width - 2)
            x = Width - 2;

        if (y >= Height)
            y = Height - 1;

        blueprint[x, y] = true;
        blueprint[x + 1, y] = true;
        ActionObj = Instantiate(Kitchen2x1Action[Random.Range(0, Kitchen2x1Action.Count)]);
        AoIC = ActionObj.GetComponent<InteractionCentral>();
        ActionObj.transform.parent = Base00.transform;
        ActionObj.transform.localPosition = new Vector3(x, 0, y);


        FillLeftRight(x);
    }


    void Place_Action_2x1(int x, int y, List<GameObject> _2x1s)
    {
        //insure a 2x1 not placed out of bound
        if (x > Width - 2)
            x = Width - 2;

        if (y >= Height)
            y = Height - 1;

        blueprint[x, y] = true;
        blueprint[x + 1, y] = true;
        Instantiate(_2x1s[Random.Range(0, _2x1s.Count)]);
        ActionObj.transform.parent = Base00.transform;
        ActionObj.transform.localPosition = new Vector3(x, 0, y);
    }

    void PlaceGreetings_All(int x)
    {

        if (x > Width - 2)
            x = Width - 2;

        if (x < 2)
            x = 2;

        int y = 0;

        blueprint[x, y] = true; //reserves a path quad to access the dang spot

        GreetingsActionObj = Instantiate(Greatings);
        GreetingsAoIC = GreetingsActionObj.GetComponent<InteractionCentral>();
        GreetingsActionObj.transform.parent = Base00.transform;
        GreetingsActionObj.transform.localPosition = new Vector3(x, 0, y - 1);
    }

    void PlaceDance_All()
    {
        int x = 3;
        int y = 1;

        blueprint[x, y] = true; //reserves a path quad to access the dang spot

        DanceActionObj = Instantiate(StarDance);
        DanceAoIC = DanceActionObj.GetComponent<InteractionCentral>();
        DanceActionObj.transform.parent = Base00.transform;
        DanceActionObj.transform.localPosition = new Vector3(x, 0, y);
    }

    void FillLeftRight(int XposOf2x1Action)
    {

        print(XposOf2x1Action);
        BuildBase0(XposOf2x1Action, "le", 0);

        BuildBase0(Width - (2 + XposOf2x1Action), "ri", (2 + XposOf2x1Action));
        //if (XposOf2x1Action == 0) { BuildBase0(Width - (2 + XposOf2x1Action), "right", (2 + XposOf2x1Action)); }
        //else
        //if (XposOf2x1Action == Width - 2) { BuildBase0(XposOf2x1Action, "left", 0); }
        //else
        //{
        //    BuildBase0(XposOf2x1Action, "le", 0);

        //    BuildBase0(Width - (2 + XposOf2x1Action), "ri", (2 + XposOf2x1Action));
        //}


    }

    void BuildBase0(int upTo_Not_Including, string log, int CurInstOffset)
    {
        int CumBlocksLen = 0;
        int leftover = 0;

        int Maxpossible = 2;
        int curSizeTouse = 1;

        for (int x = 0; x < upTo_Not_Including; x++)
        {
            leftover = upTo_Not_Including - CumBlocksLen;




            if (leftover >= 2)
            {
                Maxpossible = 2 + 1;

                curSizeTouse = Random.Range(1, Maxpossible);
            }
            else
                if (leftover < 2 && leftover > 0)
            {
                curSizeTouse = 1;
                // Maxpossible = 1 + 1;

                //                curSizeTouse = Random.Range(1, Maxpossible);
            }
            else
            {
                Debug.Log("break"); break;

            }

            Debug.Log(curSizeTouse + log);
            Instantiate_a_1or2(curSizeTouse, CumBlocksLen + CurInstOffset);

            //InstantiateBlockSizeForBackWall(curSizeTouse, CumBlocksLen, argX, Height);
            // argLeftest_X += curSizeTouse;
            CumBlocksLen += curSizeTouse;
            //   Debug.Log(CumBlocksLen + " cum");

            // if (CumBlocksLen >= upTo_Not_Including) { Debug.Log("break"); break; }

        }
    }

    bool fridgeplaced = false;
    bool StovePlaced = false;
    void Instantiate_a_1or2(int ursizeTouse, int Offset)
    {
        GameObject FurnitureMesh = null;
        if (ursizeTouse == 1)
        {

            if (!fridgeplaced && !StovePlaced)
            {
                FurnitureMesh = Instantiate(Kitchen1x1[0]);
                fridgeplaced = true;
            }
            else if (!StovePlaced)
            {
                FurnitureMesh = Instantiate(Kitchen1x1[1]);
                StovePlaced = true;
            }
            else
            {

                FurnitureMesh = Instantiate(Kitchen1x1[Random.Range(2, Kitchen1x1.Count)]);
            }
            FurnitureMesh.transform.parent = Base00.transform;
            FurnitureMesh.transform.localPosition = new Vector3(Offset, 0, Height - 1);
        }
        else
        if (ursizeTouse == 2)
        {
            FurnitureMesh = Instantiate(Kitchen2x1[Random.Range(0, Kitchen2x1.Count)]);
            FurnitureMesh.transform.parent = Base00.transform;
            FurnitureMesh.transform.localPosition = new Vector3(Offset, 0, Height - 1);
        }

        if (FurnitureMesh == null)
        {
            print("no furniture made");
        }
        else
        {
            _ff.FurnishThis(FurnitureMesh);
        }


    }



}

