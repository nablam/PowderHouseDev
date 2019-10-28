using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using static GameEnums;

public class AnimalCentralCommand : MonoBehaviour, ITaskable
{

    CharacterAnimatorController _myAnimatorCtrl;
    CharacterItemManager _myItemManager;

    string Myname = "bellhopDefault";

    public string GEtMyName(bool Compound)
    {
        if (!gameObject.CompareTag("Player"))
        {
            if (Compound)
                Myname = GetComponent<DwellerMeshComposer>().AnimalName;
            else
                Myname = GetComponent<DwellerMeshComposer>().AnimalName + " the " + GetComponent<DwellerMeshComposer>().AnimalType;
        }
        else
            Myname = "bellhopDefault";

        return Myname;
    }
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
        // _myAnimatorCtrl.ResetAgent();
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

        if (argAnimStateName == "Toss")
            _myAnimatorCtrl.Get_myAnimator().CrossFade(argAnimStateName, 0f);
        else if (argAnimStateName == "Catch1")
            _myAnimatorCtrl.Get_myAnimator().CrossFade(argAnimStateName, 0.2f);
        else
            _myAnimatorCtrl.Get_myAnimator().CrossFade(argAnimStateName, 0.4f);

    }


    void PullItem(Transform argPulledObjTran, Transform startMarker, Transform endMarker, AnimalCharacterHands argmyhand)
    {
        DeliveryItem di = argPulledObjTran.GetComponent<DeliveryItem>();
        if (di == null)
        {
            Debug.LogError("not an  Item ");
            _myAnimatorCtrl.Get_myAnimator().SetTrigger("TrigMoveOn");
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

                    catcherTriggered = true;
                    if (GameSettings.Instance.ShowDebugs)
                        Debug.Log("hey catch reflex NOW");
                }
            }
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        arrivedCallBAck();//for attaching to new owner
        _myAnimatorCtrl.Get_myAnimator().SetTrigger("TrigMoveOn");

        Debug.Log("ReachedDestination");

    }


    public void Warp(Transform argTransWarp)
    {
        _myAnimatorCtrl.WarpMeAgentto(argTransWarp);
    }

    public void TaskEnded()
    {

        BellHopGameEventManager.Instance.Call_SimpleTaskEnded();
    }
    public void ActivateAgent()
    {
        _myAnimatorCtrl.ActivateAgent();
    }
    // [Tooltip("must run Toss action on other before this")]
    public void Pull_Coordinate(ITaskable argOther, AnimalCharacterHands argfromTheirHand, AnimalCharacterHands argToMyHand) //these hands get gobbled up in next stack call
    {

        Animate(GameSettings.Instance.Catch1);// and I chill waiting for the object to finsh its flight to me, i then trgger moveon on myself to finshe my catch animwhich will in turn trigger the NADA Done.tag and signal end of task and request the next task 
        Pull_FromTheirhand_TomyHand(argOther.GetMyItemManager(), argfromTheirHand, argToMyHand);
    }

    void Pull_FromTheirhand_TomyHand(CharacterItemManager argOtherCharItemManager, AnimalCharacterHands argfromTheirHand, AnimalCharacterHands argToMyHand)
    {
        //check the other dude's right hand only
        if (argOtherCharItemManager.HasItem(argfromTheirHand))
        {
            Transform MyTempHand_Trans = (argToMyHand == AnimalCharacterHands.Right) ? _myItemManager.RightHandHoldPos : _myItemManager.LeftHandHoldPos;

            Transform startMarker_ChildTrans = argOtherCharItemManager.GetItem_LR(argfromTheirHand).transform;
            argOtherCharItemManager.DetachItem(argfromTheirHand);
            PullItem(startMarker_ChildTrans, argOtherCharItemManager.RightHandHoldPos, MyTempHand_Trans, argToMyHand);
        }
        else
        {
            Debug.LogError("no Item their " + argfromTheirHand.ToString() + " hand");
            _myAnimatorCtrl.Get_myAnimator().SetTrigger("TrigMoveOn");
        }
    }

    public CharacterItemManager GetMyItemManager()
    {
        return _myItemManager;
    }

    public void MoveOnTrigger()
    {
        _myAnimatorCtrl.Get_myAnimator().SetTrigger("TrigMoveOn");
    }
}
