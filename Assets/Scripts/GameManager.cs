using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    public FloorsManager MyFloorManager;

    public GameObject NumpadObj;

    /// <summary>
    /// Curentlevel story
    /// CharacterAnimation
    /// get Player input
    /// 
    /// 
    /// </summary>


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitOpenDoors(1));
    }

    // Update is called once per frame
    void Update()
    {

    }

    int DestinationFloor = 0;

    public void StartGoingToFloorOnButtonClicked(int argFloorNum)
    {
        DestinationFloor = argFloorNum;
        NumpadObj.SetActive(false);
        ElevatorDoorsMasterControl.Instance.CloseDoors();
    }

    //called from elevatordoorsMAsterCTRL when doorclosed animation complets
    public void SwitchActiveFloorWhileDoorsAreClosed()
    {
        MyFloorManager.SetCurFloor(DestinationFloor);
        StartCoroutine(WaitOpenDoors(3));
    }
    //called from elevatordoorsMAsterCTRL when doorOpen animation complets
    public void ReachedFloor()
    {
        StartCoroutine(WaitTurnKeypadOnDoors(4));
    }
    IEnumerator WaitOpenDoors(int argTimeWait)
    {
        yield return new WaitForSeconds(argTimeWait);
        ElevatorDoorsMasterControl.Instance.OpenDoors();
    }

    IEnumerator WaitTurnKeypadOnDoors(int argTimeWait)
    {
        yield return new WaitForSeconds(argTimeWait);
        NumpadObj.SetActive(true);
    }

    void WaitAndShowUIKeypad()
    {

    }
}
