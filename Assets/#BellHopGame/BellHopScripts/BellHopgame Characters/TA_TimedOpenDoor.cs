
using UnityEngine;

public class TA_TimedOpenDoor : ITaskAction
{



    public TA_TimedOpenDoor()
    {


    }



    public void RunME()
    {
        if (GameSettings.Instance.ShowDebugs)
            Debug.Log("run opendoors ");
        ElevatorDoorsMasterControl.Instance.OpenDoors();
        BellHopGameEventManager.Instance.Call_SimpleTaskEnded(); //<-------------------------------------------task kils itself
    }
}
