using UnityEngine;

public class DeliveryItem : MonoBehaviour
{

    public string RefName;
    string _itemName;

    public void SetItemName(string argname) { _itemName = argname; RefName = argname; }
    public string GetItemName() { return _itemName; }
    [SerializeField]
    DwellerMeshComposer _destinationFloor;

    public DwellerMeshComposer GetDestFloorNumber() { return _destinationFloor; }
    public void SetDestinationFloor(DwellerMeshComposer argDestFloor)
    {
        _destinationFloor = argDestFloor;
    }


}
