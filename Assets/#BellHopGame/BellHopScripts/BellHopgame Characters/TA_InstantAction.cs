using UnityEngine;
using static GameEnums;

public class TA_InstantTaskHandShowHide : ITaskAction
{
    ITaskable _theCharacter;
    AnimalCharacterHands _handSide;
    bool _isShow;


    public TA_InstantTaskHandShowHide(ITaskable taskableAnimal, AnimalCharacterHands argsideTosHow, bool arghow)
    {
        TheCharacter = taskableAnimal;
        _isShow = arghow;
        _handSide = argsideTosHow;

    }

    public ITaskable TheCharacter { get => _theCharacter; set => _theCharacter = value; }
    public AnimalCharacterHands HandSide { get => _handSide; set => _handSide = value; }
    public bool IsShow { get => _isShow; set => _isShow = value; }

    public void RunME()
    {
        if (GameSettings.Instance.ShowDebugs)
            Debug.Log("run showhide pull");
        _theCharacter.GetMyItemManager().Show_LR(_isShow, _handSide);
        _theCharacter.TaskEnded(); //<-------------------------------------------task kils itself
    }
}
