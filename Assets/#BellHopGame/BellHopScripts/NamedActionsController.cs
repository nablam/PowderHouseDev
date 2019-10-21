#define DebugOn
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
        DelayedCallBAckWhenTossPeacks = TheCallBAckFor_delayGetNestTask;
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
    public void InitActionCTRL(GameEnums.TaskSequenceType argSequenceType, ICharacterAnim argBunny, ICharacterAnim argDweller, DeliveryItem argContextItem)
    {

        _CurSequenceType = argSequenceType;
        MyBunny = argBunny;
        MyDweller = argDweller;
        MyDweller.AnimTrigger("Actions");
        MyContextItem = argContextItem;
        if (_CurSequenceType == GameEnums.TaskSequenceType.CutScene)
        {
            MAkeTasks();
            TheCallBackAfterAnimStetends();
        }
        else
            if (_CurSequenceType == GameEnums.TaskSequenceType.Dweller_toss_Bunny)
        {
            MAkeDweller_toss_Bunny();
            BHG_Task task = taskSystem.RequestNextTask();
            if (task != null)
                ExecuteTask(task);
        }
        else
            if (_CurSequenceType == GameEnums.TaskSequenceType.Bunny_tossDweller)
        {
            MAkeBunnyTossDweller();
            // TheCallBackAfterAnimStetends();
            BHG_Task task = taskSystem.RequestNextTask();
            if (task != null)
                ExecuteTask(task);
        }
    }




    int curActionIndex = 0;
    //TODO and also after analysis of context items , is the context item on the correct floor basically check item's dest floor against curfloor number 
    void TheCallBackAfterAnimStetends()
    {




#if DebugOn
        print("the call back when task ends");
#endif
        BHG_Task task = taskSystem.RequestNextTask();
        if (task != null)
            ExecuteTask(task);
        else
        {
#if DebugOn
            print("Signal End of Animations on this floor");
#endif
            if (_CurSequenceType == GameEnums.TaskSequenceType.CutScene) { }
            else
                 if (_CurSequenceType == GameEnums.TaskSequenceType.Dweller_toss_Bunny) { }
            else
                 if (_CurSequenceType == GameEnums.TaskSequenceType.Bunny_tossDweller) { }
            //else
            //     if (_CurSequenceType == GameEnums.TaskSequenceType.InitialHAndoffToDweller) { }

        }
    }

    public Transform tempSTART;
    public Transform tempEND;
    void TheCallBAckFor_delayGetNestTask()
    {
#if DebugOn
        print("Yo I think TossMidMarker just kickedin");
#endif

        DeliveryItem tempItem = MyContextItem;




        if (_CurSequenceType == GameEnums.TaskSequenceType.Bunny_tossDweller)
        {
            //MoveTO(taskSystem.getCurTak().TheContextItem, taskSystem.getCurTak().TheContextItem)
            tempSTART = MyBunny.GetMyRightHandHold();
            tempEND = MyDweller.GetMyRightHandHold();
            //  MoveTO(MyContextItem, tempSTART, tempEND);
        }
        else
              if (_CurSequenceType == GameEnums.TaskSequenceType.Dweller_toss_Bunny)
        {


            tempSTART = MyDweller.GetMyRightHandHold();
            tempEND = MyBunny.GetMyRightHandHold();
            //  MoveTO(MyContextItem, tempSTART, tempEND);
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


    private void ExecuteTask(BHG_Task task)
    {
        ICharacterAnim ica = task.TheCharacter;

        if (task.TheActionName.CompareTo(GameSettings.Instance.Toss) == 0)
        {

            ica.AnimateToss(DelayedCallBAckWhenTossPeacks);
        }
        else
        if (task.TheActionName.CompareTo(GameSettings.Instance.Catch) == 0)
        {

        }
        else

            ica.AnimateNamedAction(task.TheActionName, TheCallBackAfterAnimStetends);//and when he gets there run the call backs  or void Onarrivetopos(){ state= Stae.WaitingForNexrTask;}

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

        StartCoroutine(MoveCurItemRoutine(startMarker, endMarker, argItem));

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
        timeTrigcatcher = time - 0.16f;

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

    IEnumerator Curve(DeliveryItem argItem, Transform startMarker, Transform endMarker, float height, float duration)
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
    IEnumerator Parabola(DeliveryItem argItem, Transform startMarker, Transform endMarker, float height, float duration)
    {
        Vector3 startPos = startMarker.position;
        Vector3 endPos = startMarker.position;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            argItem.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
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




