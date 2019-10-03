using UnityEngine;

public class BellHopGameEventManager : MonoBehaviour
{
    public static BellHopGameEventManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }





    public delegate void EventCurSequence(GameEnums.GameSequenceType gs);
    public static EventCurSequence OnCurSequenceChanged;
    public void Call_CurSequenceChanged(GameEnums.GameSequenceType gs)
    {
        OnCurSequenceChanged?.Invoke(gs);
    }

    /*
       void HeardSequenceChanged(GameEnums.GameSequenceType argGST)
    {
        switch (argGST)
        {
            case GameEnums.GameSequenceType.GameStart:
                break;

            case GameEnums.GameSequenceType.ReachedFloor:
                break;

            case GameEnums.GameSequenceType.DoorsOppned:
                break;
            case GameEnums.GameSequenceType.DwellerReactionFinished:
                break;
            case GameEnums.GameSequenceType.BunnyReleasedObject:
                break;
            case GameEnums.GameSequenceType.BunnyCaughtObject:
                break;
            case GameEnums.GameSequenceType.DwellerReleasedObject:
                break;
            case GameEnums.GameSequenceType.DwellerCaughtObject:
                break;
            case GameEnums.GameSequenceType.BunnyReaction:
                break;
            case GameEnums.GameSequenceType.DoorsClosed:
                break;

            case GameEnums.GameSequenceType.GameEnd:
                break;


        }

        IsAllowKeypad = true;
    }
    */
    public delegate void EventButtonPressed(int argNum);
    public static EventButtonPressed OnButtonPressed;
    public void Call_ButtonPressed(int argNum)
    {
        OnButtonPressed?.Invoke(argNum);
    }


}
