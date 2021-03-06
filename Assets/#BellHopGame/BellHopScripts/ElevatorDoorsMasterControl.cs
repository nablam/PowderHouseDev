﻿using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// this script should be placed on the foreground object which is the actual sliding elevator doors. 
/// this should controll opening and closing the doors 
/// each scene will have a character in the background 
/// the main character will be a child of this object.
/// </summary>
public class ElevatorDoorsMasterControl : MonoBehaviour
{

    public static ElevatorDoorsMasterControl Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }


    /// <summary>
    /// Lets use an animator for quick turnaround. 
    /// the animation open/close will trigger an event when that animation completes
    ///  public void OnElevatorDoorAnimationComplete(string arg)  arg can be either "Opened" or "Closed"
    ///  
    ///  Elevator state starts with Closed Doors. From this state , we can only move to the Oppening state and automatically move to Oppened state . the switch happenes when TriggerActivate is triggered 
    ///  
    /// ClosedState -> TriggerActivate -> oppeneingState-> Oppened
    /// Oppened -> TriggerActivate ->closingState -> ClosedState
    /// 
    /// </summary>
    public Animator DoorAnimator;

    private bool _doorsAreOpen = false;




    public void OpenDoors()
    {
        if (!_doorsAreOpen)
        {
            StartCoroutine(OpenDoorDelay(1.3f));
        }
        else
        {
            Debug.Log("can't open an open door");
        }
    }

    public void CloseDoors()
    {

        if (_doorsAreOpen)
        {
            DoorAnimator.SetTrigger("TriggerActivate");
        }
        else
        {
            Debug.Log("can't close a closed door");
        }
    }



    /// <summary>
    /// Animation completion event listener
    /// </summary>
    /// <param name="arg"></param>
    public void OnElevatorDoorAnimationComplete(string arg)
    {
        DoorAnimator.ResetTrigger("TriggerActivate");

        if (String.Compare(arg, "Opened") == 0)
        {
            _doorsAreOpen = true;

            BellHopGameEventManager.Instance.Call_CurSequenceChanged(GameEnums.GameSequenceType.DoorsOppned);
        }
        else
           if (String.Compare(arg, "Closed") == 0)
        {
            _doorsAreOpen = false;
            BellHopGameEventManager.Instance.Call_CurSequenceChanged(GameEnums.GameSequenceType.DoorsClosed);

        }
        else
            Debug.LogError("Wrong string, check typos");
    }

    IEnumerator OpenDoorDelay(float argDelay)
    {
        yield return new WaitForSeconds(argDelay);
        DoorAnimator.SetTrigger("TriggerActivate");

    }

}
