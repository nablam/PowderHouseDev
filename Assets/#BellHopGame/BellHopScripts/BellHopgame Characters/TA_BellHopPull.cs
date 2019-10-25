using UnityEngine;
using static GameEnums;

public class TA_BellHopPull : ITaskAction
{
    ITaskable _theCharacter;
    CharacterItemManager _theOTHERCharacter;
    public TA_BellHopPull(ITaskable taskableAnimal, CharacterItemManager OthertaskableAnimal)
    {
        TheCharacter = taskableAnimal;
        TheOTHERCharacter = OthertaskableAnimal;
    }

    public ITaskable TheCharacter { get => _theCharacter; set => _theCharacter = value; }
    public CharacterItemManager TheOTHERCharacter { get => _theOTHERCharacter; set => _theOTHERCharacter = value; }

    public void RunME()
    {
        Debug.Log("run Bell pull");
        _theCharacter.Pull_Coordinate(_theOTHERCharacter, AnimalCharacterHands.Right);//Bellhop always pulls to his RIght hand making it the context item the first time he gets it
    }
}
