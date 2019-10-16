﻿//#define WayPoints
using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Dweller3rdPerson : ThirdPersonCharacter
{

    ICharacterAnim _myI;

    private new void Start()
    {
        _myI = GetComponent<ICharacterAnim>();
        base.Start();
    }

    public new void Move(Vector3 move, bool crouch, bool jump)
    {
        base.Move(move, false, false);
    }

    public void JustTurn(Vector3 move, string NameOFAnimToplayOnFinished, Action argOnRotCompledCallBAck)
    {
        base.JustTurnBack(move, NameOFAnimToplayOnFinished, argOnRotCompledCallBAck);
    }


    public void ManualStartAnim(string animationName)
    {

        StartCoroutine(DoTheAnim(animationName));
    }
    IEnumerator DoTheAnim(string arganimname)
    {
        //why a coroutne? maybe wait for sec and call event to stop the anime? 

        _myI.AnimatorPlay(arganimname);

        yield return null;



    }
}
