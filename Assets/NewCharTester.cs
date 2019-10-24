using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using static GameEnums;

public class NewCharTester : MonoBehaviour
{
    CharacterItemManager _dwellerItemManager;
    CharacterItemManager _bellhopItemManager;

    CharacterAnimatorController _dwellerAnimatorController;
    CharacterAnimatorController _bellhopAnimatorController;

    public GameObject BellHopObj;
    public GameObject DwellerObj;


    public List<DeliveryItem> Items = new List<DeliveryItem>();


    List<Func<string, CharacterAnimatorController, IEnumerator>> MyCoroutines;
    int curFuncIndex = 0;
    bool started = false;

    public GameObject targ1;
    public GameObject targ2;
    public GameObject targ3;
    public GameObject targ4;
    Transform target = null;

    Func<Vector3, string, Action, CharacterAnimatorController, IEnumerator> FA1;
    // Start is called before the first frame update
    void Awake()
    {
        MyCoroutines = new List<Func<string, CharacterAnimatorController, IEnumerator>>();


        Func<string, CharacterAnimatorController, IEnumerator> F1 = new Func<string, CharacterAnimatorController, IEnumerator>(DoTheAnim);
        Func<string, CharacterAnimatorController, IEnumerator> F2 = new Func<string, CharacterAnimatorController, IEnumerator>(DoTheAnimwait);
        FA1 = new Func<Vector3, string, Action, CharacterAnimatorController, IEnumerator>(DoTheRot);
        MyCoroutines.Add(F1);
        MyCoroutines.Add(F2);
        _dwellerItemManager = DwellerObj.GetComponent<CharacterItemManager>();
        _bellhopItemManager = BellHopObj.GetComponent<CharacterItemManager>();

        _dwellerAnimatorController = DwellerObj.GetComponent<CharacterAnimatorController>();
        _bellhopAnimatorController = BellHopObj.GetComponent<CharacterAnimatorController>();



        GameObject Item_a = Instantiate(Items[UnityEngine.Random.Range(3, Items.Count)].gameObject);
        DeliveryItem Di_a = Item_a.GetComponent<DeliveryItem>();
        GameObject Item_b = Instantiate(Items[UnityEngine.Random.Range(0, 3)].gameObject);
        DeliveryItem Di_b = Item_b.GetComponent<DeliveryItem>();

        Test_AttachItem(Di_a, AnimalCharacterHands.Right, _bellhopItemManager);
        Test_Show_LR(true, AnimalCharacterHands.Right, _bellhopItemManager);

        Test_AttachItem(Di_b, AnimalCharacterHands.Left, _dwellerItemManager);
        Test_Show_LR(true, AnimalCharacterHands.Left, _dwellerItemManager);

        // target = targ3.transform;
        // StartCoroutine(FA1(target.position, "Wave2", () => print("hi"), _dwellerAnimatorController));
        //  _bellhopAnimatorController.ActivateAgent();
        //  _bellhopAnimatorController.WarpMeAgentto(targ4.transform);
    }

    // Update is called once per frame
    bool useRotonly = false;
    /*  void Update()
      {

          if (Input.GetKeyDown(KeyCode.Alpha1))
          {
              _bellhopAnimatorController.Reset_ReachedRot();
              target = targ1.transform;
              _bellhopAnimatorController.ResetAgent();
              _bellhopAnimatorController.Set_Destination(target, useRotonly);

              //_bellhopAnimatorController.Set_TargetTRans(target);

          }

          if (Input.GetKeyDown(KeyCode.Alpha2))
          {
              _bellhopAnimatorController.Reset_ReachedRot();
              target = targ2.transform;
              _bellhopAnimatorController.ResetAgent();
              _bellhopAnimatorController.Set_Destination(target, useRotonly);

              //_bellhopAnimatorController.Set_TargetTRans(target);
          }

          if (Input.GetKeyDown(KeyCode.Alpha3))
          {
              useRotonly = true;
              _bellhopAnimatorController.Reset_ReachedRot();
              target = targ3.transform;
              _bellhopAnimatorController.ResetAgent();
              _bellhopAnimatorController.Set_Destination(target, useRotonly);

              // _bellhopAnimatorController.Set_TargetTRans(target);
          }
          if (Input.GetKeyDown(KeyCode.Alpha4))
          {
              useRotonly = true;
              _bellhopAnimatorController.Reset_ReachedRot();
              target = targ4.transform;
              _bellhopAnimatorController.ResetAgent();
              _bellhopAnimatorController.Set_Destination(target, useRotonly);

              // _bellhopAnimatorController.Set_TargetTRans(target);
          }

          if (Input.GetKeyDown(KeyCode.Alpha5))
          {
              useRotonly = !useRotonly;
              if (useRotonly == true)
              {

              }
          }


      } */
    bool iscoroutineStarted = false;
    #region TestAttch_andAnimCoroutines
    void UpdateMyCoroutinesMethod()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!started)
            {


                StartCoroutine(MyCoroutines[curFuncIndex](GameSettings.Instance.Hello, _dwellerAnimatorController));

                started = true;
            }
            else
            {

                StopCoroutine(MyCoroutines[curFuncIndex]("Dance02", _dwellerAnimatorController));
                curFuncIndex++;
                if (curFuncIndex >= MyCoroutines.Count)
                {
                    curFuncIndex = 0;
                }
                StartCoroutine(MyCoroutines[curFuncIndex]("Dance07", _dwellerAnimatorController));
            }

        }
    }


    void Test_AttachItem(DeliveryItem Di, AnimalCharacterHands argLR, CharacterItemManager arggItemmanager)
    {

        arggItemmanager.AttachItem(Di, argLR);
    }

    void Test_Show_LR(bool argshow, AnimalCharacterHands argLR, CharacterItemManager arggItemmanager)
    {
        arggItemmanager.Show_LR(argshow, argLR);
    }


    IEnumerator DoTheAnim(string arganimname, CharacterAnimatorController argAnimatorCtrl)
    {
        Animator _animtor = argAnimatorCtrl.Get_myAnimator();
        _animtor.CrossFade(arganimname, 0.4f);
        yield return null;
    }

    IEnumerator DoTheAnimwait(string arganimname, CharacterAnimatorController argAnimatorCtrl)
    {
        Animator _animtor = argAnimatorCtrl.Get_myAnimator();
        _animtor.CrossFade(arganimname, 0.4f);
        print("started");
        yield return new WaitForSeconds(3);
        print("and bedtime");
        _animtor = argAnimatorCtrl.Get_myAnimator();
        _animtor.CrossFade(GameSettings.Instance.Come, 0.4f);
    }
    #endregion



    IEnumerator DoTheRot(Vector3 argDirection, string argNameAnimToPlayNext, Action argOnRotCompledCallBAck, CharacterAnimatorController argAnimatorCtrl)
    {
        iscoroutineStarted = true;
        // argAnimatorCtrl.TurnTo(argDirection, argNameAnimToPlayNext, argOnRotCompledCallBAck);
        yield return null;
    }


}
