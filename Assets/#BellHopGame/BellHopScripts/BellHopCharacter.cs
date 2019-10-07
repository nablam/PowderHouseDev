using System;
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

    Animator _MyAnimator;
    GameManager _gm;
    bool _isEmptyHanded;
    public bool IsEmptyHAnded() { return this._isEmptyHanded; }
    GameObject _CurHeldObject = null;
    // Start is called before the first frame update
    void Start()
    {
        //_gm = GameManager.Instance;
        _MyAnimator = GetComponent<Animator>();
        m_NameObjectText = this.transform.GetChild(0).GetComponent<TextMesh>();
        m_HeldObjectText.text = "";
        m_HeldObjectText = this.transform.GetChild(1).GetComponent<TextMesh>();
        _isEmptyHanded = true;
        // UpdateHeldObject("None");
        _HeldObject = GameEnums.StoryObjects.aaNone;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AnimateToss();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            AnimateCatch();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Animateturn();
        }
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
        Debug.Log("BEllHop: Toss peakheard");
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
        Debug.Log("BEllHop: Catch peakheard");
    }

    //public void AnimTrigger(GameEnums.DwellerAnimTrigger argtrig)
    //{
    //    _MyAnimator.SetTrigger(argtrig.ToString());
    //}

    public void ReleaseObj_CalledExternally()
    {
        _CurHeldObject.transform.parent = null;
        _CurHeldObject = null;
    }

    public Transform GetMyRightHandHold() { return this.RightHandHoldPos; }
    public Transform GetMyLeftHandHold() { return this.LeftHandHoldPos; }

    public void AnimTrigger(string argTrig)
    {
        _MyAnimator.SetTrigger("Trig" + argTrig);
    }

    public void AnimateNamedAction(string argactionNAme, Action OnEnded_slash_ArrivedAtPos_Callback = null)
    {
        throw new NotImplementedException();
    }
}
