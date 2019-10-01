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
        if (OnCurSequenceChanged != null) OnCurSequenceChanged(gs);
    }

    //GameSequenceType
}
