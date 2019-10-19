using System.Collections.Generic;
using UnityEngine;

public class FurnitureFurnisher : MonoBehaviour
{
    public int ChanceOfDeco;
    public List<GameObject> Surfs = new List<GameObject>();
    public List<GameObject> Toys = new List<GameObject>();
    public List<GameObject> Rest = new List<GameObject>();

    List<GameObject> All = new List<GameObject>();

    public GameObject Get_Rand()
    {

        return All[Random.Range(0, All.Count)];

    }

    // Start is called before the first frame update
    void Start()
    {
        All.AddRange(Surfs);
        All.AddRange(Rest);
        All.AddRange(Toys);

    }

    public void FurnishThis(GameObject Go)
    {
        List<Transform> DecoSpots = new List<Transform>();
        bool found_DecoBase = false;
        Transform DecobaseTran = null;
        for (int x = 0; x < Go.transform.childCount; x++)
        {

            if (Go.transform.GetChild(x).name.CompareTo("DecoBase") == 0)
            {
                print("found DecoBase in " + Go.name);
                found_DecoBase = true;
                DecobaseTran = Go.transform.GetChild(x);
                break;
            }
        }
        if (found_DecoBase)
        {
            for (int x = 0; x < DecobaseTran.childCount; x++)
            {

                int c = Random.Range(0, 100);

                if (c < ChanceOfDeco)
                {
                    GameObject deco = Instantiate(Get_Rand());
                    deco.transform.parent = DecobaseTran.GetChild(x);
                    deco.transform.localPosition = new Vector3(0, 0, 0);
                }


            }
        }
    }
}
