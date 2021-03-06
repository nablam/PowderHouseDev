﻿//#define DebugOn
using System;
using UnityEngine;

public class BellHopCharacter : MonoBehaviour, ICharacterAnim
{
    public TextMesh m_NameObjectText;
    public TextMesh m_HeldObjectText;


    public GameEnums.StoryObjects _HeldObject;
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
        this.transform.rotation = RotationFaceScreen;
        OriginalRot = this.transform.rotation;
    }
    Quaternion RotationFaceScreen = Quaternion.Euler(0, 180, 0);
    Quaternion Rotation_faceDweller = Quaternion.Euler(0, 310f, 0);
    Quaternion Rotation_faceBunny = Quaternion.Euler(0, 160f, 0);
    //  bool ClockWeis = true;
    // bool reachedWantedRot = false;
    float timeT;
    /*  void Update()
      {


          //if (Input.GetKeyDown(KeyCode.D))
          //{
          //    ClockWeis = !ClockWeis;
          //    //Quaternion currentRotation = transform.rotation;
          //    reachedWantedRot = false;
          //    timeT = Time.time;
          //}

          if (!ClockWeis)
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


           */

    public void Set_ItemReached(GameObject ItemObj)
    {

    }


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
        Playing_Exchange_AnimState = GameSettings.Instance.Toss;
        _MyAnimator.SetTrigger("TrigToss");
    }

    public void AnimateCatch(Action argCatchAimeEvnet)
    {
        MidCatchCallBAck = argCatchAimeEvnet;
        Playing_Exchange_AnimState = GameSettings.Instance.Catch;
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



    //NOTUSED TODO:
    public void ReleaseObj_CalledExternally()
    {
        if (_CurHeldObject != null)
        {
            _CurHeldObject.transform.parent = null;
            _CurHeldObject = null;
        }
    }

    public Transform GetMyRightHandHold() { return this.RightHandHoldPos; }
    public Transform GetMyLeftHandHold() { return this.LeftHandHoldPos; }

    public void AnimTrigger(string argTrig)
    {
        //_MyAnimator.ResetTrigger();
        _MyAnimator.SetTrigger("Trig" + argTrig);
    }

    string Playing_Exchange_AnimState = "";
    Action WhatToDoWhenThisAnimStateEnds = null;
    public void AnimateNamedAction(string arg_Exchange_actionNAme, Action OnEnded_slash_ArrivedAtPos_Callback = null)
    {
        Playing_Exchange_AnimState = arg_Exchange_actionNAme;
        WhatToDoWhenThisAnimStateEnds = OnEnded_slash_ArrivedAtPos_Callback;
        AnimTrigger(arg_Exchange_actionNAme); //ads trig to it 
    }

    public void OnExchangeSequencExited()
    {
#if DebugOn
        Debug.Log("Hops: Notifiedby animator end of " + PlayingAnimState);
#endif
        WhatToDoWhenThisAnimStateEnds();
    }
    public DeliveryItem Get_CurHeldObj()
    {
        return RightHandHoldPos.GetChild(0).GetComponent<DeliveryItem>();
        //        return _CurHeldObject.GetComponent<DeliveryItem>();
    }

    public void ShowMy_R_HeldDeliveryItem(bool argShow)
    {
        if (RightHandHoldPos.childCount > 0)
        {
            RightHandHoldPos.GetChild(0).GetComponent<DeliveryItem>().ShowMe(argShow);
        }
        else
            Debug.Log("NO RightHandChild!");
    }
    public void ShowMy_L_HeldDeliveryItem(bool argShow)
    {
        if (LeftHandHoldPos.childCount > 0)
        {
            LeftHandHoldPos.GetChild(0).GetComponent<DeliveryItem>().ShowMe(argShow);
        }
        else
            Debug.Log("NO LeftHandChild!");
    }

    public void OnCrossFadedAnimationMainDoneTagExit()
    {
        throw new NotImplementedException();
    }

    public void WarpAgent(Transform artT)
    {
        throw new NotImplementedException();
    }

    public void Plz_GOTO(Transform artT, bool argDoWalk)
    {
        throw new NotImplementedException();
    }



    public void AnimatorPlay(string argname)
    {
        _MyAnimator.CrossFade(argname, 0.4f);

        // _MyAnimator.Play(argname);
    }

    public void ResumAgent()
    {
        throw new NotImplementedException();
    }

    public void Interupt_then_GOTO(Transform artT)
    {

        throw new NotImplementedException();
    }
}


