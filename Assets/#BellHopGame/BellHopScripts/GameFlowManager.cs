using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    /// <summary>
    /// MAnages Actions flow
    /// can player press button
    /// 
    /// 
    /// </summary>
    /// 


    #region EventSubscription
    private void OnEnable()
    {
        BellHopGameEventManager.OnCurSequenceChanged += HeardSequenceChanged;
        BellHopGameEventManager.OnButtonPressed += FloorDestRequested;
    }

    private void OnDisable()
    {
        BellHopGameEventManager.OnCurSequenceChanged -= HeardSequenceChanged;
        BellHopGameEventManager.OnButtonPressed += FloorDestRequested;
    }

    void HeardSequenceChanged(GameEnums.GameSequenceType argGST)
    {
        switch (argGST)
        {
            case GameEnums.GameSequenceType.GameStart:

                break;

            case GameEnums.GameSequenceType.ReachedFloor:

                break;

            case GameEnums.GameSequenceType.DoorsOppned:
                IsAllowKeypad = true;
                break;
            case GameEnums.GameSequenceType.DwellerReactionFinished:
                break;
            case GameEnums.GameSequenceType.BunnyReleasedObject:
                break;
            case GameEnums.GameSequenceType.BunnyCaughtObject:
                break;
            case GameEnums.GameSequenceType.DwellerReleasedObject:
                break;
            case GameEnums.GameSequenceType.DwellerCaughtObject:
                break;
            case GameEnums.GameSequenceType.BunnyReaction:
                break;
            case GameEnums.GameSequenceType.DoorsClosed:
                break;

            case GameEnums.GameSequenceType.GameEnd:
                break;


        }


    }
    void FloorDestRequested(int x)
    {
        IsAllowKeypad = false;
    }
    #endregion


    public static GameFlowManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);

        IsAllowKeypad = true;

    }




    bool _gameEnded;
    bool _isAllowKeypad;

    public bool GameEnded
    {
        get => _gameEnded;
        private set => _gameEnded = value;
    }
    public bool IsAllowKeypad { get => _isAllowKeypad; private set => _isAllowKeypad = value; }
}


/*  
 
    You can use this to get a name of any provided member:

public static class MemberInfoGetting
{
    public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
    {
        MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
        return expressionBody.Member.Name;
    }
}
To get name of a variable:

string testVariable = "value";
string nameOfTestVariable = MemberInfoGetting.GetMemberName(() => testVariable);
To get name of a parameter:

public class TestClass
{
    public void TestMethod(string param1, string param2)
    {
        string nameOfParam1 = MemberInfoGetting.GetMemberName(() => param1);
    }
}
C# 6.0 and higher solution
You can use the nameof operator for parameters, variables and properties alike:

string testVariable = "value";
string nameOfTestVariable = nameof(testVariable);



 */
