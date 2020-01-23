
using UnityEngine;

public class TA_GameOver : ITaskAction
{



    public TA_GameOver()
    {


    }



    public void RunME()
    {
        if (GameSettings.Instance.ShowDebugs)
            Debug.Log("run opendoors ");

        BellHopGameEventManager.Instance.Call_CurSequenceChanged(GameEnums.GameSequenceType.GameEnd);
    }
}

