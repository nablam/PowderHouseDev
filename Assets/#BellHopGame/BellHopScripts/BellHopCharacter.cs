using UnityEngine;

public class BellHopCharacter : MonoBehaviour, ICharacterAnim
{
    public TextMesh m_NameObjectText;
    public TextMesh m_HeldObjectText;


    public GameEnums.StoryObjects _HeldObject;
    StoryItem ItemToToss;
    public Transform RightHandHoldPos;
    public Transform LeftHandHoldPos;
    public void UpdateNameText(string argName) { transform.GetChild(0).GetComponent<TextMesh>().text = argName; }
    public void UpdateHeldObjNameText(string argName) { transform.GetChild(1).GetComponent<TextMesh>().text = argName; }

    public Transform GetMyRightHandHold() { return this.RightHandHoldPos; }
    public Transform GetMyLeftHandHold() { return this.LeftHandHoldPos; }
    Animator _MyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _MyAnimator = GetComponent<Animator>();
        m_NameObjectText = this.transform.GetChild(0).GetComponent<TextMesh>();
        m_HeldObjectText.text = "";
        m_HeldObjectText = this.transform.GetChild(1).GetComponent<TextMesh>();
        // UpdateHeldObject("None");
        _HeldObject = GameEnums.StoryObjects.None;
    }

    //public void UpdateHeldObject(string argObjName)
    //{

    //    GameEnums.StoryObjects tempHeldObj;
    //    if (Enum.TryParse(argObjName, true, out tempHeldObj))
    //    {
    //        if (Enum.IsDefined(typeof(GameEnums.StoryObjects), tempHeldObj) | tempHeldObj.ToString().Contains(","))
    //        {
    //            Debug.LogFormat("Converted '{0}' to {1}.", argObjName, tempHeldObj.ToString());

    //        }
    //        else
    //        {
    //            Debug.LogFormat("{0} is not an underlying value of the StoryObjects enumeration.", argObjName);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogFormat("{0} is not a member of the StoryObjects enumeration.", argObjName);
    //    }
    //    // _HeldObject = (GameEnums.StoryObjects)Enum.TryParse(typeof(GameEnums.StoryObjects), argObjName, true); //true ->  case insensitive
    //    m_HeldObjectText.text = argObjName;
    //}

    public void Set_ItemReached(GameObject ItemObj)
    {
        ItemToToss = ItemObj.GetComponent<StoryItem>();
        _HeldObject = ItemToToss.MyType;
        ItemObj.transform.parent = GetMyRightHandHold();
    }

    public void TossToDwellerHand()
    {
        AnimateToss();
        //  ItemToToss.MoveTO(transform.GetChild(0), argHand, false);
        // ItemToToss.MoveTO(GetMyRightHandHold(), argHand, false);
    }
    void ActialTossPeakRegisterStartMoveObject()
    {
        GameManager.Instance.AirBornObj = ItemToToss.transform;
        ItemToToss.MoveTO(GetMyRightHandHold(), GameManager.Instance.GetCurDweller().LeftHandHoldPos, false);
    }
    void AnimateToss() { _MyAnimator.SetTrigger("TrigToss"); }
    public void AnimateCatch() { _MyAnimator.SetTrigger("TrigCatch"); }

    bool turnright = false;
    public void Animateturn()
    {
        turnright = !turnright;
        if (turnright)
            _MyAnimator.SetTrigger("TrigTurn");
        else
            _MyAnimator.SetTrigger("TrigUnTurn");
    }

    //public void AnimTossPeack()
    //{
    //    ActialTossPeakRegisterStartMoveObject();
    //}

    //public void AnimCatchPeack()
    //{

    //}

    //public void ToossPeack()
    //{
    //    ActialTossPeakRegisterStartMoveObject();
    //}

    public void CatchPeack()
    {
        Set_ItemReached(GameManager.Instance.AirBornObj.gameObject);
    }

    void ICharacterAnim.AnimateToss()
    {
        throw new System.NotImplementedException();
    }

    public void AnimTossPeack()
    {
        ActialTossPeakRegisterStartMoveObject();
    }

    public void AnimCatchPeack()
    {
        print("wtf");
        CatchPeack();
    }
}
