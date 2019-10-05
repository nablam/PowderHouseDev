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
    GameManager _gm;
    // Start is called before the first frame update
    void Start()
    {
        _gm = GameManager.Instance;
        _MyAnimator = GetComponent<Animator>();
        m_NameObjectText = this.transform.GetChild(0).GetComponent<TextMesh>();
        m_HeldObjectText.text = "";
        m_HeldObjectText = this.transform.GetChild(1).GetComponent<TextMesh>();
        // UpdateHeldObject("None");
        _HeldObject = GameEnums.StoryObjects.aaNone;
    }



    public void Set_ItemReached(GameObject ItemObj)
    {
        ItemToToss = ItemObj.GetComponent<StoryItem>();
        _HeldObject = ItemToToss.MyType;
        ItemObj.transform.parent = GetMyRightHandHold();
    }

    public void TossToDwellerHand()
    {
        AnimateToss();

    }
    void ActialTossPeakRegisterStartMoveObject()
    {
        if (_gm != null)
        {
            _gm.AirBornObj = ItemToToss.transform;
            ItemToToss.MoveTO(GetMyRightHandHold(), _gm.GetCurDweller().LeftHandHoldPos, false);
        }
        else
        {
            Debug.LogWarning("No GameManager!!");
        }

    }
    public void AnimateToss() { _MyAnimator.SetTrigger("TrigToss"); }
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


    public void CatchPeack()
    {
        //if (_gm != null)
        //{
        //    Set_ItemReached(_gm.AirBornObj.gameObject);
        //}
        //else
        //{
        Debug.LogWarning("No GameManager!!");
        //}

    }



    public void AnimTossPeack()
    {
        ActialTossPeakRegisterStartMoveObject();
        Debug.Log("Dweller: Toss peakheard");
    }

    public void AnimCatchPeack()
    {
        //if (_gm != null)
        //{
        //    //_gm.IsAllowKeypad = true;
        //    CatchPeack();
        //}
        //else
        //{
        //    Debug.LogWarning("No GameManager!!");
        //}
        Debug.Log("Dweller: Catch peakheard");
    }
}
