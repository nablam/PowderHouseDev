using UnityEngine;

public class TA_MoveOnTrig : ITaskAction

{
    ITaskable _theCharacter;

    float _timbeforeTrig;


    public TA_MoveOnTrig(ITaskable taskableAnimal, float argDelay)
    {
        TheCharacter = taskableAnimal;
        _timbeforeTrig = argDelay;

    }

    public ITaskable TheCharacter { get => _theCharacter; set => _theCharacter = value; }
    public float TimbeforeTrig { get => _timbeforeTrig; set => _timbeforeTrig = value; }

    public void RunME()
    {
        if (GameSettings.Instance.ShowDebugs)
            Debug.Log("moveon dude");
        _theCharacter.MoveOnTrigger(_timbeforeTrig); //<-------------------------------------------task kils itself after running the animatorcontroller NADA state
        _theCharacter.TaskEnded(); //<-------------------------------------------task kils itself ..hopefully in x seconds I ll be in a state where i can accept trigmoveon
    }
}

