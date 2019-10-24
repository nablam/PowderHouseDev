using UnityEngine;

public class TA_DwellerAnimate : ITaskAction
{
    ITaskable _theCharacter;
    string _animaStateName;
    public TA_DwellerAnimate(ITaskable taskableAnimal, string argAnimationStateToCrossfade)
    {
        _animaStateName = argAnimationStateToCrossfade;
        _theCharacter = taskableAnimal;
    }

    //public ITaskable TheCharacter { get => _theCharacter; set => _theCharacter = value; }
    //public string TargetNavTrans { get => _animaStateName; private set => _animaStateName = value; }

    public void RunME()
    {
        Debug.Log("run dwel anim");
        _theCharacter.Animate(_animaStateName);
    }
}
