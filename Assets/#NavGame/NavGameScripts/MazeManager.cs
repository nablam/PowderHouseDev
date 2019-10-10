using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    #region templatePOPULATE
    void populate_template()
    {

        for (int x = 0; x < Matrix_Size.x; x++)
        {
            for (int z = 0; z < Matrix_Size.z; z++)
            {
                Transform T_Cell_in_grid;
                T_Cell_in_grid = GridArra[x, z];
                NavMazeBlock cScript = T_Cell_in_grid.GetComponent<NavMazeBlock>();
                //cScript._F = cScript._H+ cScript._G ;
                T_Cell_in_grid.name = "(" + cScript.MyStupid__X + "," + cScript.MyStupid__Z + ")" + "g." + cScript._G + "h." + cScript._H + "f." + cScript._F;
            }
        }
    }
    #endregion


    public GameObject theHome;
    public Transform WallBlocksGroup;
    public Transform Cell_Prefab_Square;
    public Vector3 Matrix_Size;
    public Transform[,] GridArra;
    //private int _startNodeX;
    //private int _startNodeZ;
    //private int _endNodeX;
    //private int _endNodeZ;

    public Material RedMAt;
    public Material GreenMatLight;
    public Material GreenMatDark;
    public Material[] LightTiles = new Material[6];
    public Material[] DarkTiles = new Material[6];

    public Vector3 StartNodeV3;
    public Vector3 EndNodeV3;

    public Transform Grid_start_ptr = null;
    public Transform Grid_end_ptr = null;
    public Transform Grid_curr_ptr = null;

    public List<Transform> OpenSet;//= new List<Transform>();
    public List<Transform> ClosedSet;//= new List<Transform>();
    public bool showblank = false;

    //void placeHome()
    //{
    //    Vector3 realHomeV3 = new Vector3(StartNodeV3.x - 1, StartNodeV3.y + 1, StartNodeV3.z);

    //    GameObject Myhome = Instantiate(theHome,
    //                                    realHomeV3,
    //                                    Quaternion.identity
    //                                    ) as GameObject;
    //    //

    //    Myhome.transform.parent = this.WallBlocksGroup.transform;
    //}
    public void DO_INTI_SETUP_GRID()
    {
        RESETALL_inGrid();
        Set_start_End();
        CreateGrid();

        // placeHome();
        Aalgo();
    }

    #region INIT
    public void RESETALL_inGrid()
    {



        //OpenSet= new List<Transform>();
        //ClosedSet= new List<Transform>();

        OpenSet.Clear();
        ClosedSet.Clear();

        Grid_start_ptr = null;
        Grid_end_ptr = null;
        Grid_curr_ptr = Grid_start_ptr;

        StartNodeV3 = new Vector3();
        EndNodeV3 = new Vector3();
        //KILLtheNODES
        //	GameObject.Destroy(StartNode.gameObject);
        int childs = WallBlocksGroup.childCount;
        for (int i = childs - 1; i > 0 - 1; i--)
        {
            GameObject.Destroy(WallBlocksGroup.GetChild(i).gameObject);
        }
        //Destroy(this);

    }

    void CreateGrid()
    {
        //intialize a new grid[x,z]
        GridArra = new Transform[(int)Matrix_Size.x, (int)Matrix_Size.z];
        //build the grid
        int Script_ID = 0;
        for (int x = 0; x < Matrix_Size.x; x++)
        {
            for (int z = 0; z < Matrix_Size.z; z++)
            {
                Transform newcell;


                newcell = Instantiate(Cell_Prefab_Square, new Vector3(x * 2, -2.1f, z * 2), Quaternion.identity) as Transform;
                newcell.name = string.Format("({0},0,{1})", x, z);
                //	newcell.name= "("+x+",0,"+z+")" ;
                newcell.parent = this.WallBlocksGroup.transform; //making the newcell parent to the emtygameogject reffered as "transform" or this.transform

                newcell.GetComponent<NavMazeBlock>().cellPosition = new Vector3(x, 0, z);
                int rando = Random.Range(1, 50);
                newcell.GetComponent<NavMazeBlock>().RandomValue = rando;
                //********************************************************************************
                //*******VALUE SET rando or just all 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1
                newcell.GetComponent<NavMazeBlock>().Value = Random.Range(100, 300); // 2;
                //************************************************************************************

                newcell.GetComponent<NavMazeBlock>().SquareID = Script_ID;
                Script_ID++;

                if (showblank)
                    newcell.GetComponent<NavMazeBlock>().UpdateBlockText("");
                else
                {
                    string debugstr = "id " + Script_ID.ToString() + " \n val=" + newcell.GetComponent<NavMazeBlock>().Value;
                    newcell.GetComponent<NavMazeBlock>().UpdateBlockText(debugstr);
                }

                newcell.GetComponent<NavMazeBlock>().MyStupid__X = x;
                newcell.GetComponent<NavMazeBlock>().MyStupid__Z = z;

                newcell.GetComponent<NavMazeBlock>().IsLegitPath = true;

                GridArra[x, z] = newcell;


                //newcell.GetComponent<NavMazeBlock>().UnparentMytile();
                //keepfr color
                //newcell.GetComponent<Renderer>().material.color= Color.cyan;
            }
        }


        //*********************************make ptr to start and end in grid
        Grid_start_ptr = GridArra[(int)StartNodeV3.x, (int)StartNodeV3.z];
        Grid_start_ptr.GetComponent<NavMazeBlock>().isStart = true;
        // GridArra[(int)StartNodeV3.x, (int)StartNodeV3.z].GetComponent<Renderer>().material.color = Color.yellow;

        Grid_end_ptr = GridArra[(int)EndNodeV3.x, (int)EndNodeV3.z];
        //GridArra[(int)EndNodeV3.x, (int)EndNodeV3.z].GetComponent<Renderer>().material.color = Color.black;
        Grid_end_ptr.GetComponent<NavMazeBlock>().isEnd = true;

        Vector3 midarraypos = GridArra[(int)(Matrix_Size.x / 2), (int)(Matrix_Size.z / 2)].position;
        float AsqrBsqr = (Matrix_Size.x * Matrix_Size.x) + (Matrix_Size.z * Matrix_Size.z);
        float howhigh = Mathf.Sqrt(AsqrBsqr);
        Vector3 campos = new Vector3(midarraypos.x - (midarraypos.x / 2), midarraypos.y + howhigh, midarraypos.z);

        WallBlocksGroup.position = new Vector3(-Matrix_Size.x, 0, -Matrix_Size.z);

        foreach (Transform trans in GridArra)
        {
            trans.GetComponent<NavMazeBlock>().UnparentMytile();
        }
        //  Camera.main.transform.position = GridArra[(int)(0f), (int)(Matrix_Size.z * 2)].position + Vector3.up * 10;
        //Camera.main.transform.position = GridArra[(int)(Matrix_Size.x / 2), (int)(Matrix_Size.z * 2)].position + Vector3.up * 10;
        //	Camera.mainCamera.orthographicSize= Mathf.Max(Matrix_Size.x, Matrix_Size.z);
    }//XGridCreate

    void Set_start_End()
    {
        //left door
        int maxLow = (int)Matrix_Size.z / 2;
        //right exit
        int maxZ = (int)Matrix_Size.z - 1;
        int selecteRandomStartZ1 = Random.Range(1, maxLow);
        int selecteRandomStartZ2 = Random.Range(maxLow, maxZ);
        //StartNode.x=0;
        //
        //		StartNode.z=0;
        StartNodeV3.x = 0;
        StartNodeV3.z = selecteRandomStartZ1;

        EndNodeV3.x = (int)Matrix_Size.x - 1;
        EndNodeV3.z = selecteRandomStartZ2;
    }

    #endregion


    void Update_node_colors()
    {


        for (int x = 0; x < Matrix_Size.x; x++)
        {
            for (int z = 0; z < Matrix_Size.z; z++)
            {
                Transform T_Cell_in_grid;
                T_Cell_in_grid = GridArra[x, z];
                NavMazeBlock cScript = T_Cell_in_grid.GetComponent<NavMazeBlock>();
                //keepfor color
                //if (cScript.IsLegitPath) GridArra[x, z].GetComponent<Renderer>().material.color = Color.cyan;
                //if (cScript.isStart) GridArra[x, z].GetComponent<Renderer>().material.color = Color.white;
                //if (cScript.isEnd) GridArra[x, z].GetComponent<Renderer>().material.color = Color.black;
                //if (!cScript.IsLegitPath) GridArra[x, z].GetComponent<Renderer>().material.color = Color.yellow;

                if (cScript.isfound) { GridArra[x, z].GetComponent<NavMazeBlock>().UpdateTileMAterial(LightTiles[1]); }
                else { GridArra[x, z].GetComponent<NavMazeBlock>().UpdateTileMAterial(DarkTiles[1]); }
                if (cScript.isStart) { GridArra[x, z].GetComponent<NavMazeBlock>().UpdateTileMAterial(GreenMatDark); }
                if (cScript.isEnd) { GridArra[x, z].GetComponent<NavMazeBlock>().UpdateTileMAterial(RedMAt); }
                // if (!cScript.isfound) { GridArra[x, z].GetComponent<NavMazeBlock>().UpdateBlockText("X"); }


                //if (Grid_end_ptr.GetComponent<NavMazeBlock>().isfound)
                //{
                //    Grid_end_ptr.GetComponent<NavMazeBlock>().UpdateTileMAterial(LightTiles[1]);

                //}
                //else
                //{
                //    Grid_end_ptr.GetComponent<NavMazeBlock>().UpdateTileMAterial(DarkTiles[1]);

                //}

                //if(cScript.isStart)GridArra[x,z].GetComponent<Renderer>().material.color=Color.white;
                //if(cScript.isStart)GridArra[x,z].GetComponent<Renderer>().material.color=Color.white;
            }
        }






    }


    #region AlgoHelper
    void populate_H()
    {
        for (int x = 0; x < Matrix_Size.x; x++)
        {
            for (int z = 0; z < Matrix_Size.z; z++)
            {
                Transform T_Cell_in_grid;
                T_Cell_in_grid = GridArra[x, z];
                NavMazeBlock cScript = T_Cell_in_grid.GetComponent<NavMazeBlock>();
                cScript._H = (Mathf.Abs(cScript.MyStupid__X - (int)EndNodeV3.x)) + (Mathf.Abs(cScript.MyStupid__Z - (int)EndNodeV3.z));

                //	cScript._H= myx-_endNodeX + myy- endy
            }
        }
    }


    void makeAdjList_and_Actualparent_AFTER_WALLSELECTION()
    {
        for (int x = 0; x < Matrix_Size.x; x++)
        {
            for (int z = 0; z < Matrix_Size.z; z++)
            {
                Transform T_Cell_in_grid;
                T_Cell_in_grid = GridArra[x, z];
                if (T_Cell_in_grid.GetComponent<NavMazeBlock>().IsLegitPath)
                {

                    NavMazeBlock cScript = T_Cell_in_grid.GetComponent<NavMazeBlock>();
                    //CHECK BOT
                    if (x - 1 >= 0)
                    {
                        cScript.Adjacent_list_Transforms.Add(GridArra[x - 1, z]);
                        //GridArra[x-1,z].GetComponent<NavMazeBlock>().ptrT_Parent=T_Cell_in_grid;
                        GridArra[x - 1, z].GetComponent<NavMazeBlock>().ptrT_ActualParent = T_Cell_in_grid;
                    }
                    //CHECK TOP
                    if (x + 1 < Matrix_Size.x)
                    {
                        cScript.Adjacent_list_Transforms.Add(GridArra[x + 1, z]);
                        //GridArra[x+1,z].GetComponent<NavMazeBlock>().ptrT_Parent=T_Cell_in_grid;
                        GridArra[x + 1, z].GetComponent<NavMazeBlock>().ptrT_ActualParent = T_Cell_in_grid;
                    }
                    //CHECK BRIGHT
                    if (z - 1 >= 0)
                    {
                        cScript.Adjacent_list_Transforms.Add(GridArra[x, z - 1]);
                        //GridArra[x,z-1].GetComponent<NavMazeBlock>().ptrT_Parent=T_Cell_in_grid;
                        GridArra[x, z - 1].GetComponent<NavMazeBlock>().ptrT_ActualParent = T_Cell_in_grid;

                    }
                    //CHECK LEFT
                    if (z + 1 < Matrix_Size.z)
                    {
                        cScript.Adjacent_list_Transforms.Add(GridArra[x, z + 1]);
                        //GridArra[x,z+1].GetComponent<NavMazeBlock>().ptrT_Parent=T_Cell_in_grid;
                        GridArra[x, z + 1].GetComponent<NavMazeBlock>().ptrT_ActualParent = T_Cell_in_grid;
                    }
                }

            }//Xforz
        }//Xforx
    }//XSetAdj


    int sort_adj_ByLowest_value(Transform inputA, Transform inputB)
    {
        int a = (int)inputA.GetComponent<NavMazeBlock>()._F;
        int b = (int)inputB.GetComponent<NavMazeBlock>()._F;
        int what = a.CompareTo(b);
        return what;
    }//Xsort

    int sort_ListItem_byF(Transform inputA, Transform inputB)
    {
        int a = (int)inputA.GetComponent<NavMazeBlock>()._F;
        int b = (int)inputB.GetComponent<NavMazeBlock>()._F;
        int what = a.CompareTo(b);
        return what;
    }//Xsort


    public bool isThisInSet(Transform t, List<Transform> L)
    {
        foreach (Transform ti in L)
        {
            if (ti.GetComponent<NavMazeBlock>().SquareID == t.GetComponent<NavMazeBlock>().SquareID) return true;
        }
        return
            false;
    }

    public Transform Findnode_loweststF_inOpenset()
    {
        if (OpenSet.Count > 0)
        {
            OpenSet.Sort(sort_ListItem_byF);
            return OpenSet[0];
        }
        else
            return null;

    }



    public void MakePath()
    {
        do
        {
            Grid_curr_ptr.GetComponent<NavMazeBlock>().isfound = true;


            Vector3 x = new Vector3(Grid_curr_ptr.position.x, Grid_curr_ptr.position.y + 1, Grid_curr_ptr.position.z);

            // MWay_ptr.wp_V3_List.Add(x);
            //if(!showblank)
            //		Grid_curr_ptr.GetComponent<Renderer>().material.color = Color.blue;			
            Grid_curr_ptr = Grid_curr_ptr.GetComponent<NavMazeBlock>().ptrT_Parent_PATH;
        } while (Grid_curr_ptr.GetComponent<NavMazeBlock>().ptrT_Parent_PATH != null);

        Vector3 x1real = new Vector3(StartNodeV3.x, StartNodeV3.y + 1, StartNodeV3.z);


    }

    #endregion

    void clearListOfAdjascent()
    {


        for (int x = 0; x < Matrix_Size.x; x++)
        {
            for (int z = 0; z < Matrix_Size.z; z++)
            {
                Transform T_Cell_in_grid;
                T_Cell_in_grid = GridArra[x, z];
                NavMazeBlock cScript = T_Cell_in_grid.GetComponent<NavMazeBlock>();
                if (cScript.ptrT_ActualParent != null)
                    cScript.ptrT_ActualParent = null;
                //cScript.ptrT_Parent_PATH=null;
                //cScript.ptrT_Parent=null;
                cScript.isfound = false;
                cScript.Adjacent_list_Transforms.Clear();
                //	T_Cell_in_grid.GetComponent<Renderer>().material.color=Color.cyan;
                //cScript._F = cScript._H+ cScript._G ;
                //T_Cell_in_grid.name= "("+ cScript.MyStupid__X  + "," + cScript.MyStupid__Z+ ")" + "g."+ cScript._G + "h."+cScript._H + "f." + cScript._F;
            }
        }
    }

    void resetALL_G_H_F()
    {

        for (int x = 0; x < Matrix_Size.x; x++)
        {
            for (int z = 0; z < Matrix_Size.z; z++)
            {
                Transform T_Cell_in_grid;
                T_Cell_in_grid = GridArra[x, z];
                NavMazeBlock cScript = T_Cell_in_grid.GetComponent<NavMazeBlock>();
                cScript._g = 0;
                cScript._H = 0;
                cScript._F = 0;
            }
        }

    }

    void resetPTRSandLists()
    {
        OpenSet.Clear();
        ClosedSet.Clear();

        Grid_curr_ptr = Grid_start_ptr;

    }

    public bool Aalgo()
    {

        Update_node_colors();
        resetPTRSandLists();
        populate_H();
        clearListOfAdjascent();
        //  MWay_ptr.Reset_Waypointtile_list();
        //   MWay_ptr.Reset_Waypointtile_OBJ_list();
        // randCell_deselect();
        makeAdjList_and_Actualparent_AFTER_WALLSELECTION();
        resetALL_G_H_F();

        Grid_curr_ptr = Grid_start_ptr;
        ClosedSet.Add(Grid_curr_ptr);

        do
        {
            foreach (Transform n in Grid_curr_ptr.GetComponent<NavMazeBlock>().Adjacent_list_Transforms)
            {
                float curnodeG = Grid_curr_ptr.GetComponent<NavMazeBlock>()._G;
                if (isThisInSet(n, ClosedSet))
                    continue;


                else if (isThisInSet(n, OpenSet))
                {
                    //computeG(n);


                    float adjG = n.GetComponent<NavMazeBlock>()._G;
                    float newG = curnodeG + adjG;

                    if (newG < curnodeG)
                    {
                        //n.GetComponent<NavMazeBlock>().GetComponent<Transform>().GetComponent<Renderer>().material.color=Color.red;
                        n.GetComponent<NavMazeBlock>().ptrT_Parent_PATH = Grid_curr_ptr;
                        n.GetComponent<NavMazeBlock>()._G = newG;
                        n.GetComponent<NavMazeBlock>()._F = n.GetComponent<NavMazeBlock>()._G + n.GetComponent<NavMazeBlock>()._H;
                    }//end
                }
                else
                {
                    n.GetComponent<NavMazeBlock>().ptrT_Parent_PATH = Grid_curr_ptr;
                    //	n.GetComponent<NavMazeBlock>().GetComponent<Transform>().GetComponent<Renderer>().material.color=Color.red;

                    //compute H
                    NavMazeBlock nscript = n.GetComponent<NavMazeBlock>();
                    nscript._H = (Mathf.Abs(nscript.MyStupid__X - (int)EndNodeV3.x)) + (Mathf.Abs(nscript.MyStupid__Z - (int)EndNodeV3.z));
                    //compute G
                    nscript._G = (Mathf.Abs((int)StartNodeV3.x - nscript.MyStupid__X)) + (Mathf.Abs((int)StartNodeV3.z - nscript.MyStupid__Z));
                    n.GetComponent<NavMazeBlock>()._F = n.GetComponent<NavMazeBlock>()._G + n.GetComponent<NavMazeBlock>()._H;
                    OpenSet.Add(n);
                }
            }//loopdone

            //if (isThisInSet(ClosedSet, new));
            if (OpenSet.Count < 1) break;

            Grid_curr_ptr = Findnode_loweststF_inOpenset();
            OpenSet.RemoveAt(0);
            ClosedSet.Add(Grid_curr_ptr);


        } while (Grid_curr_ptr.GetComponent<NavMazeBlock>().SquareID != Grid_end_ptr.GetComponent<NavMazeBlock>().SquareID);

        MakePath();



        if (Grid_end_ptr.GetComponent<NavMazeBlock>().isfound)
        {
            Grid_end_ptr.GetComponent<Transform>().GetComponent<Renderer>().material.color = Color.green;
            return true;
        }
        else
        {
            Grid_end_ptr.GetComponent<Transform>().GetComponent<Renderer>().material.color = Color.red;
            return false;
        }


    }




    void RaizeWalls()
    {

        for (int x = 0; x < Matrix_Size.x; x++)
        {
            for (int z = 0; z < Matrix_Size.z; z++)
            {

                if (!GridArra[x, z].GetComponent<NavMazeBlock>().IsLegitPath)
                    GridArra[x, z].transform.position = new Vector3(GridArra[x, z].transform.position.x, GridArra[x, z].transform.position.y + 1, GridArra[x, z].transform.position.z); ;
                //NavMazeBlock cScript= T_Cell_in_grid.GetComponent<NavMazeBlock>();
                //cScript._F = cScript._H+ cScript._G ;
                //T_Cell_in_grid.name= "("+ cScript.MyStupid__X  + "," + cScript.MyStupid__Z+ ")" + "g."+ cScript._G + "h."+cScript._H + "f." + cScript._F;
            }
        }
    }


    public void randCell_deselect()
    {
        int twothidsx = ((int)Matrix_Size.x * 1 / 7);
        int twothidsx_max = ((int)Matrix_Size.x * 6 / 7);
        //int newx= Matrix_Size.x - twothidsx;r
        int twothidsz = (int)Matrix_Size.z * 6 / 7;

        for (int x = 0; x < Matrix_Size.x; x++)
        {
            for (int z = 0; z < Matrix_Size.z; z++)
            {
                int randox = Random.Range(1, 5);

                if (randox == 1)
                {

                    if (
                        //x>4 ||
                        twothidsx < x && x < twothidsx_max
                        )


                    {

                        //GridArra[x, z].GetComponent<Renderer>().material.color = Color.gray;
                        // GridArra[x, z].GetComponent<NavMazeBlock_Clicking>().MoveUpOne();
                        GridArra[x, z].GetComponent<NavMazeBlock>().IsLegitPath = false;
                    }

                }
            }


        }

    }


    //ref to the other object of waypoints
    //WayPointManager wm;


    public Transform SELECTEDcell;
    void OnEnable()
    {
        // NavMazeBlock_Clicking.Onclicked += SetSelectedNode;

    }
    void OnDisable()
    {
        // NavMazeBlock_Clicking.Onclicked -= SetSelectedNode;
    }

    public void SetSelectedNode(Transform t)
    {
        SELECTEDcell = t;


    }


    // MNGR_Waypoints MWay_ptr;

    // Use this for initialization
    void Start()
    {
        //ref to the other object of waypoints
        //= GameObject.Find("Waypoints_object_keepName").GetComponent<WayPointManager>();
        // MWay_ptr = GameObject.Find("Empty_Waypoints").GetComponent<MNGR_Waypoints>();
        DO_INTI_SETUP_GRID();

        node = Grid_end_ptr.GetComponent<NavMazeBlock>();


    }
    NavMazeBlock node;
    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{

        //    if (node.ptrT_ActualParent != null)
        //    {

        //        node = node.ptrT_ActualParent.GetComponent<NavMazeBlock>();
        //        node.UpdateTileMAterial(DarkTiles[0]);
        //    }

        //}

    }
}
