using UnityEngine;
using static GameEnums;

public class TA_DwellerPullCoord_2L : MonoBehaviour, ITaskAction
{
    ITaskable _theCharacter;
    ITaskable _theOTHERCharacter;
    AnimalCharacterHands _fromHand;
    AnimalCharacterHands _toMyHand;

    // Dweller Always pulls from Bunny 's right ,  ... forget it , just make a pull 2L and pull 2R objects
    public TA_DwellerPullCoord_2L(ITaskable taskableAnimal, ITaskable theOtherstolenFrom/* , AnimalCharacterHands argTheirhandFrom,  AnimalCharacterHands argMyHandTO*/)
    {
        TheCharacter = taskableAnimal;
        TheOTHERCharacter = theOtherstolenFrom;
        FromHand = AnimalCharacterHands.Right;
        ToMyHand = AnimalCharacterHands.Left;//<--------------------------------------------2L

    }

    public ITaskable TheCharacter { get => _theCharacter; set => _theCharacter = value; }
    public ITaskable TheOTHERCharacter { get => _theOTHERCharacter; set => _theOTHERCharacter = value; }
    public AnimalCharacterHands FromHand { get => _fromHand; set => _fromHand = value; }
    public AnimalCharacterHands ToMyHand { get => _toMyHand; set => _toMyHand = value; }




    public void RunME()
    {
        Debug.Log("run Dwel pull 2L");
        _theCharacter.Pull_Coordinate(_theOTHERCharacter, _fromHand, _toMyHand);

    }
}
