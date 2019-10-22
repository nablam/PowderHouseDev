using UnityEngine;

public class DeliveryItem : MonoBehaviour
{

    public string RefName;
    string _itemName;
    MeshRenderer _myMeshRen;
    int _ownerid;
    public int Id { get => _ownerid; set => _ownerid = value; }
    public void SetItemName(string argname) { _itemName = argname; RefName = argname; }
    public string GetItemName() { return _itemName; }

    [SerializeField]
    DwellerMeshComposer _dwellerOwner;

    public DwellerMeshComposer GetDestFloorDweller() { return _dwellerOwner; }
    public void SetOwner(DwellerMeshComposer argowner)
    {
        _dwellerOwner = argowner;
        _ownerid = _dwellerOwner.Id;
    }

    public bool IsMyOwner(DwellerMeshComposer argThisGuy)
    {

        if (argThisGuy.GetHashCode() == _dwellerOwner.GetHashCode())
            return true;
        return false;
    }


    private void Awake()
    {
        _myMeshRen = this.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        _myMeshRen.enabled = false;
    }


    public void ShowMe(bool argShowme)
    {
        _myMeshRen.enabled = argShowme;
    }

}
