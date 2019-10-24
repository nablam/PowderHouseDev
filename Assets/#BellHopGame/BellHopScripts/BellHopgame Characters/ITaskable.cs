using UnityEngine;
using static GameEnums;

public interface ITaskable
{
    void GoTo(Transform argTransNav);
    void Face(Transform argTransLookAt);
    void Animate(string argAnimStateName);
    void Pull(CharacterItemManager argOtherCharItemManager, AnimalCharacterHands argToMyHand);
    void Warp(Transform argTransWarp);

    void TaskEnded();
}
