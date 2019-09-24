using UnityEngine;

public class AnimalDweller : MonoBehaviour
{
    public GameEnums.StoryObjects _HeldItem = GameEnums.StoryObjects.Ball;
    public GameEnums.AnimalCharcter My_type = GameEnums.AnimalCharcter.Alligator;
    StoryNode MyStoryNode = null;
    GameObject _CurHeldObject = null;
    GameObject _CurReceivedObject = null;
    public Transform RightHandHoldPos;
    public Transform LeftHandHoldPos;
    GameObject TextBoxName;
    GameObject TextBoxObjNeeded;
    bool HasTossedObjectToBellHop = false;

    int TimesAsked = 0;

    int _floor = 0;

    public int Floor_Number { get => _floor; set => _floor = value; }

    //StoryPacket _StoryPacket;
    public void UpdateNameText(string argName) { transform.GetChild(0).GetComponent<TextMesh>().text = argName; }
    void Awake()
    {
        TextBoxName = transform.GetChild(0).gameObject;
        TextBoxObjNeeded = transform.GetChild(1).gameObject;


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
            _CurHeldObject.transform.position = RightHandHoldPos.position;
            _CurHeldObject.transform.parent = RightHandHoldPos;
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

            _CurHeldObject.GetComponent<StoryItem>().MoveTO(RightHandHoldPos, ArgBellhopHand, true);
            HasTossedObjectToBellHop = true;
        }
    }


    public void Set_ItemReachedDwellr(GameObject ItemObj)
    {

        _CurReceivedObject = ItemObj;
        _CurReceivedObject.transform.parent = LeftHandHoldPos.transform;


    }
    public void IncrementAskTimes()
    {
        TimesAsked++;
    }


    public int GEt_AskedTimes() { return this.TimesAsked; }

    // public string I_Need_() { return " I need the " + MyStoryNode.; }
}
