using System;
using UnityEngine;

public interface ICharacterAnim
{


    // to be used as anim event handler
    void AnimTossPeack();
    void AnimCatchPeack();


    //to be called by other objects
    void AnimateToss(Action CallBackOnAnimEvent);
    void AnimateCatch(Action CallBackOnAnimEvent);

    // void AnimTrigger(GameEnums.DwellerAnimTrigger argtrig);
    void AnimTrigger(string argTrig);

    void AnimateNamedAction(string argactionNAme, Action OnEnded_slash_ArrivedAtPos_Callback = null);

    void AnimatorPlay(string argname);
    void ResumAgent();


    void ReleaseObj_CalledExternally();
    Transform GetMyRightHandHold();
    Transform GetMyLeftHandHold();
    void NotifyMeWheanAnimationStateExit();
    //GameObject TemMyGO();

    void OnAnimationstateTaggedDoneExit();


    void WarpAgent(Transform artT);
    void Plz_GOTO(Transform artT, bool argDoWalk);
    void Interupt_then_GOTO(Transform artT);
    // void AgentMustSetTarget(Transform artT);
}
