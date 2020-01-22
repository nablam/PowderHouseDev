using System.Collections.Generic;

/// <summary>
/// 
/// new delivery session starts when bunny owns a delivery item 
/// 
/// we must record the floor on which delivery started. these lists should look like this 0 1 2     2 4     4 0 1 3       3 0
/// 
/// THE LAST FLOOR VISITED IS OBVIOUSLY THE CORRECT FLOOR , so counting session.floorvisited should tell us how many wrong floors
/// 
/// </summary>

public class DeliverySession
{

    int _id;
    public int Id { get => _id; set => _id = value; }


    DeliveryItem _deliveryItem;
    public DeliveryItem DeliveryItem { get => _deliveryItem; set => _deliveryItem = value; }

    DwellerMeshComposer _dwellerOwner;
    public DwellerMeshComposer DwellerOwner { get => _dwellerOwner; private set => _dwellerOwner = value; }

    List<int> _floorsVisited;
    public List<int> FloorsVisited { get => _floorsVisited; private set => _floorsVisited = value; }

    int _correctDestinationFloorNumver;

    int _wrongAnswers;
    public int WrongAnswers { get => _wrongAnswers; private set => _wrongAnswers = value; }
    public DeliverySession(DeliveryItem argdeliveryItem, int argID)
    {
        _id = argID;
        _deliveryItem = argdeliveryItem;
        _floorsVisited = new List<int>();
        _dwellerOwner = argdeliveryItem.GetDestFloorDweller();
        _correctDestinationFloorNumver = _dwellerOwner.MyFinalResidenceFloorNumber;
        _wrongAnswers = 0;
    }

    public int CorrectDestinationFloorNumver { get => _correctDestinationFloorNumver; private set => _correctDestinationFloorNumver = value; }


    public void AddFloorVisited(int argFloorVisited)
    {

        if (!_floorsVisited.Contains(argFloorVisited))
            _floorsVisited.Add(argFloorVisited);
    }

    public void IncrementWrongAnswers()
    {
        _wrongAnswers++;
    }
}
