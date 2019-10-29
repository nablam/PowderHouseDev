using UnityEngine;
using static GameEnums;

public interface ITaskable
{
    void GoTo(Transform argTransNav);
    void Face(Transform argTransLookAt);
    void Animate(string argAnimStateName);
    // void Pull_FromTheirRight_ToMyRight(CharacterItemManager argOtherCharItemManager, AnimalCharacterHands argToMyHand);
    void Pull_Coordinate(ITaskable argOther, AnimalCharacterHands argFromTheirMyHand, AnimalCharacterHands argToMyHand);
    void Warp(Transform argTransWarp);


    CharacterItemManager GetMyItemManager();
    void MoveOnTrigger(float sec);
    void TaskEnded();
    void ActivateAgent();
}
