using System.Collections.Generic;
using UnityEngine;

public class NavMazeBlock : MonoBehaviour
{

    public TMPro.TextMeshPro m_Text;

    public GameObject MyTile;
    Renderer MyTileRenderer;

    public void UpdateBlockText(string argTxt) { m_Text.text = argTxt; }

    public void UpdateTileMAterial(Material argMat) { MyTile.GetComponent<Renderer>().material = argMat; }

    public void UnparentMytile() { MyTile.transform.parent = null; }

    public Vector3 cellPosition;

    public List<Transform> Adjacent_list_Transforms;

    private int _mystupidx;
    public int MyStupid__X
    {
        get { return _mystupidx; }
        set { _mystupidx = value; }
    }

    private int _mystupidz;
    public int MyStupid__Z
    {
        get { return _mystupidz; }
        set { _mystupidz = value; }
    }

    //public bool OnOff =true;
    public bool IsLegitPath = true;
    public bool isUP = false;
    public bool isDown = true;
    public bool isfound = false;
    public bool hasTower = false;
    public bool isStart = false;
    public bool isEnd = false;




    private Color _mycolor = Color.blue;
    public Color mycolor
    {
        get { return _mycolor; }
        set { _mycolor = value; }
    }






    private int _squareID = 0;
    public int SquareID
    {
        get { return _squareID; }
        set { _squareID = value; }
    }

    private int _randomeValue = 0;
    public int RandomValue
    {
        get { return _randomeValue; }
        set { _randomeValue = value; }
    }

    private int _value = 0;
    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }

    private int _d = 13371337;
    public int Dee
    {
        get { return _d; }
        set { _d = value; }
    }

    public Transform ptrT_Parent = null;

    public Transform ptrT_ActualParent = null;
    public Transform ptrT_Parent_PATH = null;

    public float _H;
    public float _G;
    public float _g;
    public float _F;
    public float _Fnew;
}
