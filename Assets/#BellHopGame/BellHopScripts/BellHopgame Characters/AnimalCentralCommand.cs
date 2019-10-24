using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using static GameEnums;

public class AnimalCentralCommand : MonoBehaviour, ITaskable
{

    CharacterAnimatorController _myAnimatorCtrl;
    CharacterItemManager _myItemManager;

    private void OnEnable()
    {
        _myAnimatorCtrl = GetComponent<CharacterAnimatorController>();
        _myItemManager = GetComponent<CharacterItemManager>();
    }
    public void InitializeHeldObject(GameObject argItemInHand)
    {
        DeliveryItem di = argItemInHand.GetComponent<DeliveryItem>();
        if (di != null)
        {
            _myItemManager.AttachItem(di, AnimalCharacterHands.Right);
        }
        else
            Debug.LogError(argItemInHand.name + "is not a DeliveryItem");
    }



    public void GoTo(Transform argTransNav)
    {
        _myAnimatorCtrl.Reset_ReachedRot();
        _myAnimatorCtrl.ResetAgent();
        _myAnimatorCtrl.Set_Destination(argTransNav, false);

    }

    public void Face(Transform argTransLookAt)
    {
        _myAnimatorCtrl.Reset_ReachedRot();
        _myAnimatorCtrl.ResetAgent();
        _myAnimatorCtrl.Set_Destination(argTransLookAt, true);
    }

    public void Animate(string argAnimStateName)
    {
        _myAnimatorCtrl.Get_myAnimator().CrossFade(argAnimStateName, 0.4f);
    }

    public void Pull(CharacterItemManager argOtherCharItemManager, AnimalCharacterHands argToMyHand)
    {
        //check the other dude's right hand only
        if (argOtherCharItemManager.HasItem(AnimalCharacterHands.Right))
        {
            Transform MyTempHand_Trans = (argToMyHand == AnimalCharacterHands.Right) ? _myItemManager.RightHandHoldPos : _myItemManager.LeftHandHoldPos;

            Transform startMarker_ChildTrans = argOtherCharItemManager.GetItem_LR(AnimalCharacterHands.Right).transform;
            argOtherCharItemManager.DetachItem(AnimalCharacterHands.Right);
            PullItem(startMarker_ChildTrans, argOtherCharItemManager.RightHandHoldPos, MyTempHand_Trans, argToMyHand);
        }
        else
        {
            Debug.Log("no Item in hand");
            TaskEnded();
        }

    }

    void PullItem(Transform argPulledObjTran, Transform startMarker, Transform endMarker, AnimalCharacterHands argmyhand)
    {
        DeliveryItem di = argPulledObjTran.GetComponent<DeliveryItem>();
        if (di == null)
        {
            Debug.LogError("not an  Item ");
            TaskEnded();
            return;
        }
        StartCoroutine(Parabola(startMarker, endMarker, argPulledObjTran, 0.6f, 0.5f, () => _myItemManager.AttachItem(di, argmyhand)));

    }
    IEnumerator Parabola(Transform startMarker, Transform endMarker, Transform argPulledObjTran, float height, float duration, Action arrivedCallBAck)
    {
        Vector3 startPos = startMarker.position;
        Vector3 endPos = endMarker.position;
        float normalizedTime = 0.0f;
        bool catcherTriggered = false;

        while (normalizedTime < 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            argPulledObjTran.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            if (normalizedTime > .7f)
            {
                if (!catcherTriggered)
                {
                    Debug.Log("hey catch reflex NOW");

                    catcherTriggered = true;
                }
            }
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        Debug.Log("ReachedDestination");
        TaskEnded();
    }


    public void Warp(Transform argTransWarp)
    {
        _myAnimatorCtrl.WarpMeAgentto(argTransWarp);
    }

    public void TaskEnded()
    {
        Debug.Log("task or animationeneded BROADCAST");
    }
}
