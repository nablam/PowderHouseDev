//#define DebugOn

using System.Collections;
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

    void HeardSequenceChanged(GameEnums.GameSequenceType argGST)
    {
        //m_Text_GameFlowState.text = argGST.ToString();
        m_Text_GameFlowState.text = "";
    }
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


            int VerticalIntPos = (int)(transform.position.y - 1.3f);

            if ((VerticalIntPos) % _gs.Master_Floor_Height == 0)
            {
                int numtodisplay = (VerticalIntPos / (int)_gs.Master_Floor_Height);
                numkeypad.SetFloorNumberOnDisplay(numtodisplay);
            }



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
                ReachedInitialPos = true;
                StartCoroutine(Rotate2(1.5f, 10f));


            }
        }
    }
    IEnumerator Rotate2(float duration, float argAngle)
    {
        Quaternion startRot = transform.rotation;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.rotation = startRot * Quaternion.AngleAxis(t / duration * argAngle, Vector3.up); //or transform.right if you want it to be locally based
            yield return null;
        }
        print("DONE2");
        ElevatorWall.transform.parent = this.transform;
        ButtonsCanvas.SetActive(true);

        _eventManager.Call_CurSequenceChanged(GameEnums.GameSequenceType.GameStart);
        // transform.rotation = startRot;
    }


    IEnumerator RotateForSeconds(float duration, float argAngle)
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + argAngle;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % argAngle;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;


        }
        print("DONE2");
        ElevatorWall.transform.parent = this.transform;
        ButtonsCanvas.SetActive(true);

        _eventManager.Call_CurSequenceChanged(GameEnums.GameSequenceType.GameStart);
    }
}
