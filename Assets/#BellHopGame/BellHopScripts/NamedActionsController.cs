﻿#define DebugOn
using System;
using System.Collections;
using UnityEngine;

public class NamedActionsController : MonoBehaviour
{

    #region EventSubscription

    //TODO: use this for end of translate animataion
    //private void OnEnable()
    //{
    //    BellHopGameEventManager.OnEventNamedActionFinished += HeardAnimstateExited;

    //}

    //private void OnDisable()
    //{
    //    BellHopGameEventManager.OnEventNamedActionFinished -= HeardAnimstateExited;

    //}




    //void HeardAnimstateExited(string argGST)
    //{

    //    string Who = argGST.Split('.')[0];
    //    string what = argGST.Split('.')[1];

    //    if (Who.CompareTo("Player") == 0)
    //    {

    //        curActionIndex++;
    //        startExecutionShouldBeAfterDoorSopen();
    //    }


    //}

    #endregion
    // good
    // List<string> GoodFloor_Sequence;
    // List<string> BadFloor_Sequence;
    // List<BHG_Task> tasks;
    [SerializeField]
    GameObject _tempBunnyObj;
    ICharacterAnim MyBunny;

    ICharacterAnim MyDweller;
    [SerializeField]
    GameObject _tempdwellertemp;
    [SerializeField]
    public DeliveryItem MyContextItem;

    GameEnums.TaskSequenceType _CurSequenceType; //will drive the GameEventSqenceChange
    #region GAmeTAskHandler MAker initer


    private void Start()
    {
        DelayedCallBAckWhenTossPeacks = CB_MoveContextItem;
        taskSystem = new BHG_TaskSystem();
    }
    #endregion

    void MAkeTasks()
    {

        BHG_Task DwellerSayHi = new BHG_Task(GameSettings.Instance.Wave1, MyDweller, MyContextItem);
        BHG_Task BunnySayHi = new BHG_Task(GameSettings.Instance.Wave2, MyBunny, MyContextItem);
        BHG_Task DwellerCome = new BHG_Task(GameSettings.Instance.Come, MyDweller, MyContextItem);
        BHG_Task BunnyCome = new BHG_Task(GameSettings.Instance.Come, MyBunny, MyContextItem);



        taskSystem.AddTask(BunnyCome);
        taskSystem.AddTask(DwellerSayHi);
        taskSystem.AddTask(BunnySayHi);
        taskSystem.AddTask(DwellerCome);
        taskSystem.AddTask(BunnyCome);
    }

    void MakeToss2wayTAsk()
    {

        BHG_Task DwellerSayHi = new BHG_Task(GameSettings.Instance.Wave1, MyDweller, MyContextItem);
        BHG_Task BunnyToss = new BHG_Task(GameSettings.Instance.Toss, MyBunny, MyContextItem);
        BHG_Task DwellerCatch1 = new BHG_Task(GameSettings.Instance.Catch1, MyDweller, MyContextItem);
        BHG_Task BunnySayHi = new BHG_Task(GameSettings.Instance.Wave2, MyBunny, MyContextItem);
        BHG_Task DwellerToss = new BHG_Task(GameSettings.Instance.Toss, MyDweller, MyContextItem);
        BHG_Task BunnyCatch1 = new BHG_Task(GameSettings.Instance.Catch1, MyBunny, MyContextItem);

        taskSystem.AddTask(DwellerSayHi);
        taskSystem.AddTask(BunnyToss);

        taskSystem.AddTask(DwellerCatch1);

        taskSystem.AddTask(BunnySayHi);
        taskSystem.AddTask(DwellerToss);

        taskSystem.AddTask(BunnyCatch1);

    }

