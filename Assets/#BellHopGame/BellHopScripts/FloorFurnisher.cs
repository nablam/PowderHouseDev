using System.Collections.Generic;
using UnityEngine;

public class FloorFurnisher : MonoBehaviour
{
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

    const int Width = 6;
    const int Height = 6;
    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < Height; y++)
        {

            for (int x = 0; x < Width; x++)
            {
                blueprint[x, y] = false;
            }
        }
        PlaceActionObj_Kitchen(2, 3);
    }




    public bool[,] blueprint = new bool[Width, Height];

    void PlaceActionObj_Kitchen(int x, int y)
    {
        //insure a 2x1 not placed out of bound
        if (x > Width - 2)
            x = Width - 2;

        blueprint[x, y] = true;
        blueprint[x + 1, y] = true;
        GameObject ActionObj = Instantiate(Kitchen2x1Action[Random.Range(0, Kitchen2x1Action.Count)]);

    }
}
