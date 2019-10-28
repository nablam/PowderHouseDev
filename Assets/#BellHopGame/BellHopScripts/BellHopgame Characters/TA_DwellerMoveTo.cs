using UnityEngine;

public class TA_DwellerMoveTo : ITaskAction
{
    ITaskable _theCharacter;
    Transform _taretNav;
    public TA_DwellerMoveTo(ITaskable taskableAnimal, Transform argNaveTarg)
    {
        TargetNavTrans = argNaveTarg;
        TheCharacter = taskableAnimal;
    }

    public ITaskable TheCharacter { get => _theCharacter; set => _theCharacter = value; }
    public Transform TargetNavTrans { get => _taretNav; private set => _taretNav = value; }

    public void RunME()
    {
        if (GameSettings.Instance.ShowDebugs)
            Debug.Log("run dwel move");
        _theCharacter.GoTo(TargetNavTrans);
    }
}
