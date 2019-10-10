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
        camPov.SetNextPos(_curfloor.BaseCamPos.transform);


    }
    #endregion

    public CameraPov camPov;

    public List<HotelFloor> _floors;
    HotelFloor _curfloor;
    void Start()
    {
        //_curfloor = 0; 
        //curfloor 0 is set upon initialization
    }

    public void InitializeFLoors(List<HotelFloor> argFloors) { _floors = argFloors; _curfloor = _floors[0]; }

    public void HideShowAllBarriers(bool argShow)
    {
        foreach (HotelFloor hf in _floors) { hf.ShowHideBArrier(argShow); }
    }

    public DwellerMeshComposer GetCurFloorDweller() { return _curfloor.FloorDweller; }


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

    public void WarpAll()
    {
        foreach (HotelFloor hf in _floors)
        {
            hf.WarpInit();
        }

    }
}
