using System.Collections.Generic;
using UnityEngine;

public class HotelFloorsManager : MonoBehaviour
{
    List<HotelFloor> _floors;
    HotelFloor _curfloor;
    void Start()
    {

    }

    public void InitializeFLoors(List<HotelFloor> argFloors) { _floors = argFloors; }
    // Update is called once per frame
    void Update()
    {

    }
}
