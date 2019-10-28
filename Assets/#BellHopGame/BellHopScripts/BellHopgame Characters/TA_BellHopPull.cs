using UnityEngine;
using static GameEnums;

public class TA_BellHopPull : ITaskAction
{
    ITaskable _theCharacter;
    ITaskable _theOTHERCharacter;
    AnimalCharacterHands _fromHand;
    AnimalCharacterHands _toMyHand;

    public TA_BellHopPull(ITaskable taskableAnimal, ITaskable theOtherstolenFrom, AnimalCharacterHands argTheirhandFrom/*, AnimalCharacterHands argMyHandTO*/) //bellhopalwayspulls to his right
    {
        TheCharacter = taskableAnimal;
        TheOTHERCharacter = theOtherstolenFrom;
        FromHand = argTheirhandFrom;
        ToMyHand = AnimalCharacterHands.Right;//Bellhop always pulls to his RIght hand making it the context item the first time he gets it

    }

    public ITaskable TheCharacter { get => _theCharacter; set => _theCharacter = value; }
    public ITaskable TheOTHERCharacter { get => _theOTHERCharacter; set => _theOTHERCharacter = value; }
    public AnimalCharacterHands FromHand { get => _fromHand; set => _fromHand = value; }
    public AnimalCharacterHands ToMyHand { get => _toMyHand; set => _toMyHand = value; }






    public void RunME()
    {
        if (GameSettings.Instance.ShowDebugs)
            Debug.Log("run Bell pull");
        _theCharacter.Pull_Coordinate(_theOTHERCharacter, _fromHand, _toMyHand);
    }
}
