using UnityEngine;

public class BackRowFurnitureBuilder : MonoBehaviour
{


    public GameObject C1;
    public GameObject C2;
    public GameObject C3;

    const int ConstWidth = 5;
    const int constXposTopLeft = -3;
    const int ConstZposTopLeft = 3;
    // Start is called before the first frame update

    enum Oriantation
    {
        Right,
        Down,
        Left,
        Up
    }

    Vector3 OrientRight = new Vector3(0, 0, 0);
    Vector3 OrientDown = new Vector3(0, 90, 0);
    Vector3 OrientLeft = new Vector3(0, 180, 0);
    Vector3 Orientup = new Vector3(0, -90, 0);
    void Start()
    {
        BuildRow(0, 0);



    }

    void BuildRow(int argX, int argY)
    {
        argY += ConstZposTopLeft;
        argX += constXposTopLeft;
        int CumBlocksLen = 0;
        int leftover = 0;

        int Maxpossible = 2;
        int curSizeTouse = 1;

        for (int x = 0; x < ConstWidth; x++)
        {
            leftover = ConstWidth - CumBlocksLen;
            if (CumBlocksLen >= ConstWidth + 1) { Debug.Log("break"); break; }

            if (leftover >= 2)
            {
                Maxpossible = 2 + 1;

                curSizeTouse = Random.Range(1, Maxpossible);
            }
            else
                if (leftover < 2)
            {
                Maxpossible = 1 + 1;

                curSizeTouse = Random.Range(1, Maxpossible);
            }

            Debug.Log(curSizeTouse);

            InstantiateBlockSizeForBackWall(curSizeTouse, CumBlocksLen, argX, argY, Oriantation.Right);
            // argLeftest_X += curSizeTouse;
            CumBlocksLen += curSizeTouse;

        }
    }

    void BuildRowInsure(int Arg12)
    {

    }

    void InstantiateBlockSizeForBackWall(int s, int xCumBlock, int argxOffset, int argyOffset, Oriantation argOr)
    {
        Vector3 here = new Vector3(xCumBlock + argxOffset, 0, argyOffset);


        Quaternion QRotation = new Quaternion();
        switch (argOr)
        {

            case Oriantation.Right:
                QRotation = Quaternion.Euler(OrientRight);
                break;
            case Oriantation.Down:
                QRotation = Quaternion.Euler(OrientDown);
                break;
            case Oriantation.Left:
                QRotation = Quaternion.Euler(OrientLeft);
                break;
            case Oriantation.Up:
                QRotation = Quaternion.Euler(Orientup);
                break;
        }


        if (s == 1)
        {
            Instantiate(C1, here, QRotation);

        }
        else
            if (s == 2)
        {
            Instantiate(C2, here, QRotation);
        }
        else
            if (s == 3)
        {
            Instantiate(C3, here, QRotation);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
