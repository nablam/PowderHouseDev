﻿//#define DebugOn
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
    Quaternion OriginalRot;
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
        OriginalRot = this.transform.rotation;
    }
    Quaternion RotationFaceScreen = Quaternion.Euler(0, 180, 0);
    Quaternion Rotation_faceDweller = Quaternion.Euler(0, 330f, 0);
    Quaternion Rotation_faceBunny = Quaternion.Euler(0, 160f, 0);
    bool ClockWeis = true;
    bool reachedWantedRot = false;
    float timeT;
    void Update()
    {




        if (Input.GetKeyDown(KeyCode.D))
        {
            ClockWeis = !ClockWeis;
            //Quaternion currentRotation = transform.rotation;
            reachedWantedRot = false;
            timeT = Time.time;
        }

        if (ClockWeis)
        {


            if (!reachedWantedRot)
            {


                Vector3 relativePos = GameFlowManager.Instance.GEt_DwellerPos() - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(relativePos);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 2 * Time.deltaTime);


                //timeT += Time.deltaTime;
                // transform.rotation = Quaternion.Lerp(RotationFaceScreen, Rotation_faceDweller, timeT * 0.5f);
                float angle = Quaternion.Angle(transform.rotation, toRotation);



                if (angle < 5f)
                {
                    Debug.Log("Reached Rot dwell");
                    reachedWantedRot = true;
                }


            }


        }
        else
        {


            if (!reachedWantedRot)
            {
                // timeT += Time.deltaTime;
                // transform.rotation = Quaternion.Lerp(Rotation_faceDweller, RotationFaceScreen, timeT * 0.5f);
                Vector3 relativePos = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z) - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(relativePos);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 2 * Time.deltaTime);
                float angle = Quaternion.Angle(transform.rotation, toRotation);



                if (angle < 3f)
                {
                    Debug.Log("Reached Rot screen");
                    reachedWantedRot = true;
                }


            }


            // Quaternion wantedRotation = Quaternion.Euler(0, 0, 0);
            //transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * 20f);
            //  transform.rotation = Quaternion.Lerp(t, OriginalRot, Time.time * 0.2f);
        }

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Quaternion currentRotation = transform.rotation;
        //    Quaternion wantedRotation = OriginalRot; // Quaternion.Euler(0, 0, 90);
        //    transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * 20f);
        //}


        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    AnimateToss();
        //}
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    AnimateCatch();
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Animateturn();
        //}
    }

    public void Set_ItemReached(GameObject ItemObj)
    {
        ItemToToss = ItemObj.GetComponent<StoryItem>();
        _HeldObject = ItemToToss.MyType;
        ItemObj.transform.parent = GetMyRightHandHold();
    }


    //void ActialTossPeakRegisterStartMoveObject()
    //{
    //    if (_gm != null)
    //    {
    //        _gm.AirBornObj = ItemToToss.transform;
    //        ItemToToss.MoveTO(GetMyRightHandHold(), _gm.GetCurDweller().LeftHandHoldPos, false);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No GameManager!!");
    //    }

    //}
    //public void AnimateToss() { _MyAnimator.SetTrigger("TrigToss"); }
    //public void AnimateCatch() { _MyAnimator.SetTrigger("TrigCatch"); }

    bool turnright = false;
    public void Animateturn()
    {
        turnright = !turnright;
        if (turnright)
            _MyAnimator.SetTrigger("TrigTurn");
        else
            _MyAnimator.SetTrigger("TrigUnTurn");
    }


    //anim event handler
    Action MidTossCallBAck;
    Action MidCatchCallBAck; //may not be needed
    public void AnimateToss(Action argTossAimeEvnet)
    {
        MidTossCallBAck = argTossAimeEvnet;
        PlayingAnimState = GameSettings.Instance.Toss;
        _MyAnimator.SetTrigger("TrigToss");
    }

    public void AnimateCatch(Action argCatchAimeEvnet)
    {
        MidCatchCallBAck = argCatchAimeEvnet;
        PlayingAnimState = GameSettings.Instance.Catch;
        _MyAnimator.SetTrigger("TrigCatch");
    }
    public void AnimTossPeack()
    {
#if DebugOn
        Debug.Log("Dweller: TOSS APEXXX");
#endif
        // BellHopGameEventManager.Instance.Call_CurSequenceChanged(GameEnums.GameSequenceType.DwellerReleaseObject);
        MidTossCallBAck();
    }

    public void AnimCatchPeack()
    {
#if DebugOn
        Debug.Log("Dweller: CATCH APEXXX");
#endif

        MidCatchCallBAck();
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
        // print("sanitycheck Bunny anim " + "Trig" + argTrig);
        _MyAnimator.SetTrigger("Trig" + argTrig);
    }

    string PlayingAnimState = "";
    Action WhatToDoWhenThisAnimStateEnds = null;
    public void AnimateNamedAction(string argactionNAme, Action OnEnded_slash_ArrivedAtPos_Callback = null)
    {
        PlayingAnimState = argactionNAme;
        WhatToDoWhenThisAnimStateEnds = OnEnded_slash_ArrivedAtPos_Callback;
        AnimTrigger(argactionNAme); //ads trig to it 
    }

    public void NotifyMeWheanAnimationStateExit()
    {
#if DebugOn
        Debug.Log("Hops: Notifiedby animator end of " + PlayingAnimState);
#endif
        WhatToDoWhenThisAnimStateEnds();
    }

    public GameObject TemMyGO()
    {
        return this.gameObject;
    }

    public void SpecialSequenceFinished()
    {
        throw new NotImplementedException();
    }
}
