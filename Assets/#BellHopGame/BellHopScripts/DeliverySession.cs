using System.Collections.Generic;

/// <summary>
/// 
/// new delivery session starts when bunny owns a delivery item 
/// 
/// we must record the floor on which delivery started. these lists should look like this 0 1 2     2 4     4 0 1 3       3 0  
/// 
/// </summary>

public class DeliverySession
{

    DeliveryItem _deliveryItem;
    public DeliveryItem DeliveryItem { get => _deliveryItem; set => _deliveryItem = value; }

    DwellerMeshComposer _dwellerOwner;
    public DwellerMeshComposer DwellerOwner { get => _dwellerOwner; private set => _dwellerOwner = value; }

    List<int> _floorsVisited;
    public List<int> FloorsVisited { get => _floorsVisited; private set => _floorsVisited = value; }

    int _correctDestinationFloorNumver;

    public DeliverySession(DeliveryItem argdeliveryItem)
    {
        _deliveryItem = argdeliveryItem;
        _floorsVisited = new List<int>();
        _dwellerOwner = argdeliveryItem.GetDestFloorDweller();
        _correctDestinationFloorNumver = _dwellerOwner.MyFinalResidenceFloorNumber;
    }

    public int CorrectDestinationFloorNumver { get => _correctDestinationFloorNumver; private set => _correctDestinationFloorNumver = value; }


    public void AddFloorVisited(int argFloorVisited)
    {
        _floorsVisited.Add(argFloorVisited);
    }
}
