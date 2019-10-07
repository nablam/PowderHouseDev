using System;
using System.Collections;
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

    int _requestedFloor;
    int _floorNumArrivedAt;
    bool firstTime = true;
    void HeardSequenceChanged(GameEnums.GameSequenceType argGST)
    {
        switch (argGST)
        {
            case GameEnums.GameSequenceType.GameStart:
                _floorsmngr.HideShowAllBarriers(false);
                print("here");
                _curDweller = _floorsmngr.GetCurFloorDweller();
                _curDeliveryItem = _curDweller.HELP_firstGuyOut();
                _ContextItem = _curDeliveryItem;
                //then setup context later when bunny owns it
                _ElevatorDoors.OpenDoors();
                IsAllowKeypad = true;
                CheckFloorStatusUponArrival();
                break;

            case GameEnums.GameSequenceType.ReachedFloor:
                CheckFloorStatusUponArrival();
                _ElevatorDoors.OpenDoors();
                break;

            case GameEnums.GameSequenceType.DoorsOppned:
                //moveme down 
                // _curDweller.AnimTrigger(GameEnums.DwellerAnimTrigger.TrigHello);
                //_bellHop.AnimTrigger(GameEnums.DwellerAnimTrigger.TrigWave1);
                //_bellHop.Animateturn();
                //if (firstTime)
                //{
                //    _ContextItem = _curDeliveryItem;
                //    firstTime = false;
                //    // _curDweller.AnimateToss();

                //    //if (_bellHop.GetMyRightHandHold().GetChild(0) == null)
                //    //{


                //    //}
                //}
                //else
                //{
                //    if (_GOODFLOOR)
                //    {
                //        _bellHop.AnimateToss();
                //    }
                //    else
                //    {

                //    }


                //}



                break;


            case GameEnums.GameSequenceType.FloorActionsFinished:
                break;



            case GameEnums.GameSequenceType.PlayerInputs:
                break;
            //case GameEnums.GameSequenceType.DwellerReleaseObject:
            //    _curDweller.ReleaseObj_CalledExternally();
            //    MoveTO(_curDeliveryItem, _curDweller.GetMyRightHandHold(), _bellHop.GetMyRightHandHold());
            //    //_curDeliveryItem
            //    //_curDeliveryItem.transform.parent = null;
            //    break;

            case GameEnums.GameSequenceType.DoorsClosed:
                _bellHop.Animateturn();
                _floorsmngr.UpdateCurFloorDest(_requestedFloor);
                break;

            case GameEnums.GameSequenceType.GameEnd:
                break;


        }


    }
    void FloorDestRequested(int x)
    {
        IsAllowKeypad = false;
        _ElevatorDoors.CloseDoors();
        _requestedFloor = x;

    }
    #endregion
    bool _GOODFLOOR = false;
    //is called when 
    void CheckFloorStatusUponArrival()
    {
        if (_ContextItem.IsMyOwner(_curDweller))
        {
            Debug.Log("YAYA");
            _GOODFLOOR = true;

            _NamedActionsController.ReInitContextObjectsOnArrivalTOFloor(_bellHop, _curDweller, _ContextItem);
        }
        else
        {
            Debug.Log("nay");
            _GOODFLOOR = false;
            _NamedActionsController.ReInitContextObjectsOnArrivalTOFloor(_bellHop, _curDweller, _ContextItem);
        }


        //_curDeliveryItem = null;

        //if (_curDeliveryItem.IsMyOwner(_curDweller))
        //{
        //    Debug.Log("WOOT");

        //    if (_bellHop.IsEmptyHAnded())
        //    {
        //        Debug.Log("bunny empty handed");
        //    }
        //    else
        //    {
        //        Debug.Log("bunny full handed");
        //    }

        //}
        //else
        //{
        //    Debug.Log("original floor item not here");
        //}
    }
    void ThrowRoutine()
    {
        _bellHop.Animateturn();
        //_curDweller.AnimateToss();

    }

    [SerializeField]
    DwellerMeshComposer _curDweller;
    [SerializeField]
    DeliveryItem _curDeliveryItem;
    DeliveryItem _ContextItem;
    [SerializeField]
    BellHopCharacter _bellHop;

    HotelFloorsManager _floorsmngr;
    ElevatorDoorsMasterControl _ElevatorDoors;
    CameraPov _cam;
    NamedActionsController _NamedActionsController;

    public void InitializeMyThings(BellHopCharacter argbh, HotelFloorsManager argfloors, CameraPov argCam, NamedActionsController argNameActionCTRL)
    {

        _bellHop = argbh;
        _floorsmngr = argfloors;
        _cam = argCam;
        _ElevatorDoors = ElevatorDoorsMasterControl.Instance;
        _NamedActionsController = argNameActionCTRL;
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

        IsAllowKeypad = true;

    }




    bool _gameEnded;
    public bool GameEnded
    {
        get => _gameEnded;
        private set => _gameEnded = value;
    }

    bool _isAllowKeypad;
    public bool IsAllowKeypad { get => _isAllowKeypad; private set => _isAllowKeypad = value; }


    Action<BellHopCharacter, DwellerMeshComposer> _ActionBunnyandDweller;

    void BunnyAndDwellerWave(BellHopCharacter bunny, DwellerMeshComposer bameobject) { }

    void ActionBunnyWaves() { }
    void ActionElevatorDoorsOpen() { }
    void ActionElevatorDoorsClose() { }
    void ActionAllowInput() { }


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
