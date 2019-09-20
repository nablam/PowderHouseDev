using UnityEngine;

public class AnimalDweller : MonoBehaviour
{
    public GameEnums.StoryObjects _HeldItem = GameEnums.StoryObjects.Ball;
    public GameEnums.AnimalCharcter My_type = GameEnums.AnimalCharcter.Alligator;
    StoryNode MyStoryNode = null;
    GameObject _CurHeldObject = null;
    Transform MyHandPos;
    GameObject TextBoxName;
    GameObject TextBoxObjNeeded;
    bool HasTossedObjectToBellHop = false;

    int TimesAsked = 0;

    int _floor = 0;

    public int Floor_Number { get => _floor; set => _floor = value; }

    //StoryPacket _StoryPacket;

    void Awake()
    {
        TextBoxName = transform.GetChild(0).gameObject;
        TextBoxObjNeeded = transform.GetChild(1).gameObject;
        MyHandPos = transform.GetChild(2);

    }
    ////forBuilding the resorce prefab only. keep while grayboxing
    //public void InitializeMe(GameEnums.AnimalCharcter argAnimal)
    //{

    //}
    //public void InitMyPacket(StoryPacket argPacket)
    //{
    //    _StoryPacket = argPacket;
    //}

    /// <summary>
    /// TODO:
    /// fix this later . dont assume argObj type , or handle errors
    /// </summary>
    /// <param name="argObj"></param>
    public void AssignHeldObject(GameObject argObj, StoryNode argStoryNode)
    {
        _HeldItem = argObj.GetComponent<StoryItem>().MyType;
        MyStoryNode = argStoryNode;
        if (_CurHeldObject == null)
        {
            _CurHeldObject = argObj;
            _CurHeldObject.transform.position = MyHandPos.position;
            _CurHeldObject.transform.parent = MyHandPos;
        }

    }
    public StoryNode GetStoryNode()
    {
        return this.MyStoryNode;
    }


    public void TossObjectToBellhop(Transform ArgBellhopHand)
    {
        if (HasTossedObjectToBellHop)
        {

            return;
        }
        if (_CurHeldObject != null)
        {

            _CurHeldObject.GetComponent<StoryItem>().MoveTO(MyHandPos, ArgBellhopHand, true);
            HasTossedObjectToBellHop = true;
        }
    }

    public void IncrementAskTimes()
    {
        TimesAsked++;
    }


    public int GEt_AskedTimes() { return this.TimesAsked; }

    // public string I_Need_() { return " I need the " + MyStoryNode.; }
}