    void MAkeDweller_toss_Bunny()
    {

        BHG_Task DwellerSayHi = new BHG_Task(GameSettings.Instance.Wave1, MyDweller, MyContextItem);
        BHG_Task BunnyTurn = new BHG_Task(GameSettings.Instance.Turn, MyBunny, MyContextItem);
        BHG_Task DwellerToss = new BHG_Task(GameSettings.Instance.Toss, MyDweller, MyContextItem);
        BHG_Task BunnyCatch1 = new BHG_Task(GameSettings.Instance.Catch1, MyBunny, MyContextItem);
        BHG_Task BunnyUnTurn = new BHG_Task(GameSettings.Instance.UnTurn, MyBunny, MyContextItem);
        taskSystem.AddTask(BunnyTurn);
        taskSystem.AddTask(DwellerSayHi);
        taskSystem.AddTask(DwellerToss);
        //taskSystem.AddTask(BunnyCatch1);
        taskSystem.AddTask(BunnyUnTurn);
        taskSystem.AddTask(BunnyUnTurn);
    }
    void MAkeBunnyTossDweller()
    {

        BHG_Task DwellerSayHi = new BHG_Task(GameSettings.Instance.Wave2, MyDweller, MyContextItem);
        BHG_Task DwellerCome = new BHG_Task(GameSettings.Instance.Come, MyDweller, MyContextItem);
        BHG_Task BunnyToss = new BHG_Task(GameSettings.Instance.Toss, MyBunny, MyContextItem);
        BHG_Task DwellerSayHEllo = new BHG_Task(GameSettings.Instance.Hello, MyDweller, MyContextItem);
        taskSystem.AddTask(DwellerSayHi);
        taskSystem.AddTask(DwellerCome);
        taskSystem.AddTask(BunnyToss);
        taskSystem.AddTask(DwellerSayHEllo);
    }

    void MAkeWrongFloorTAsk()
    {

        BHG_Task DwellerSayNO = new BHG_Task(GameSettings.Instance.No, MyDweller, MyContextItem);

        taskSystem.AddTask(DwellerSayNO);

    }
    public void InitActionCTRL(GameEnums.TaskSequenceType argSequenceType, ICharacterAnim argBunny, ICharacterAnim argDweller, DeliveryItem argContextItem)
    {

        _CurSequenceType = argSequenceType;
        MyBunny = argBunny;
        MyDweller = argDweller;

        MyContextItem = argContextItem;
        if (_CurSequenceType == GameEnums.TaskSequenceType.Wrongfloor)
        {
            MyBunny.AnimTrigger("Actions");
            MyDweller.AnimTrigger("Actions");
            MAkeWrongFloorTAsk();
            ITaskAction task = taskSystem.RequestNextTask();
            if (task != null)
                ExecuteTask(task);
        }
        else
     if (_CurSequenceType == GameEnums.TaskSequenceType.CutScene)
        {
            MyDweller.AnimTrigger("MoveOn");
            //  MAkeTasks();
            // TheCallBackAfterAnimStetends();
        }
        else
            if (_CurSequenceType == GameEnums.TaskSequenceType.Dweller_toss_Bunny)
        {
            MyBunny.AnimTrigger("Actions");
            MyDweller.AnimTrigger("Actions");
            MAkeDweller_toss_Bunny();
            ITaskAction task = taskSystem.RequestNextTask();
            if (task != null)
                ExecuteTask(task);
        }
        else
            if (_CurSequenceType == GameEnums.TaskSequenceType.Bunny_tossDweller)
        {
            MyBunny.AnimTrigger("Actions");
            MyDweller.AnimTrigger("Actions");
            MAkeBunnyTossDweller();

            // TheCallBackAfterAnimStetends();
            ITaskAction task = taskSystem.RequestNextTask();
            if (task != null)
                ExecuteTask(task);
        }
        else
            if (_CurSequenceType == GameEnums.TaskSequenceType.TowWayExchange)
        {
            MyBunny.AnimTrigger("Actions");
            MyDweller.AnimTrigger("Actions");

            MakeToss2wayTAsk();
            // TheCallBackAfterAnimStetends();
            ITaskAction task = taskSystem.RequestNextTask();
            if (task != null)
                ExecuteTask(task);
        }
    }




