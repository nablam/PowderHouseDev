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


}
