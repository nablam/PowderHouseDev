using System.Collections.Generic;
using UnityEngine;

public class NamedActionsController : MonoBehaviour
{
    // good
    List<string> GoodFloor_Sequence;
    List<string> BadFloor_Sequence;
    ICharacterAnim MyBunny;
    ICharacterAnim MyDweller;
    DeliveryItem MyContextItem;
    private void Awake()
    {
        GoodFloor_Sequence = new List<string>
        {
            GameSettings.Instance.Catch1,
            GameSettings.Instance.Hello,
            GameSettings.Instance.Catch1,

        };

        GoodFloor_Sequence = new List<string>
        {
            GameSettings.Instance.Toss,
            GameSettings.Instance.Wave1,
            GameSettings.Instance.Wave1,

        };

    }
    public void ReInitContextObjectsOnArrivalTOFloor(ICharacterAnim argBunny, ICharacterAnim argDweller, DeliveryItem argContextItem)
    {
        MyBunny = argBunny;
        MyDweller = argDweller;
        MyContextItem = argContextItem;

        startExecutionShouldBeAfterDoorSopen();
    }

    //TODO and also after analysis of context items , is the context item on the correct floor basically check item's dest floor against curfloor number 
    void startExecutionShouldBeAfterDoorSopen()
    {

        MyBunny.AnimTrigger(GoodFloor_Sequence[0]);


    }


    #region TaskAIrequests

    private enum State
    {
        WaitingForNextTask,
        ExecutingTask,
    }

    private ICharacterAnim worker;
    private BHG_TaskSystem taskSystem;
    private State state;
    private float waitingTimer;

    public void Setup(ICharacterAnim worker, BHG_TaskSystem taskSystem)
    {
        this.worker = worker;
        this.taskSystem = taskSystem;
        state = State.WaitingForNextTask;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingForNextTask:
                // Waiting to request a new task
                waitingTimer -= Time.deltaTime;
                if (waitingTimer <= 0)
                {
                    float waitingTimerMax = .2f; // 200ms
                    waitingTimer = waitingTimerMax;
                    RequestNextTask();
                }
                break;
            case State.ExecutingTask:
                break;
        }
    }

    private void RequestNextTask()
    {
        // CMDebug.TextPopup("RequestNextTask", worker.GetPosition());
        BHG_Task task = taskSystem.RequestNextTask();
        if (task == null)
        {
            state = State.WaitingForNextTask;
        }
        else
        {
            state = State.ExecutingTask;
            ExecuteTask(task);
        }
    }

    private void ExecuteTask(BHG_Task task)
    {
        //   CMDebug.TextPopup("ExecuteTask", worker.GetPosition());
        worker.AnimateNamedAction(task.TheActionName, () =>   //and when he gets there run the call backs  or void Onarrivetopos(){ state= Stae.WaitingForNexrTask;}
        {
            state = State.WaitingForNextTask;
        });
    }
    #endregion
}




