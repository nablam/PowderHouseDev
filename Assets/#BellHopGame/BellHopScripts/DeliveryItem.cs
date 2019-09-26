using UnityEngine;

public class DeliveryItem : MonoBehaviour
{

    public string RefName;
    string _itemName;

    public void SetItemName(string argname) { _itemName = argname; RefName = argname; }
    public string GetItemName() { return _itemName; }

    int _destinationFloor;

    public int GetDestFloorNumber() { return _destinationFloor; }
    public void SetDestinationFloor(int argDestFloor)
    {
        _destinationFloor = argDestFloor;
    }


}
