using System.Collections.Generic;
using UnityEngine;

public class HotelFloorsManager : MonoBehaviour
{
    #region EventSubscription
    private void OnEnable()
    {
        //  BellHopGameEventManager.OnCurSequenceChanged += HeardSequenceChanged;
        // BellHopGameEventManager.OnButtonPressed += FloorDestRequested;
    }

    private void OnDisable()
    {
        //   BellHopGameEventManager.OnCurSequenceChanged -= HeardSequenceChanged;
        // BellHopGameEventManager.OnButtonPressed += FloorDestRequested;
    }

    //  void HeardSequenceChanged(GameEnums.GameSequenceType argGST) { }
    public void UpdateCurFloorDest(int x)
    {
        if (_floors == null) { Debug.LogError("HotelFloorsManager: No Floors list!"); return; }
        if (x >= _floors.Count) { Debug.LogError("HotelFloorsManager: floor index out of range"); return; }
        _curfloor = _floors[x];

        if (!DiscoveredFloors.Contains(x))
            DiscoveredFloors.Add(x);
        camPov.SetNextPos(_curfloor.BaseCamPos.transform);

        Debug.Log("session ");
        foreach (int d in DiscoveredFloors)
        {
            Debug.Log("df_ " + d);
        }
        Debug.Log("xxxxxxxxxx");
    }



    #endregion
    public void StartMovingCam() { camPov.StartMovingCameraDown(); }
    public CameraPov camPov;

    public List<HotelFloor> _floors;

    public List<int> DiscoveredFloors;
    HotelFloor _curfloor;
    void Start()
    {
        //_curfloor = 0; 
        //curfloor 0 is set upon initialization
    }

    public void InitializeFLoors(List<HotelFloor> argFloors)
    {
        _floors = argFloors; _curfloor = _floors[0];
        DiscoveredFloors = new List<int>();
        DiscoveredFloors.Add(0);
    }

    public int Get_Random_FloorDiscovered()
    {
        if (DiscoveredFloors == null) { return 0; }
        if (DiscoveredFloors.Count == 0) { return 0; }

        return DiscoveredFloors[Random.Range(0, DiscoveredFloors.Count)];
    }

    public void HideShowAllBarriers(bool argShow)
    {
        foreach (HotelFloor hf in _floors) { hf.ShowHideBArrier(argShow); }
    }

    public AnimalCentralCommand GetCurFloorDweller() { return _curfloor.FloorDweller; }
    public string Get_FloorDwellerNAmeOnFloor(int x)
    {
        return _floors[x].FloorDweller.GEtMyName(true);
    }


    public int GetFloorByAnimal(int argID)
    {
        return 0;
    }

    public int GetFloorByAnimal(GameEnums.DynAnimal argdynanimal)
    {
        return 0;
    }

    public HotelFloor Get_curFloor()
    {
        return _curfloor;
    }

    public void INNNINNTNPOOOW()
    {
        print("INITIALIZINGAGENTS");
        foreach (HotelFloor hf in _floors)
        {
            hf.InitDwellerAgentNowIGuess();
        }
    }


}
