using System.Collections.Generic;
using UnityEngine;

public class SquareNode : MonoBehaviour
{
    public SquareNode() { }

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



    public bool OnOff = true;

    //private bool _onoff=false;
    //public bool OnOff{
    //	get {return _onoff;}
    //	set {_onoff= value;}
    //}


    private Color _mycolor = Color.blue;
    public Color mycolor
    {
        get { return _mycolor; }
        set { _mycolor = value; }
    }

    public bool isfound = false;

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

    /*
        public SquareNode ptr_right=null;
        public SquareNode ptr_left=null;
        public SquareNode ptr_top=null;
        public SquareNode ptr_bot=null;
        */
    public Transform ptrT_right = null;
    public Transform ptrT_left = null;
    public Transform ptrT_top = null;
    public Transform ptrT_bot = null;



    public Transform ptrT_Parent = null;


    public float _H;
    public float _G;
    public float _F;



}