    int curActionIndex = 0;
    //TODO and also after analysis of context items , is the context item on the correct floor basically check item's dest floor against curfloor number 
    void TheCallBackAfterAnimStetends()
    {


        //        curActionIndex++;

        //#if DebugOn
        //        // print("the call back when task ends  " + curActionIndex + " " + _CurSequenceType.ToString());
        //#endif
        //        BHG_Task task = taskSystem.RequestNextTask();
        //        if (task != null)
        //            ExecuteTask(task);
        //        else
        //        {
        //#if DebugOn
        //            print("Signal End of Animations on this floor" + _CurSequenceType.ToString());
        //#endif
        //            if (_CurSequenceType == GameEnums.TaskSequenceType.CutScene)
        //            {
        //                BellHopGameEventManager.Instance.Call_CurSequenceChanged(GameEnums.GameSequenceType.PlayerInputs);

        //                print("No tast left cutscnene");

        //            }
        //            else
        //                 if (_CurSequenceType == GameEnums.TaskSequenceType.Dweller_toss_Bunny)
        //            {

        //                print("No tast left dwell -> bun");
        //                BellHopGameEventManager.Instance.Call_CurSequenceChanged(GameEnums.GameSequenceType.PlayerInputs);

        //            }
        //            else
        //                 if (_CurSequenceType == GameEnums.TaskSequenceType.Bunny_tossDweller)
        //            {
        //                print("No tast left bun -> dwell");
        //                BellHopGameEventManager.Instance.Call_CurSequenceChanged(GameEnums.GameSequenceType.PlayerInputs);
        //                //   InitActionCTRL(GameEnums.TaskSequenceType.Dweller_toss_Bunny, MyBunny, MyDweller, MyDweller.HELP_firstGuyOut());
        //            }
        //            else
        //                 if (_CurSequenceType == GameEnums.TaskSequenceType.TowWayExchange)
        //            {
        //                print("No tast left 2way");
        //                BellHopGameEventManager.Instance.Call_CurSequenceChanged(GameEnums.GameSequenceType.PlayerInputs);
        //                //   InitActionCTRL(GameEnums.TaskSequenceType.Dweller_toss_Bunny, MyBunny, MyDweller, MyDweller.HELP_firstGuyOut());
        //            }
        //            else
        //                 if (_CurSequenceType == GameEnums.TaskSequenceType.Wrongfloor)
        //            {
        //                print("No tast left wrongfloor");
        //                BellHopGameEventManager.Instance.Call_CurSequenceChanged(GameEnums.GameSequenceType.PlayerInputs);
        //                //   InitActionCTRL(GameEnums.TaskSequenceType.Dweller_toss_Bunny, MyBunny, MyDweller, MyDweller.HELP_firstGuyOut());
        //            }
        //            //else
        //            //     if (_CurSequenceType == GameEnums.TaskSequenceType.InitialHAndoffToDweller) { }

        //        }
    }

    public Transform tempSTART;
    public Transform tempEND;
    void CB_MoveContextItem()
    {
#if DebugOn
        print("Yo I think TossMidMarker just kickedin");
#endif

        DeliveryItem tempItem = MyContextItem;

        if (_CurSequenceType == GameEnums.TaskSequenceType.Bunny_tossDweller)
        {
            tempSTART = MyBunny.GetMyRightHandHold();
            tempEND = MyDweller.GetMyLeftHandHold();

        }
        else
              if (_CurSequenceType == GameEnums.TaskSequenceType.Dweller_toss_Bunny)
        {


            tempSTART = MyDweller.GetMyRightHandHold();
            tempEND = MyBunny.GetMyRightHandHold();
        }

        MoveTO(MyContextItem, tempSTART, tempEND);

    }





    #region TaskAIrequests

    //private enum State
    //{
    //    WaitingForNextTask,
    //    ExecutingTask,
    //}

    private ICharacterAnim _myIbunny;
    private ICharacterAnim _myIDweller;
    private DeliveryItem _myItem;
    private BHG_TaskSystem taskSystem;
    // private State state;
    private float waitingTimer;

    Action DelayedCallBAckWhenTossPeacks;


    private void ExecuteTask(ITaskAction task)
    {
        //ICharacterAnim ica = task.TheCharacter;

        //if (task.TheActionName.CompareTo(GameSettings.Instance.Toss) == 0)
        //{

        //    ica.AnimateToss(DelayedCallBAckWhenTossPeacks);
        //}
        //else
        //if (task.TheActionName.CompareTo(GameSettings.Instance.Catch) == 0)
        //{

        //}
        //else

        //    ica.AnimateNamedAction(task.TheActionName, TheCallBackAfterAnimStetends);//and when he gets there run the call backs  or void Onarrivetopos(){ state= Stae.WaitingForNexrTask;}

    }


