﻿using System;
using UnityEngine;

public interface ICharacterAnim
{


    // to be used as anim event handler
    void AnimTossPeack();
    void AnimCatchPeack();


    //to be called by other objects
    void AnimateToss();
    void AnimateCatch();

    // void AnimTrigger(GameEnums.DwellerAnimTrigger argtrig);
    void AnimTrigger(string argTrig);

    void AnimateNamedAction(string argactionNAme, Action OnEnded_slash_ArrivedAtPos_Callback = null);

    void ReleaseObj_CalledExternally();
    Transform GetMyRightHandHold();
    Transform GetMyLeftHandHold();


}
