using UnityEngine;

public class AnimalDweller : MonoBehaviour, ICharacterAnim
{
    public GameEnums.AnimalCharcter My_type = GameEnums.AnimalCharcter.Alligator;
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

    int _name = 0;
    public int Name { get => _name; set => _name = value; }

    Animator _MyAnimator;

    //StoryPacket _StoryPacket;
    public void UpdateNameText(string argName) { transform.GetChild(0).GetComponent<TextMesh>().text = argName; }
    void Awake()
    {
        _MyAnimator = GetComponentInChildren<Animator>();
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
    public void AssignHeldObject(GameObject argObj)
    {
        if (_CurHeldObject == null)
        {
            _CurHeldObject = argObj;
            _CurHeldObject.transform.position = RightHandHoldPos.position;
            _CurHeldObject.transform.parent = RightHandHoldPos;
        }

    }
    public GameObject GetHeldObject()
    {
        return this._CurHeldObject;
    }

    public void TossObjectToBellhop()
    {
        if (HasTossedObjectToBellHop)
        {

            return;
        }
        if (_CurHeldObject != null)
        {
            AnimateToss();

        }
    }
    void ActialTossPeakRegisterStartMoveObject()
    {
        GameManager.Instance.AirBornObj = _CurHeldObject.transform;
        _CurHeldObject.GetComponent<StoryItem>().MoveTO(RightHandHoldPos, GameManager.Instance.TheBellHop.RightHandHoldPos, true);
        HasTossedObjectToBellHop = true;
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

    public void AnimateToss() { _MyAnimator.SetTrigger("TrigToss"); }
    public void AnimateCatch() { _MyAnimator.SetTrigger("TrigCatch"); }




    public void CatchPeack()
    {
        Set_ItemReachedDwellr(GameManager.Instance.AirBornObj.gameObject);
    }



    public void AnimTossPeack()
    {
        ActialTossPeakRegisterStartMoveObject();
    }

    public void AnimCatchPeack()
    {
        CatchPeack();
    }


}
