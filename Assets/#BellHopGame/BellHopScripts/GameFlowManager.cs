#define DebugOn

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        _seqMNGR = GetComponent<SequenceManager>();
        BellHopGameEventManager.OnCurSequenceChanged += HeardSequenceChanged;
        BellHopGameEventManager.OnButtonPressed += FloorDestRequested;
        BellHopGameEventManager.OnDeliveryItemObtained += HeardDeliveryItemObtainedByBellhop;
    }

    private void OnDisable()
    {
        BellHopGameEventManager.OnCurSequenceChanged -= HeardSequenceChanged;
        BellHopGameEventManager.OnButtonPressed -= FloorDestRequested;
        BellHopGameEventManager.OnDeliveryItemObtained -= HeardDeliveryItemObtainedByBellhop;
    }

    void HeardDeliveryItemObtainedByBellhop(DeliveryItem argItem)
    {
        _sessionMNGR.CreateSessionWhenBellHopObtainsANewItem(argItem);

        //Request New Equation. it will be cashed in storytextgenerator
        BellHopGameEventManager.Instance.Call_NewTextThis(
             //  _StoryTextGen.RiddleMaker(argItem, _sessionMNGR.GetNumberOfWrongAnswersInThisSession())
             _StoryTextGen.RiddleMaker(argItem, 0) //zero wrong answers . this is the first time we request 
            );
    }

    SequenceManager _seqMNGR;

    int _requestedFloor;
    int _floorNumArrivedAt;

    void HeardSequenceChanged(GameEnums.GameSequenceType argGST)
    {
        switch (argGST)
        {
            case GameEnums.GameSequenceType.GameStart:

                _seqMNGR.gamestarted = true;
#if DebugOn
                print("startgame");
#endif
                FirstTime = false; //just kill the first time in chack floorupon arrival


                _sessionMNGR.AddFloorVisitFloorsVisitedINThisSession(_floorsmngr.Get_curFloor().FloorNumber);

                _curDweller = _floorsmngr.GetCurFloorDweller();
                _curDeliveryItem = _curDweller.GetMyItemManager().GetItem_LR(GameEnums.AnimalCharacterHands.Right);
                _ContextItem = _curDeliveryItem;
                _seqMNGR.InitAllPointsAccordingToCurFloor(_floorsmngr.Get_curFloor(), _bellHop, GameEnums.SequenceType.sq_FIRST, 0);
                BellHopGameEventManager.Instance.Call_NewTextThis("");
                //_cam.m_Text_Game.text = "Hello !";
                //_cam.m_Text_Game.text = _StoryTextGen.SimpleRiddle_takethisto(_ContextItem, _floorsmngr.Get_curFloor().FloorNumber, _sessionMNGR.GetFloorsVisitedINThisSession());

                _floorsmngr.HideShowAllBarriers(false);
                _curDweller.IsCurentFloorAnimal = true;
                _seqMNGR.StartSequence();


                break;

            case GameEnums.GameSequenceType.ReachedFloor:
#if DebugOn
                print("reachedfloor");
#endif
                _floorsmngr.HideShowAllBarriers(false);
                CheckFloorStatusUponArrival();
                break;

            case GameEnums.GameSequenceType.DoorsOppned:
#if DebugOn
                if (GameSettings.Instance.ShowDebugs)
                    print("doorsOpened");
#endif
                TextColor(cashedWrongAnswers);
                break;


            case GameEnums.GameSequenceType.FloorActionsFinished:
#if DebugOn
                print("action floor finished");
#endif
                break;

            case GameEnums.GameSequenceType.PlayerInputs:
                //    _ContextItem = _bellHop.Get_CurHeldObj();
                IsAllowKeypad = true;
                _cam.numkeypad.SetButtonColor(Color.green);

                break;

            case GameEnums.GameSequenceType.DoorsClosed:
#if DebugOn
                //  print("here");
#endif
                _curDweller.IsCurentFloorAnimal = false;
                _floorsmngr.UpdateCurFloorDest(_requestedFloor);
                //_cam.m_Text_Game.text = "";
                break;

            case GameEnums.GameSequenceType.GameEnd:

                SceneManager.LoadScene("DeliveryStart");
                break;


        }


    }
    void FloorDestRequested(int x)
    {
        IsAllowKeypad = false;
        _ElevatorDoors.CloseDoors();
        _requestedFloor = x;
        _cam.numkeypad.SetButtonColor(Color.red);

        x++;
        _cam.m_Text_Game.text += x.ToString();


        if (x > _floorsmngr.Get_curFloor().FloorNumber) { _cam.numkeypad.Set_GoingUP(); }
        else if (x < _floorsmngr.Get_curFloor().FloorNumber) { _cam.numkeypad.Set_GoingDown(); }
        else { _cam.numkeypad.Set_cleararrows(); }

    }

    #endregion


    bool _GOODFLOOR = false;
    bool FirstTime = true;
    int cashedWrongAnswers = 0;
    void CheckFloorStatusUponArrival()
    {

        _cam.numkeypad.SetFloorNumberOnDisplay(_floorsmngr.Get_curFloor().FloorNumber);
        _cam.numkeypad.Set_cleararrows();
        cashedWrongAnswers = _sessionMNGR.GetNumberOfWrongAnswersInThisSession();
        if (FirstTime)
        {
            Debug.Log("FIRST");


        }
        else
        {
            _bellHop.Warp(_BellhopPos.GetActionPos());
            _curDweller = _floorsmngr.GetCurFloorDweller();
            _curDeliveryItem = _bellHop.GetMyItemManager().GetItem_LR(GameEnums.AnimalCharacterHands.Right);
            _ContextItem = _curDeliveryItem;



            if (_ContextItem.IsMyOwner(_curDweller.GetComponent<DwellerMeshComposer>()))
            {
                if (_floorsmngr.Get_curFloor().FloorNumber == 0)
                {
                    _seqMNGR.InitAllPointsAccordingToCurFloor(_floorsmngr.Get_curFloor(), _bellHop, GameEnums.SequenceType.sq_GameOver, 0);
                }
                else
                {
                    _seqMNGR.InitAllPointsAccordingToCurFloor(_floorsmngr.Get_curFloor(), _bellHop, GameEnums.SequenceType.sq_correct, cashedWrongAnswers);
                }
            }
            else
            {
                _sessionMNGR.AddFloorVisitFloorsVisitedINThisSession(_floorsmngr.Get_curFloor().FloorNumber);
                _sessionMNGR.IncrementWrongAnswersForCurSession();
                _seqMNGR.InitAllPointsAccordingToCurFloor(_floorsmngr.Get_curFloor(), _bellHop, GameEnums.SequenceType.sq_wrong, cashedWrongAnswers);

                //reset the text to only the equation
                BellHopGameEventManager.Instance.Call_NewTextThis(_StoryTextGen.GetCashedEquation());

                //if (cashedWrongAnswers >= 1)
                //{
                // _StoryTextGen.RiddleMaker(_ContextItem, cashedWrongAnswers);
                //}
            }
            //  BellHopGameEventManager.Instance.Call_SimpleTaskEnded(); //this will kick in the first task
            // _ElevatorDoors.OpenDoors();
        }
        _curDweller.IsCurentFloorAnimal = true;

        _seqMNGR.StartSequence();

        // print("GOES TO " + _ContextItem.GetDestFloorDweller().AnimalName + " floor" + _ContextItem.GetDestFloorDweller().MyFinalResidenceFloorNumber);
    }
    void ThrowRoutine()
    {
        ///  _bellHop.Animateturn();
    }




    [SerializeField]
    AnimalCentralCommand _curDweller;
    [SerializeField]
    DeliveryItem _curDeliveryItem;
    [SerializeField]
    DeliveryItem _ContextItem;
    [SerializeField]
    AnimalCentralCommand _bellHop;

    HotelFloorsManager _floorsmngr;
    ElevatorDoorsMasterControl _ElevatorDoors;
    CameraPov _cam;

    InteractionCentral _BellhopPos;

    DeliverySessionManager _sessionMNGR;
    StoryTextGenerator _StoryTextGen;
    public void InitializeMyThings(AnimalCentralCommand argbh, HotelFloorsManager argfloors, CameraPov argCam, InteractionCentral argBellhopCocation, DeliverySessionManager argSessionMNGR, StoryTextGenerator argStoryTExtGen)

    {
        _sessionMNGR = argSessionMNGR;
        _sessionMNGR.Init();
        _BellhopPos = argBellhopCocation;
        _bellHop = argbh;
        _floorsmngr = argfloors;
        _cam = argCam;
        _ElevatorDoors = ElevatorDoorsMasterControl.Instance;

        _StoryTextGen = argStoryTExtGen;
        _StoryTextGen.InitMyRefs(_floorsmngr);
    }



    public static GameFlowManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);

        // IsAllowKeypad = true;

    }




    bool _gameEnded;
    public bool GameEnded
    {
        get => _gameEnded;
        private set => _gameEnded = value;
    }

    bool _isAllowKeypad;
    public bool IsAllowKeypad { get => _isAllowKeypad; private set => _isAllowKeypad = value; } //s the culprit


    Action<BellHopCharacter, DwellerMeshComposer> _ActionBunnyandDweller;

    void BunnyAndDwellerWave(BellHopCharacter bunny, DwellerMeshComposer bameobject) { }

    void ActionBunnyWaves() { }
    void ActionElevatorDoorsOpen() { }
    void ActionElevatorDoorsClose() { }
    void ActionAllowInput() { }


    public Vector3 GEt_DwellerPos() { return _floorsmngr.GetCurFloorDweller().transform.position; }


    public Vector3 GEt_BEllhopPos() { return _bellHop.transform.position; }


    void Start()
    {
        StartCoroutine(WaitTostartMovingCam());

    }

    IEnumerator WaitTostartMovingCam()
    {
        yield return new WaitForSeconds(1);
        _floorsmngr.INNNINNTNPOOOW();
        _bellHop.Warp(_BellhopPos.GetActionPos());
        _bellHop.transform.parent = _BellhopPos.transform;
        _floorsmngr.StartMovingCam();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("DeliveryGame");
        }
    }


    void TextColor(int argWrongs)
    {
        //(0.678f, 0.082f, 0.039f)
        //red set1 AD150A 

        //(0.851f, 0.8f, 0.216f)
        // yellow D9CC37

        // 7AFA92
        // green (0.478f, 0.98f, 0.573f)
        if (argWrongs == 0) { _cam.m_Text_Game.color = new Color(1.0f, 1.0f, 1.0f); }
        else
            if (argWrongs == 1) { _cam.m_Text_Game.color = new Color(0.478f, 0.98f, 0.573f); }
        else
            if (argWrongs == 2) { _cam.m_Text_Game.color = new Color(0.851f, 0.8f, 0.216f); }
        else
        { _cam.m_Text_Game.color = new Color(0.678f, 0.082f, 0.039f); }


    }

}



