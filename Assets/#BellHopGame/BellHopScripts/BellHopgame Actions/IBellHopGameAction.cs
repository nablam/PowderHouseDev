public interface IBellHopGameAction
{
    GameEnums.GameActionsType Get_MyActionType();
    void StartGameAction();
    void RunGameAction();
    void EndGameAction();
    void CompletedAnim(int argCombatanim);
}
