//#define  ENABLE_DEBUGLOG
using System;

public class BHGA_Base : IBellHopGameAction, IDisposable
{
    GameEnums.GameActionsType _myActionType;
    Action _Action;

    public BHGA_Base(Action Action, GameEnums.GameActionsType argmyActionType)
    {
        _Action = Action;
        _myActionType = argmyActionType;
    }
    ~BHGA_Base()
    {
#if ENABLE_DEBUGLOG
        // Debug.Log("eb_base Destructor " + _myActionType.ToString());
#endif
        Dispose();
    }

    public void Dispose()
    {
#if ENABLE_DEBUGLOG
        //  Debug.Log("dispose base");
#endif
        GC.SuppressFinalize(this);
    }



    public GameEnums.GameActionsType Get_MyActionType()
    {
        return this._myActionType;
    }



    public virtual void StartGameAction()
    {
        throw new System.NotImplementedException();
    }

    public virtual void RunGameAction()
    {
        throw new System.NotImplementedException();
    }

    public virtual void EndGameAction()
    {
        throw new System.NotImplementedException();
    }

    public virtual void CompletedAnim(int argCombatanim)
    {
        // throw new System.NotImplementedException();
    }

}
