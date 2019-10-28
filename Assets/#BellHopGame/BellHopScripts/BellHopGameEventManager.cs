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


    public delegate void EventButtonPressed(int argNum);
    public static EventButtonPressed OnButtonPressed;
    public void Call_ButtonPressed(int argNum)
    {
        OnButtonPressed?.Invoke(argNum);
    }

    // NAMEDACTION <==strongly tied to => Animator Event Tag which needs to match the typed name of the action started ... using gamesettings reduces erorrs 
    // namedactions can be started by providing gamesetings action name, then subscribers can wait for particular named actionto finishe
    public delegate void EventNamedAction(string gs);
    public static EventNamedAction OnEventNamedActionFinished;
    public void Call_NamedActionFinished(string gs)
    {
        OnEventNamedActionFinished?.Invoke(gs);
    }


    public delegate void EvenKeyStroke(char k);
    public static EvenKeyStroke OnEvenKeyStroked;
    public void Call_KeyStroked(char k)
    {
        OnEvenKeyStroked?.Invoke(k);
    }


    public delegate void EventSimpleTaskEnded();
    public static EventSimpleTaskEnded OnSimpleTaskEnded;
    public void Call_SimpleTaskEnded()
    {
        //   Debug.Log("task or animationeneded BROADCAST");
        OnSimpleTaskEnded?.Invoke();
    }

    public delegate void EventDeliveryItemObtained(DeliveryItem argDi);
    public static EventDeliveryItemObtained OnDeliveryItemObtained;
    public void Call_DeliveryItemObtained(DeliveryItem argDi)
    {
        //  Debug.Log("task or animationeneded BROADCAST");
        OnDeliveryItemObtained?.Invoke(argDi);
    }
}
