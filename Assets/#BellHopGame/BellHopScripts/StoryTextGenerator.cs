using UnityEngine;

public class StoryTextGenerator : MonoBehaviour
{

    GameSettings _gs;
    HotelFloorsManager _hotelManager;

    int IncreaseDiffafter3 = 3;



    public void InitMyRefs(HotelFloorsManager arghotemanager) { _hotelManager = arghotemanager; }


    // Start is called before the first frame update
    void Start()
    {
        _gs = GameSettings.Instance;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public string SimpleRiddle_takethisto(DeliveryItem argDeliveryItem, int argCurfloor, int numberofWronsForThisSession)
    {
        string nameofGiver = _hotelManager.GetCurFloorDweller().GEtMyName(true);
        string nameOfDestinationDude = argDeliveryItem.GetDestFloorDweller().AnimalName;
        string ItemNAme = argDeliveryItem.RefName;
        int DestinationFloorNumber = argDeliveryItem.GetDestFloorDweller().MyFinalResidenceFloorNumber;
        int RandomFloorDiscovered = _hotelManager.Get_Random_FloorDiscovered();


        string strToSay = "";
        if (numberofWronsForThisSession == 0)
        {
            if (_hotelManager.DiscoveredFloors.Count <= 1)
            {
                //we jsut started 
                strToSay = "a_" + Take_thisThing_to_FloorNumberRelativeToHere(ItemNAme, DestinationFloorNumber, argCurfloor);

            }
            else
            {
                strToSay = "b_" + Take_thisThing_to_FloorNumberRelativeToHere(ItemNAme, DestinationFloorNumber, argCurfloor);

            }
        }
        else
        if (numberofWronsForThisSession > 0 && numberofWronsForThisSession < 2)
        {
            strToSay = "c_" + Take_this_to_FloorNumberRelativeTo_Memorized(ItemNAme, DestinationFloorNumber, RandomFloorDiscovered);
        }
        else if (numberofWronsForThisSession >= 2)
        {
            strToSay = "d_" + Take_thisThing_to_FloorNumber(ItemNAme, argDeliveryItem.GetDestFloorDweller().MyFinalResidenceFloorNumber);

        }

        return strToSay;
    }



    public string SimpleRiddle_takethisto_WithAlgebra(DeliveryItem argDeliveryItem, int argCurfloor, int numberofWronsForThisSession)
    {
        string strToSay = "";
        string nameofGiver = _hotelManager.GetCurFloorDweller().GEtMyName(true);
        string nameOfDestinationDude = argDeliveryItem.GetDestFloorDweller().AnimalName;
        string ItemNAme = argDeliveryItem.RefName;
        int DestinationFloorNumberZeroBased = argDeliveryItem.GetDestFloorDweller().MyFinalResidenceFloorNumber;
        DestinationFloorNumberZeroBased++;
        int RandomFloorDiscovered = _hotelManager.Get_Random_FloorDiscovered();

        Debug.Log("randFloor " + RandomFloorDiscovered);

        int X = Random.Range(0, (_gs.Master_Number_of_Floors * 2));
        int Y = Mathf.Abs(X - DestinationFloorNumberZeroBased);
        if (X >= DestinationFloorNumberZeroBased)
        {

            //do substractiom
            strToSay = X.ToString() + " - " + Y.ToString() + " = ";
        }
        else
        {
            //do addition
            strToSay = X.ToString() + " + " + Y.ToString() + " = ";

        }



        return strToSay;
    }

    string Take_thisThing_to_FloorNumber(string argThisthing, int argDestFloorNum)
    {

        return "direct " + argThisthing + " to floor " + argDestFloorNum;
    }


    string Take_thisThing_to_FloorNumberRelativeToHere(string argThisthing, int argDestFloorNum, int argCurFloor)
    {
        string FloorsUpDown = "";
        string Floor_S = "Floor ";
        int Diff = 0;


        if (argDestFloorNum > argCurFloor)
        {
            FloorsUpDown = " Up ";
            Diff = argDestFloorNum - argCurFloor;
        }
        else
        {
            FloorsUpDown = " Down ";
            Diff = argCurFloor - argDestFloorNum;
        }



        if (Diff > 1)
        {
            Floor_S = "Floors ";
        }


        return " " + argThisthing + "   " + Diff.ToString() + " " + Floor_S + FloorsUpDown + "from here";
    }


    string Take_this_to_MemorizesDiscoveredGuy(string argThisthing, string argDiscoveredDwellerName)
    {

        return "  " + argThisthing + " to  " + argDiscoveredDwellerName;

    }


    string Take_this_to_FloorNumberRelativeTo_Memorized(string argThisthing, int argDestFloorNum, int argRandMemorizedFloor)
    {
        string FloorsUpDown = "";
        string Floor_S = "Floor ";
        int Diff = 0;


        if (argDestFloorNum > argRandMemorizedFloor)
        {
            FloorsUpDown = " Up ";
            Diff = argDestFloorNum - argRandMemorizedFloor;
        }
        else
        {
            FloorsUpDown = " Down ";
            Diff = argRandMemorizedFloor - argDestFloorNum;
        }
        Diff--;

        if (Diff > 1)
        {
            Floor_S = "Floors ";
        }



        return "the dweller " + Diff.ToString() + " " + Floor_S + FloorsUpDown + "from " + _hotelManager.Get_FloorDwellerNAmeOnFloor(argRandMemorizedFloor) + " need this" + argThisthing;

    }
}