/*
  float speed = 2.0F;

    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    float journeyLength;
    float fracJourney;




    public void MoveTO(DeliveryItem argItem, Transform startMarker, Transform endMarker)
    {
        if (argItem.transform.parent != null)
        {
            argItem.transform.parent = null;
        }

        journeyLength = 10000000;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

        StartCoroutine(MoveCurItemRoutine(startMarker, endMarker, argItem));

    }






    private IEnumerator MoveCurItemRoutine(Transform startMarker, Transform endMarker, DeliveryItem argDeliveryItem)
    {

        float elapsedTime = 0;
        float timeTrigcatcher;
        bool catcherTriggered = false;
        float time;
        if (endMarker == null)
            yield return null;

        if (endMarker.gameObject.CompareTag("Player"))
        {
            time = 1.14f;
        }
        else
        {
            time = 2f;
        }
        //timeTrigcatcher = time * 0.8f;
        timeTrigcatcher = time - 0.16f;

        while (elapsedTime < time)
        {

            float distCovered = (Time.time - startTime) * speed;
            fracJourney = distCovered / journeyLength;

            if (elapsedTime <= timeTrigcatcher)
            {
                if (!catcherTriggered)
                {
                    Debug.Log("hey catch reflex NOW");
                    endMarker.gameObject.GetComponentInParent<ICharacterAnim>().AnimateCatch();
                    catcherTriggered = true;
                }
            }

            argDeliveryItem.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
            //Debug.Log(fracJourney);




            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("ReachedDestination");
        argDeliveryItem.transform.parent = endMarker;
    }

 */

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
/*
In C#, a benefits of enumerators is that the yield instruction makes the code block "pause", and restart from the exact same line when you want to get the next value.
IEnumerator Start ()
{
   Debug.Log ( "Let's wait 10 frames" );
   yield return WaitForFrames ( 10 );
   Debug.Log ( "Finished" );
}

YieldInstruction WaitForFrames ( int count )
{
   var routine = DoAsyncRoutine ( count );
   return StartCoroutine ( routine );
}

IEnumerator DoAsyncRoutine ( int count )
{
   var i = 0;
   while ( i < count )
   {
       yield return null;
   }

   yield break; // This instruction is useless in this context.
                // It just helps me illustrate.
}
This mecanism is very useful when you want to get access to a list of values, but you want to prevent this list to be modified. Just keep in mind that you have to create an accessor to get the number of values of your list.
class ReadOnlyListExample
{
   List<object> mValues;

   public ReadOnlyListExample ()
   {
       // mValues initialization with n values.
   }

   // Retrieves the number of available values.
   public int Count
   {
       get { return mValues.Count; }
   }

   // Retrieves the values, one by one.
   public IEnumerable<object> Values
   {
       get
       {
           for ( var i = 0; i < mValues.Count; i++ )
           {
               yield return mValues[i];
           }
       }
   }
}

Coroutines
Now that this little presentation is done, let's talk about these coroutines.
IEnumerator Start ()
{
   Debug.Log ( "Let's wait 10 frames" );
   yield return WaitForFrames ( 10 );
   Debug.Log ( "Finished" );
}

YieldInstruction WaitForFrames ( int count )
{
   var routine = DoAsyncRoutine ( count );
   return StartCoroutine ( routine );
}

IEnumerator DoAsyncRoutine ( int count )
{
   var i = 0;

   while ( i < count )
   {
       yield return null;
   }

   yield break; // This instruction is useless in this context.
                // It just helps me illustrate.
}

In this example, we wait n frames (10 here) before displaying a final message. Internally, Unity3D handles coroutines with several lists. When calling StartCoroutine(), the system creates a YieldInstruction object which references the specified enumerator. This YieldInstruction object is added to an execution list. This list is executed before the update of the components (Update()). The system asks each YieldInstruction object to get the next value of its own enumerator. The next step depends on the type of the value returned by the enumerator. Most of the time, this value is simply ignored, and lost, so you can't get it. So it is good to return null. (see yield return null in the DoAsyncRoutine() method). But some values make the system behave in different ways.
In my example, Start() returns an IEnumerator, so the Unity internals makes it called in a coroutine. Let's call this coroutine StartInstruction. In the enumerator of StartInstruction, I invoke yield return WaitForFrames (n). WaitForFrames(n) returns a YieldInstruction object, which I'll call WaitInstruction. Returning the values makes to StartInstruction stop until WaitInstruction is finished. The trick is simple. When the system gets the WaitInstruction from StartInstruction, it can observe that this value is a YieldInstruction object. It then remove StartInstruction from the execution list, and adds WaitInstruction. When WaitInstruction doesn't have any value to return anymore, or when the explicit yield break instruction is invoked, it removes WaitInstruction from the execution list, and put StartInstruction back. This is for sure not how Unity3D developers did it, but this version is easy to understand. Ultimately, while WaitInstruction returns values, StartInstruction is ignored. We so have three different behaviours when enumerator returns a value:
a YieldInstruction object notifies the system to make a special process
any other types, or NULL, which are ignored by the system.
yield break, which explicitly stops the enumerator, and so the associated coroutine.
By the way, there's different types of YieldInstructions, like WaitForSecond, WaitForFixedUpdate, … which just make the coroutine go from a list to another one, which is executed at a different step of the whole execution loop.
Make coroutines return a value
But if any returned values of the enumerator are ignored, how to make a coroutine actually return a value that I could reuse. The simplest solution is to use closures. The benefit of using coroutine is that you can write asynchronous code block inside procedural block. So, the main idea is not to inject a lambda with a complex code block, but try to make a result of the process readable from the code block which called the asynchronous block, through the lambda. This way, we keep the procedural look.
// My asynchronous process returning un boolean.
// Note that my process takes a lambda as parameter, which takes
// a boolean as parameter.
IEnumerator ProcessRoutine ( Action<bool> resultCB )
{
   // Makes the process asynchronous
   yield return null;

   // I create a result
   var result = true;

   if (resultCB != null)
   {
       // I give my result to my callback
       resultCB( result );
   }
}

YieldInstruction Process ( Action<bool> resultCB )
{
   // I give my callback to the process
   return StartCoroutine ( ProcessRoutine ( resultCB ) );
}

IEnumerator Start ()
{
   // I create a target variable which will keep the result of
   // my asynchronous process.
   bool result;

   // I give a lambda to my process.
   // This lambda just updates the local "result" variable
   // with the result of the process.
   yield return Process ( r => result = r );

   // Here I can use my result
   if ( result )
   {
       // …
   }
}

   */