    #endregion

    float speed = 2.0F;

    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    float journeyLength;
    float fracJourney;




    public void MoveTO(DeliveryItem argItem, Transform startMarker, Transform endMarker)
    {
        // if (argItem.transform.parent != null)
        //  {
        argItem.transform.parent = null;
        // }

        journeyLength = 10000000;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

        // StartCoroutine(MoveCurItemRoutine(startMarker, endMarker, argItem));
        StartCoroutine(Parabola(startMarker, endMarker, argItem, 0.6f, 0.5f));

    }

    private IEnumerator MoveCurItemRoutine(Transform startMarker, Transform endMarker, DeliveryItem argDeliveryItem)
    {

        float elapsedTime = 0;
        float timeTrigcatcher;
        bool catcherTriggered = false;
        float time;
        if (endMarker == null)
            yield return null;

        if (endMarker.gameObject.CompareTag("Player"))
        {
            time = 1.14f;
        }
        else
        {
            time = 2f;
        }
        //timeTrigcatcher = time * 0.8f;
        timeTrigcatcher = time - 0.26f;

        while (elapsedTime < time)
        {

            float distCovered = (Time.time - startTime) * speed;
            fracJourney = distCovered / journeyLength;

            if (elapsedTime <= timeTrigcatcher)
            {
                if (!catcherTriggered)
                {
                    Debug.Log("hey catch reflex NOW");
                    endMarker.gameObject.GetComponentInParent<ICharacterAnim>().AnimTrigger(GameSettings.Instance.Catch1);
                    //endMarker.gameObject.GetComponentInParent<ICharacterAnim>().AnimateCatch(JustTellMeSomthinAfterHEardMid__CAtch);
                    catcherTriggered = true;
                }
            }

            argDeliveryItem.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
            //Debug.Log(fracJourney);





            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("ReachedDestination");
        argDeliveryItem.transform.parent = endMarker;
        endMarker.gameObject.GetComponentInParent<ICharacterAnim>().AnimTrigger(GameSettings.Instance.Catch2);

        //BHG_Task task = taskSystem.RequestNextTask();
        //if (task != null)
        //    ExecuteTask(task);

    }

    IEnumerator Curve(Transform startMarker, Transform endMarker, DeliveryItem argItem, float height, float duration)
    {
        Vector3 startPos = startMarker.position;
        Vector3 endPos = startMarker.position; ;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = m_Curve.Evaluate(normalizedTime);
            argItem.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
    }
    public AnimationCurve m_Curve = new AnimationCurve();
    IEnumerator Parabola(Transform startMarker, Transform endMarker, DeliveryItem argItem, float height, float duration)
    {


        Vector3 startPos = startMarker.position;
        Vector3 endPos = endMarker.position;
        float normalizedTime = 0.0f;


        //float elapsedTime = 0;
        //float timeTrigcatcher;
        bool catcherTriggered = false;
        //float time;
        //if (endMarker == null)
        //    yield return null;


        while (normalizedTime < 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            argItem.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;


            if (normalizedTime > .7f)
            {
                if (!catcherTriggered)
                {
                    Debug.Log("hey catch reflex NOW");
                    endMarker.gameObject.GetComponentInParent<ICharacterAnim>().AnimTrigger(GameSettings.Instance.Catch1);
                    catcherTriggered = true;
                }
            }

            normalizedTime += Time.deltaTime / duration;


            //float distCovered = (Time.time - startTime) * speed;
            //fracJourney = distCovered / journeyLength;



            yield return null;
        }
        Debug.Log("ReachedDestination");
        argItem.transform.parent = endMarker;
        endMarker.gameObject.GetComponentInParent<ICharacterAnim>().AnimTrigger(GameSettings.Instance.Catch2);
    }
    void JustTellMeSomthinAfterHEardMid__CAtch()
    {
#if DebugOn
        print("SnapCatch");
#endif
    }
    void JustTellMeSomthinAfterHEardMid__TOSS()
    {
#if DebugOn
        print("SnapToss");
#endif
    }
}




