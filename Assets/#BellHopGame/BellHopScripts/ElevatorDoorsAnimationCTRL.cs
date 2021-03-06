﻿using UnityEngine;
/// <summary>
/// this will only get the event from the doors animation finished (it must be on the object that owns the animator. 
/// this will then just update the state of the door in ElevatorSceneNavigator
/// </summary>
public class ElevatorDoorsAnimationCTRL : MonoBehaviour, IDoorsAnimCTRL
{
    public void OnAnimationCompleted(string argOpenedClosed)
    {
        ElevatorDoorsMasterControl.Instance.OnElevatorDoorAnimationComplete(argOpenedClosed);
    }

}
