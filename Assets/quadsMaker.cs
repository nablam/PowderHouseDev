using UnityEngine;

public class quadsMaker : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Quad;
    public Material mat1;
    public Material mat2;

    int cnt = 0;
    bool ismat1;
    int chidrencnt;
    void Start()
    {


        for (int x = 0; x < this.transform.childCount; x++)
        {

            //string[] arra = this.transform.GetChild(x).gameObject.name.Split('_');
            //print((arra.Length));
            //if (arra.Length < 2) print(" less than 3" + this.transform.GetChild(x).gameObject.name);

            GameObject OBJ = new GameObject();
            GameObject Copy = Instantiate(this.transform.GetChild(x).gameObject);
            Copy.name = this.transform.GetChild(x).gameObject.name.Split('_')[1];
            OBJ.name = this.transform.GetChild(x).gameObject.name.Split('_')[2] + "_" + this.transform.GetChild(x).gameObject.name.Split('_')[1];
            Copy.transform.parent = OBJ.transform;



        }

    }

    void MAkeQuadGrid()
    {

        for (int y = 0; y < 6; y++)
        {
            ismat1 = !ismat1;
            for (int x = 0; x < 6; x++)
            {
                cnt++;
                ismat1 = !ismat1;
                GameObject myq = Instantiate(Quad);
                TextMesh mytm = myq.GetComponentInChildren<TextMesh>();
                Renderer ren = myq.GetComponent<Renderer>();
                mytm.text = cnt.ToString();

                myq.transform.position = new Vector3(x - 2.5f, 0, 2.5f - y);
                myq.transform.parent = this.transform;
                if (ismat1)
                {
                    ren.material = mat1;
                }
                else
                {
                    ren.material = mat2;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
