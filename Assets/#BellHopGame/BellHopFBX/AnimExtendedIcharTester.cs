
using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimExtendedIcharTester : MonoBehaviour, ICharacterAnim
{


    public TextMesh _tm;
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

    public void MoveAgentTo(Transform artT, bool argDoWalk)
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
            GameSettings.Instance.Wave2

        };
    }

    public int AnimIndex;

    string curStr = "";
    public bool UpdateOnceToggel = false;

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

        print("playing " + AnimIndex + " " + curStr);
        _myanim.Play(curStr, 0);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ReadInoutPlayAnim();
            UpdateOnceToggel = false;

        }
    }
}
