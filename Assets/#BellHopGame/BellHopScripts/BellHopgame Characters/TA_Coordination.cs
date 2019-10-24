
using UnityEngine;
using static GameEnums;

public class TA_Coordination : ITaskAction
{
    ITaskable _theCharacter;
    ITaskable _theOTHERCharacter;
    CharacterItemManager _otherItemmanager;
    public TA_Coordination(ITaskable taskableAnimal, CharacterItemManager argOtherItemManager, ITaskable theOtherstolenFrom)
    {
        TheCharacter = taskableAnimal;
        TheOTHERCharacter = theOtherstolenFrom;
        OtherItemmanager = argOtherItemManager;
    }

    public ITaskable TheCharacter { get => _theCharacter; set => _theCharacter = value; }
    public ITaskable TheOTHERCharacter { get => _theOTHERCharacter; set => _theOTHERCharacter = value; }
    public CharacterItemManager OtherItemmanager { get => _otherItemmanager; set => _otherItemmanager = value; }

    public void RunME()
    {
        Debug.Log("run Bell pull");
        _theCharacter.Pull(_theOTHERCharacter, AnimalCharacterHands.Right);//Bellhop always pulls to his RIght hand making it the context item the first time he gets it
    }
}
