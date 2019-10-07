using UnityEngine;

public class CameraPov : MonoBehaviour
{

    #region EventSubscription
    //private void OnEnable()
    //{
    //    BellHopGameEventManager.OnCurSequenceChanged += HeardSequenceChanged;
    //}

    //private void OnDisable()
    //{
    //    BellHopGameEventManager.OnCurSequenceChanged -= HeardSequenceChanged;
    //}

    //void HeardSequenceChanged(GameEnums.GameSequenceType argGST) { }
    #endregion



    Transform _target = null;
    public GameObject ElevatorWall;
    GameObject BunnyHop;
    public GameObject ButtonsCanvas;
    public Transform InitialPos;

    GameSettings _gs;
    ElevatorDoorsMasterControl _ElevatorDoorsCTRL;
    BellHopGameEventManager _eventManager;

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



    // Update is called once per frame
    void Update()
    {
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

                Debug.Log("Cam pov reacehd");
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

                Debug.Log("Cam pov Startpos");
                _eventManager.Call_CurSequenceChanged(GameEnums.GameSequenceType.GameStart);
                ElevatorWall.transform.parent = this.transform;
                BunnyHop.transform.parent = this.transform;
                ButtonsCanvas.SetActive(true);
                ReachedInitialPos = true;
                // _ElevatorDoorsCTRL.OpenDoors();
                BellHopGameEventManager.Instance.Call_CurSequenceChanged(GameEnums.GameSequenceType.GameStart);
            }
        }
    }
}
