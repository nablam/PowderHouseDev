#define DebugOn
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

    int _requestedFloor;
    int _floorNumArrivedAt;
    bool firstTime = true;
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
    DeliveryItem MyContextItem;
    private void Start()
    {
        #region GAmeTAskHandler MAker initer
        taskSystem = new BHG_TaskSystem();
        //CM_Worker worker = CM_Worker.Create(new Vector3(500, 500));
        //  CM_WorkerTaskAI workerTaskAI = worker.gameObject.AddComponent<CM_WorkerTaskAI>();


        #endregion





        //GoodFloor_Sequence = new List<string>
        //{
        //    GameSettings.Instance.Catch1,
        //    GameSettings.Instance.Hello,
        //    GameSettings.Instance.Catch1,

        //};

        //GoodFloor_Sequence = new List<string>
        //{
        //    GameSettings.Instance.Toss,
        //    GameSettings.Instance.Wave1,
        //    GameSettings.Instance.Wave1,

        //};



        //tasks = new List<BHG_Task>
        //{
        //    BunnyCome,
        //    DwellerSayHi,
        //    BunnySayHi,
        //    DwellerCome,
        //    BunnyCome,
        //};

    }

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

    public void ReInitContextObjectsOnArrivalTOFloor(ICharacterAnim argBunny, ICharacterAnim argDweller, DeliveryItem argContextItem)
    {
        MyBunny = argBunny;
        MyDweller = argDweller;
        MyContextItem = argContextItem;
        _tempdwellertemp = argDweller.TemMyGO();
        _tempBunnyObj = argBunny.TemMyGO();

        MAkeTasks();

        //  Setup(MyBunny, MyDweller, MyContextItem);


        startExecutionShouldBeAfterDoorSopen();
        //  RequestNextTask();
    }


    int curActionIndex = 0;
    //TODO and also after analysis of context items , is the context item on the correct floor basically check item's dest floor against curfloor number 
    void startExecutionShouldBeAfterDoorSopen()
    {
        // if (curActionIndex >= GoodFloor_Sequence.Count) return;

        // MyBunny.AnimTrigger(GoodFloor_Sequence[curActionIndex]);
        //  curActionIndex++;


#if DebugOn
        print("WHEN IS THIS CALLED");
#endif
        BHG_Task task = taskSystem.RequestNextTask();
        if (task != null)
            ExecuteTask(task);
        else
        {
#if DebugOn
            print("Signal End of Animations on this floor");
#endif

        }
    }








    #region TaskAIrequests

    private enum State
    {
        WaitingForNextTask,
        ExecutingTask,
    }

    private ICharacterAnim _myIbunny;
    private ICharacterAnim _myIDweller;
    private DeliveryItem _myItem;
    private BHG_TaskSystem taskSystem;
    private State state;
    private float waitingTimer;

    //public void Setup(ICharacterAnim argworker_Bunny, ICharacterAnim argworker_Dweller, DeliveryItem argItem)
    //{
    //    this._myIbunny = argworker_Bunny;
    //    this.MyDweller = argworker_Dweller;
    //    this._myItem = argItem;
    //    // this.taskSystem = taskSystem;
    //    // state = State.WaitingForNextTask;
    //}

    //private void Update()
    //{
    //    switch (state)
    //    {
    //        case State.WaitingForNextTask:
    //            // Waiting to request a new task
    //            waitingTimer -= Time.deltaTime;
    //            if (waitingTimer <= 0)
    //            {
    //                float waitingTimerMax = .2f; // 200ms
    //                waitingTimer = waitingTimerMax;
    //                RequestNextTask();
    //            }
    //            break;
    //        case State.ExecutingTask:
    //            break;
    //    }
    //}

    //private void RequestNextTask()
    //{
    //    // CMDebug.TextPopup("RequestNextTask", worker.GetPosition());
    //    BHG_Task task = taskSystem.RequestNextTask();
    //    if (task == null)
    //    {
    //        state = State.WaitingForNextTask;
    //    }
    //    else
    //    {
    //        state = State.ExecutingTask;
    //        ExecuteTask(task);
    //    }
    //}



    //private void ExecuteTask(BHG_Task task)
    //{
    //    //   CMDebug.TextPopup("ExecuteTask", worker.GetPosition());
    //    _myIbunny.AnimateNamedAction(task.TheActionName, () =>   //and when he gets there run the call backs  or void Onarrivetopos(){ state= Stae.WaitingForNexrTask;}
    //    {
    //        state = State.WaitingForNextTask;
    //    });
    //}


    private void ExecuteTask(BHG_Task task)
    {
        //   CMDebug.TextPopup("ExecuteTask", worker.GetPosition());
        ICharacterAnim ica = task.TheCharacter;
        ica.AnimateNamedAction(task.TheActionName, startExecutionShouldBeAfterDoorSopen);//and when he gets there run the call backs  or void Onarrivetopos(){ state= Stae.WaitingForNexrTask;}



    }


    #endregion



}




