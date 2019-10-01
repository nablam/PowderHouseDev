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

    void HeardSequenceChanged(GameEnums.GameSequenceType argGST) { }
    #endregion



    Transform _target = null;
    public GameObject ElevatorWall;
    public GameObject BunnyHop;

    private void Start()
    {
        ElevatorWall.SetActive(false);
        BunnyHop.SetActive(false);
    }

    //  public Transform Target { get => _target; set => _target = value; }

    bool Reached = false;

    public void SetInitialPos(Transform here)
    {
        this.transform.position = here.position;
    }
    public void SetNextPos(Transform here)
    {
        Reached = false;
        _target = here;
    }


    // Update is called once per frame
    void Update()
    {
        if (_target != null && !Reached)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, 2f);
            if (transform.position == _target.position)
            {

                Debug.Log("reacehd");
                Reached = true;
            }
        }
    }
}
