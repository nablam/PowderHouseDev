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
    [Tooltip("MYST CONTAIN at least 2Mustplace objs")]

    public List<GameObject> Kitchen1x1 = new List<GameObject>();


    public List<GameObject> Bedroom2x1Action = new List<GameObject>();
    public List<GameObject> Bedroom2x1 = new List<GameObject>();
    [Tooltip("MYST CONTAIN at least 2Mustplace objs")]
    public List<GameObject> Bedroom1x1 = new List<GameObject>();


    public List<GameObject> Livingroom2x1Action = new List<GameObject>();
    public List<GameObject> Livingroom2x1 = new List<GameObject>();
    [Tooltip("MYST CONTAIN at least 2Mustplace objs")]

    public List<GameObject> Livingroom1x1 = new List<GameObject>();


    public List<GameObject> Lab2x1Action = new List<GameObject>();
    public List<GameObject> Lab2x1 = new List<GameObject>();
    [Tooltip("MYST CONTAIN at least 2Mustplace objs")]
    public List<GameObject> Lab1x1 = new List<GameObject>();



    //-------------------------------------------
    GameObject ActionObj;
    InteractionCentral AoIC;
    public InteractionCentral GetMainAction() { return this.AoIC; }

    GameObject DanceActionObj;
    InteractionCentral DanceAoIC;
    public InteractionCentral GetDanceAction() { return this.DanceAoIC; }
    GameObject GreetingsActionObj;
    InteractionCentral GreetingsAoIC;

    public InteractionCentral GetGreetingsAction() { return this.GreetingsAoIC; }

    const int Width = 6;
    const int Height = 4;

    FurnitureFurnisher _ff;
    // Start is called before the first frame update

    GameEnums.RoomType _roomtypeToBuild;
    void Awake()
    {
        _ff = GetComponent<FurnitureFurnisher>();
        for (int y = 0; y < Height; y++)
        {

            for (int x = 0; x < Width; x++)
            {
                blueprint[x, y] = false;
            }
        }


        // BuildKichen();
        // Build_BEdroom();
        // Build_Livingroom();

    }

    public void Build_rand_RoomType()
    {
        //SetTransDoorStepAsInteraction = GreetingsActionObj.transform;
        BuildRoomType((GameEnums.RoomType)Random.Range(0, 4));
        //  BuildRoomType(GameEnums.RoomType.Bedroom);
        //  BuildRoomType(GameEnums.RoomType.Livingroom);

    }

    public void BuildRoomType(GameEnums.RoomType argTypr)
    {

        PlaceDance_All();
        PlaceGreetings_All(2);

        if (argTypr == GameEnums.RoomType.Kitchen) { BuildKichen(); }
        else
                    if (argTypr == GameEnums.RoomType.Bedroom) { Build_BEdroom(); }
        else
                    if (argTypr == GameEnums.RoomType.Livingroom) { Build_Livingroom(); }
        else
                    if (argTypr == GameEnums.RoomType.Lab) { Build_Lab(); }

    }

    void BuildKichen()
    {
        _roomtypeToBuild = GameEnums.RoomType.Kitchen;
        PlaceActionObj_andFillRow(Random.Range(0, Width), Height - 1, Kitchen2x1Action, Kitchen1x1, Kitchen2x1);
    }


    void Build_BEdroom()
    {
        _roomtypeToBuild = GameEnums.RoomType.Bedroom;
        PlaceActionObj_andFillRow(Random.Range(0, Width), Height - 1, Bedroom2x1Action, Bedroom1x1, Bedroom2x1);
    }

    void Build_Livingroom()
    {
        _roomtypeToBuild = GameEnums.RoomType.Livingroom;
        //allow couchspace
        PlaceActionObj_andFillRow(Random.Range(3, Width), Height - 1, Livingroom2x1Action, Livingroom1x1, Livingroom2x1);
    }

    void Build_Lab()
    {
        _roomtypeToBuild = GameEnums.RoomType.Lab;
        PlaceActionObj_andFillRow(Random.Range(0, Width), Height - 1, Lab2x1Action, Lab1x1, Lab2x1);
    }


    public bool[,] blueprint = new bool[Width, Height];



    public GameObject Base00;
    void PlaceActionObj_andFillRow(int x, int y, List<GameObject> AcionObjs, List<GameObject> OtherObjsOfSameType_X1, List<GameObject> OtherObjsOfSameType_X2)
    {
        //insure a 2x1 not placed out of bound
        if (x > Width - 2)
            x = Width - 2;

        if (y >= Height)
            y = Height - 1;

        blueprint[x, y] = true;
        blueprint[x + 1, y] = true;

        ActionObj = Instantiate(AcionObjs[Random.Range(0, AcionObjs.Count)]);
        AoIC = ActionObj.GetComponentInParent<InteractionCentral>();
        ActionObj.transform.parent = Base00.transform;
        ActionObj.transform.localPosition = new Vector3(x, 0, y);


        FillLeftRight(x, OtherObjsOfSameType_X1, OtherObjsOfSameType_X2);
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

        int y = 1;

        blueprint[x, y] = true; //reserves a path quad to access the dang spot

        GreetingsActionObj = Instantiate(Greatings);
        GreetingsAoIC = GreetingsActionObj.GetComponentInParent<InteractionCentral>();
        GreetingsActionObj.transform.parent = Base00.transform;
        GreetingsActionObj.transform.localPosition = new Vector3(x + 0.4f, 0, y - 0.85f);
    }

    void PlaceDance_All()
    {
        int x = 3;
        int y = 1;

        blueprint[x, y] = true; //reserves a path quad to access the dang spot

        DanceActionObj = Instantiate(StarDance);
        DanceAoIC = DanceActionObj.GetComponentInParent<InteractionCentral>();
        DanceActionObj.transform.parent = Base00.transform;
        DanceActionObj.transform.localPosition = new Vector3(x - 0.25f, 0, y + 0.1f);
    }

    void FillLeftRight(int XposOf2x1Action, List<GameObject> OtherObjsOfSameType_size1, List<GameObject> OtherObjsOfSameType_size2)
    {

        ///  print(XposOf2x1Action);
        BuildBase0(XposOf2x1Action, "le", 0, OtherObjsOfSameType_size1, OtherObjsOfSameType_size2);

        BuildBase0(Width - (2 + XposOf2x1Action), "ri", (2 + XposOf2x1Action), OtherObjsOfSameType_size1, OtherObjsOfSameType_size2);
        //if (XposOf2x1Action == 0) { BuildBase0(Width - (2 + XposOf2x1Action), "right", (2 + XposOf2x1Action)); }
        //else
        //if (XposOf2x1Action == Width - 2) { BuildBase0(XposOf2x1Action, "left", 0); }
        //else
        //{
        //    BuildBase0(XposOf2x1Action, "le", 0);

        //    BuildBase0(Width - (2 + XposOf2x1Action), "ri", (2 + XposOf2x1Action));
        //}


    }

    void BuildBase0(int upTo_Not_Including, string log, int CurInstOffset, List<GameObject> OtherObjsFiller_1, List<GameObject> OtherObjsFiller_2)
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
                //  Debug.Log("break"); break;

            }

            //Debug.Log(curSizeTouse + log);
            Instantiate_a_1or2(curSizeTouse, CumBlocksLen + CurInstOffset, OtherObjsFiller_1, OtherObjsFiller_2);

            //InstantiateBlockSizeForBackWall(curSizeTouse, CumBlocksLen, argX, Height);
            // argLeftest_X += curSizeTouse;
            CumBlocksLen += curSizeTouse;
            //   Debug.Log(CumBlocksLen + " cum");

            // if (CumBlocksLen >= upTo_Not_Including) { Debug.Log("break"); break; }

        }
    }

    bool fridgeplaced = false;
    bool StovePlaced = false;
    void Instantiate_a_1or2(int ursizeTouse, int Offset, List<GameObject> OtherObjsFiller_x1, List<GameObject> OtherObjsFiller_x2)
    {
        GameObject FurnitureMesh = null;
        if (ursizeTouse == 1)
        {



            if (_roomtypeToBuild == GameEnums.RoomType.Bedroom)
            {
                if (!fridgeplaced && Offset == 0)
                {
                    FurnitureMesh = Instantiate(OtherObjsFiller_x1[0]);
                    fridgeplaced = true;
                }
                else if (!StovePlaced && Offset == Width - 1)
                {
                    FurnitureMesh = Instantiate(OtherObjsFiller_x1[1]);
                    StovePlaced = true;
                }
                else
                {

                    FurnitureMesh = Instantiate(OtherObjsFiller_x1[Random.Range(2, OtherObjsFiller_x1.Count)]); //MYST CONTAIN at least 2Mustlace objs
                }


            }
            else if (_roomtypeToBuild == GameEnums.RoomType.Kitchen)
            {
                if (!fridgeplaced && !StovePlaced)
                {
                    FurnitureMesh = Instantiate(OtherObjsFiller_x1[0]);
                    fridgeplaced = true;
                }
                else if (!StovePlaced)
                {
                    FurnitureMesh = Instantiate(OtherObjsFiller_x1[1]);
                    StovePlaced = true;
                }
                else
                {

                    FurnitureMesh = Instantiate(OtherObjsFiller_x1[Random.Range(2, OtherObjsFiller_x1.Count)]); //MYST CONTAIN at least 2Mustlace objs
                }

            }

            else if (_roomtypeToBuild == GameEnums.RoomType.Livingroom)
            {
                if (!fridgeplaced && !StovePlaced)
                {
                    FurnitureMesh = Instantiate(OtherObjsFiller_x1[0]);
                    fridgeplaced = true;
                }
                else if (!StovePlaced)
                {
                    FurnitureMesh = Instantiate(OtherObjsFiller_x1[1]);
                    StovePlaced = true;
                }
                else
                {

                    FurnitureMesh = Instantiate(OtherObjsFiller_x1[Random.Range(2, OtherObjsFiller_x1.Count)]); //MYST CONTAIN at least 2Mustlace objs
                }

            }

            else//lab
            {
                if (!fridgeplaced && !StovePlaced)
                {
                    FurnitureMesh = Instantiate(OtherObjsFiller_x1[0]);
                    fridgeplaced = true;
                }
                else if (!StovePlaced)
                {
                    FurnitureMesh = Instantiate(OtherObjsFiller_x1[1]);
                    StovePlaced = true;
                }
                else
                {

                    FurnitureMesh = Instantiate(OtherObjsFiller_x1[Random.Range(2, OtherObjsFiller_x1.Count)]); //MYST CONTAIN at least 2Mustlace objs
                }

            }



            FurnitureMesh.transform.parent = Base00.transform;
            FurnitureMesh.transform.localPosition = new Vector3(Offset, 0, Height - 1);
        }
        else
        if (ursizeTouse == 2)
        {
            FurnitureMesh = Instantiate(OtherObjsFiller_x2[Random.Range(0, OtherObjsFiller_x2.Count)]);
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

    public void PlaceCeilingLightHere(Transform argHere)
    {
        GameObject RandLight = Instantiate(_ff.RandCEilingLight());
        RandLight.transform.position = argHere.position;
        RandLight.transform.parent = argHere.transform;
    }
    public void PlaceRug(Transform argHere)
    {
        GameObject RandRug = Instantiate(_ff.RandRug());
        RandRug.transform.position = argHere.position;
        RandRug.transform.parent = argHere.transform;
    }

}

