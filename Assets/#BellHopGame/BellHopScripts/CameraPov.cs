//#define DebugOn

using UnityEngine;

public class CameraPov : MonoBehaviour
{

    #region EventSubscription
    private void OnEnable()
    {
        BellHopGameEventManager.OnCurSequenceChanged += HeardSequenceChanged;
    }

    private void OnDisable()
    {
        BellHopGameEventManager.OnCurSequenceChanged -= HeardSequenceChanged;
    }

    void HeardSequenceChanged(GameEnums.GameSequenceType argGST) { m_Text_GameFlowState.text = argGST.ToString(); }
    #endregion



    Transform _target = null;
    public GameObject ElevatorWall;
    GameObject BunnyHop;
    public GameObject ButtonsCanvas;
    public Transform InitialPos;

    public Transform BellHopLobbyPosTan;

    GameSettings _gs;
    ElevatorDoorsMasterControl _ElevatorDoorsCTRL;
    BellHopGameEventManager _eventManager;
    public TMPro.TextMeshProUGUI m_Text_Game;
    public TMPro.TextMeshProUGUI m_Text_GameFlowState;

    public NumPadCTRL numkeypad;
    private void Start()
    {
        _gs = GameSettings.Instance;
        if (_gs == null) { Debug.LogError("NumPadCTRL: no gm in scene!"); }
        _ElevatorDoorsCTRL = ElevatorDoorsMasterControl.Instance;
        if (_ElevatorDoorsCTRL == null) Debug.LogError("no static elevator door ctrl in scene!");
        _eventManager = BellHopGameEventManager.Instance;
        if (_eventManager == null) Debug.LogError("no static game Event manger in scene!");


        // ElevatorWall.SetActive(false);
        // BunnyHop.SetActive(false);
        ButtonsCanvas.SetActive(false);
    }
    public void assignBunny(GameObject argbunny) { BunnyHop = argbunny; }

    //  public Transform Target { get => _target; set => _target = value; }

    bool Reached = false;
    bool ReachedInitialPos = false;

    public void SetInitialPos(Transform here)
    {
        this.transform.position = here.position + new Vector3(0, 1, 0);
    }
    public void SetNextPos(Transform here)
    {
        Reached = false;
        _target = here;
    }

    bool startedMoving = false;
    public void StartMovingCameraDown() { startedMoving = true; }
    // Update is called once per frame
    void LateUpdate()
    {
        if (!startedMoving) { return; }
        if (ReachedInitialPos)
            MoveToTargetFloor();
        else
            MoveToBAsement();
    }

    void MoveToTargetFloor()
    {
        if (_target != null && !Reached)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _gs.ElevatorSpeed);
            if (transform.position == _target.position)
            {
#if DebugOn
                Debug.Log("Cam pov reacehd");
#endif
                _eventManager.Call_CurSequenceChanged(GameEnums.GameSequenceType.ReachedFloor);
                Reached = true;
            }
        }
    }

    void MoveToBAsement()
    {
        if (!ReachedInitialPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, InitialPos.position, _gs.ElevatorSpeed / 4f);
            if (transform.position == InitialPos.position)
            {
#if DebugOn
                Debug.Log("Cam pov Startpos");
#endif
                _eventManager.Call_CurSequenceChanged(GameEnums.GameSequenceType.GameStart);
                ElevatorWall.transform.parent = this.transform;
                // BunnyHop.transform.parent = this.transform;
                ButtonsCanvas.SetActive(true);
                ReachedInitialPos = true;
                // _eventManager.Call_CurSequenceChanged(GameEnums.GameSequenceType.ReachedFloor);

            }
        }
    }
}
