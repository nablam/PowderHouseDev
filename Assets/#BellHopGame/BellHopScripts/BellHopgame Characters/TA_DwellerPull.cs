using UnityEngine;
using static GameEnums;
public class TA_DwellerPull : ITaskAction
{
    ITaskable _theCharacter;
    CharacterItemManager _theOTHERCharacter;
    public TA_DwellerPull(ITaskable taskableAnimal, CharacterItemManager OthertaskableAnimal)
    {
        TheCharacter = taskableAnimal;
        TheOTHERCharacter = OthertaskableAnimal;
    }

    public ITaskable TheCharacter { get => _theCharacter; set => _theCharacter = value; }
    public CharacterItemManager TheOTHERCharacter { get => _theOTHERCharacter; set => _theOTHERCharacter = value; }

    public void RunME()
    {
        Debug.Log("run dwelpull");
        _theCharacter.Pull_Coordinate(_theOTHERCharacter, AnimalCharacterHands.Left);//dwellersalwaspull to left
    }
}
