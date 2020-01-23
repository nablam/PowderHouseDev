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


    public void RunME()
    {
        if (GameSettings.Instance.ShowDebugs)
            Debug.Log("run dwel anim" + _animaStateName);
        _theCharacter.Animate(_animaStateName);
    }
}
