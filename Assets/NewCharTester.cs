﻿using System;
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
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        MyCoroutines = new List<Func<string, CharacterAnimatorController, IEnumerator>>();


        Func<string, CharacterAnimatorController, IEnumerator> F1 = new Func<string, CharacterAnimatorController, IEnumerator>(DoTheAnim);
        Func<string, CharacterAnimatorController, IEnumerator> F2 = new Func<string, CharacterAnimatorController, IEnumerator>(DoTheAnimwait);
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
    }

    // Update is called once per frame
    void Update()
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


}
