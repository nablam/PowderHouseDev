
//#define Debug_On
using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimExtendedIcharTester : MonoBehaviour, ICharacterAnim
{


    public TextMesh _tm;
    public Transform TargetLoooook;
    #region Interface
    public void AnimateCatch(Action CallBackOnAnimEvent)
    {
        //x
        Debug.Log("needImp");
        //y
    }

    public void AnimateNamedAction(string argactionNAme, Action OnEnded_slash_ArrivedAtPos_Callback = null)
    {
        throw new NotImplementedException();
    }

    public void AnimateToss(Action CallBackOnAnimEvent)
    {
        throw new NotImplementedException();
    }

    public void AnimatorPlay(string argname)
    {
        throw new NotImplementedException();
    }

    public void AnimCatchPeack()
    {
        throw new NotImplementedException();
    }

    public void AnimTossPeack()
    {
        throw new NotImplementedException();
    }

    public void AnimTrigger(string argTrig)
    {
        throw new NotImplementedException();
    }

    public Transform GetMyLeftHandHold()
    {
        throw new NotImplementedException();
    }

    public Transform GetMyRightHandHold()
    {
        throw new NotImplementedException();
    }

    public void Plz_GOTO(Transform artT, bool argDoWalk)
    {
        throw new NotImplementedException();
    }

    public void NotifyMeWheanAnimationStateExit()
    {
        throw new NotImplementedException();
    }

    public void OnAnimationstateTaggedDoneExit()
    {

        Debug.Log("Done.Tag anim EXIT");
        if (cancelAutoPlay) return;
        ReadInoutPlayAnim();
    }

    public void ReleaseObj_CalledExternally()
    {
        throw new NotImplementedException();
    }

    public void ResumAgent()
    {
        throw new NotImplementedException();
    }

    public GameObject TemMyGO()
    {
        throw new NotImplementedException();
    }

    public void WarpAgent(Transform artT)
    {
        throw new NotImplementedException();
    }
    public void Interupt_then_GOTO(Transform artT)
    {
        throw new NotImplementedException();
    }

    public void ShowMy_R_HeldDeliveryItem(bool argShow)
    {
        throw new NotImplementedException();

    }
    public void ShowMy_L_HeldDeliveryItem(bool argShow)
    {
        throw new NotImplementedException();

    }
    #endregion
    // Start is called before the first frame update

    Animator _myanim;


    void Start()
    {
        _myanim = GetComponent<Animator>();

        AnimsNames = new List<string>
        {
            GameSettings.Instance.Answerphone,
            GameSettings.Instance.Brushteeth,
            GameSettings.Instance.Bad,
            GameSettings.Instance.Come,
            GameSettings.Instance.Cutonion,
            GameSettings.Instance.Dialphone,
            GameSettings.Instance.Good,
            GameSettings.Instance.Hello,
            GameSettings.Instance.Happy,
            GameSettings.Instance.Investigateground,
            GameSettings.Instance.Palmpilot,
            GameSettings.Instance.Playpiano,
            GameSettings.Instance.Raking,
            GameSettings.Instance.Searchground,
            GameSettings.Instance.Slicebread,
            GameSettings.Instance.Shaving,
            GameSettings.Instance.Shrug,
            GameSettings.Instance.Typelaptop,
            GameSettings.Instance.Wave1,
            GameSettings.Instance.Wave2,
             GameSettings.Instance.SitCouch,
              GameSettings.Instance.SitChair,
               GameSettings.Instance.SitCross

        };
    }

    public int AnimIndex;
    public int LayerNumber;

    string curStr = "";


    public List<string> AnimsNames;

    void ReadInoutPlayAnim()
    {
        if (AnimIndex >= AnimsNames.Count)
        {
            AnimIndex = AnimsNames.Count - 1;


        }
        curStr = AnimsNames[AnimIndex];
        _tm.text = " " + AnimIndex + "_" + curStr;

        _myanim.Play(curStr, 0);
        AnimIndex++;
    }

    void RR_PlayAnim()
    {
        if (AnimIndex >= AnimsNames.Count)
        {
            AnimIndex = AnimsNames.Count - 1;


        }
        curStr = AnimsNames[AnimIndex];

        //print("playing " + AnimIndex + " " + curStr);
        _myanim.Play(curStr, LayerNumber);

    }

    bool cancelAutoPlay = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ReadInoutPlayAnim();
            cancelAutoPlay = true;

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            cancelAutoPlay = false;

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _myanim.Play("Run", 0);

        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            _myanim.Play("Idle", 0);

        }

        float D = Vector3.Dot(transform.right, (TargetLoooook.position - transform.position).normalized);
#if Debug_On
        Debug.Log(D);
#endif
        _myanim.SetFloat("Dot", D);
    }

    public DeliveryItem Get_CurHeldObj()
    {
        throw new NotImplementedException();
    }
}
