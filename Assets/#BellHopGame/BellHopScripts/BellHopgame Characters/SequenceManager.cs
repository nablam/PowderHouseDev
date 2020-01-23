using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class SequenceManager : MonoBehaviour
{
    GameSettings _gs;
    [SerializeField]
    public AnimalCentralCommand _Bellhop;
    public AnimalCentralCommand _Dweller;
    public DeliveryItem _theItem;

    InteractionCentral _exhangeI;
    InteractionCentral _danceI;
    InteractionCentral _spawnI;
    Transform _outDoorsPlace;
    InteractionCentral _bedI;
    InteractionCentral _mainRoomActionI;
    InteractionCentral _BunnyPos;







    TA_DwellerWarp W_D_ExchandePos;
    TA_DwellerWarp W_D_MAinRoomActionPos;
    TA_DwellerWarp W_D_DancePos;

    TA_DwellerWarp W_D_SpawnPos;

    TA_DwellerMoveTo M_D_ExchandePos;
    TA_DwellerMoveTo M_D_MainRoomActionPos;
    TA_DwellerMoveTo M_D_DancePos;

    TA_DwellerMoveTo M_D_Outdoors;

    TA_DwellerFace F_D_Cam;
    TA_DwellerFace F_D_Bell;
    TA_DwellerFace F_D_MainRoomActionLookat;
    TA_DwellerFace F_B_Dweller;
    TA_DwellerFace F_B_Cam;

    TA_DwellerFace F_D_SpawnLookat;


    TA_DwellerAnimate A_D_Wave1;
    TA_DwellerAnimate A_D_Wave2;
    TA_DwellerAnimate A_D_Hello;
    TA_DwellerAnimate A_D_No;
    TA_DwellerAnimate A_D_Good;
    TA_DwellerAnimate A_B_Good;
    TA_DwellerAnimate A_D_MainRoomAction;
    TA_DwellerAnimate A_D_Toss;
    //TA_DwellerAnimate A_D_Catch1;//NOT NEEDED , the coordinator runs it
    TA_DwellerAnimate A_B_Toss;
    TA_DwellerPullCoord_2L P_D_2L;
    TA_DwellerPullCoord_2R P_B_2R;

    TA_InstantTaskHandShowHide I_D_showRight;
    TA_InstantTaskHandShowHide I_D_HideLeft;
    TA_TimedOpenDoor O_DoorsNow;
    TA_MoveOnTrig T_moveOn;
    TA_GameOver T_GG;

    List<ITaskAction> Sequence_SimpleGreet;


    List<ITaskAction> Sequence_Exc_DwellerToss1way_end;

    List<ITaskAction> Sequence_U_Long_goodfloor;
    List<ITaskAction> Sequence_U_short_goodfloor;
    List<ITaskAction> Sequence_U_goodfloor_fromSpaw;

    List<ITaskAction> Sequence_WrongFloor_Short;

    List<ITaskAction> Sequence_WrongFloorLONG;
    List<ITaskAction> Sequence_U_WrongFloorLONG_walk;
    ITaskAction _DwellerAnimate_NO;

    List<ITaskAction> Sequence_U_FIRST;





    List<ITaskAction> Sequence_U_GameOver; //also is the endcallller

    List<ITaskAction> Sequence_DwellerPulls;

    BHG_TaskSystem TaskSys;

    //these 2 will point to different task lists depending on on wrong answers
    List<ITaskAction> REF_correct;
    List<ITaskAction> REF_Wrong;

    bool sequencStarted = false;
    private void OnEnable()
    {
        BellHopGameEventManager.OnSimpleTaskEnded += HeardTaskEnded;
    }

    private void OnDisable()
    {
        BellHopGameEventManager.OnSimpleTaskEnded -= HeardTaskEnded;
    }
    public bool gamestarted = false;
    void HeardTaskEnded()
    {
        if (!gamestarted) return; //make listen to state change instead
        ITaskAction task = TaskSys.RequestNextTask();
        if (task != null)
            task.RunME();
        else
        {//no more tasks 
            if (sequencStarted)
            {

                BellHopGameEventManager.Instance.Call_CurSequenceChanged(GameSequenceType.PlayerInputs);
            }


            sequencStarted = false;
        }
    }

    public void StartSequence()
    {
        sequencStarted = true;

        HeardTaskEnded();
    }

    public void InitAllPointsAccordingToCurFloor(HotelFloor argHF, AnimalCentralCommand argBEllHop, SequenceType argSequenceType, int argWrongAnswers)
    {
        _Bellhop = argBEllHop;
        _Dweller = argHF.FloorDweller;
        _exhangeI = argHF.Greetings_HF;
        _danceI = argHF.Dance_HF;
        _spawnI = argHF.SpawnPoint_HF;
        _mainRoomActionI = argHF.Mainaction_HF;
        _outDoorsPlace = argHF.OutDoorsPoint.transform;

        _Bellhop.ActivateAgent();
        //_Dweller.ActivateAgent();

        Make_newSystem(argSequenceType, argWrongAnswers);
    }


    int cntx = 0;
    void Make_newSystem(SequenceType argSequenceType, int argwrons)
    {
        BellHopGameEventManager.Instance.Call_DebugThis("w=" + argwrons.ToString() + " " + argSequenceType.ToString());


        TaskSys = new BHG_TaskSystem();
        _gs = GameSettings.Instance;

        W_D_ExchandePos = new TA_DwellerWarp(_Dweller, _exhangeI.GetActionPos());
        W_D_MAinRoomActionPos = new TA_DwellerWarp(_Dweller, _mainRoomActionI.GetActionPos());
        W_D_DancePos = new TA_DwellerWarp(_Dweller, _danceI.GetActionPos());
        W_D_SpawnPos = new TA_DwellerWarp(_Dweller, _spawnI.GetActionPos());

        M_D_ExchandePos = new TA_DwellerMoveTo(_Dweller, _exhangeI.GetActionPos());
        M_D_MainRoomActionPos = new TA_DwellerMoveTo(_Dweller, _mainRoomActionI.GetActionPos());
        M_D_DancePos = new TA_DwellerMoveTo(_Dweller, _danceI.GetActionPos());
        M_D_Outdoors = new TA_DwellerMoveTo(_Dweller, _outDoorsPlace);
        F_D_Cam = new TA_DwellerFace(_Dweller, Camera.main.transform);
        F_B_Cam = new TA_DwellerFace(_Bellhop, Camera.main.transform);
        F_D_Bell = new TA_DwellerFace(_Dweller, _Bellhop.transform);
        F_D_MainRoomActionLookat = new TA_DwellerFace(_Dweller, _mainRoomActionI.GetLookTarg());
        F_B_Dweller = new TA_DwellerFace(_Bellhop, _Dweller.transform);
        F_D_SpawnLookat = new TA_DwellerFace(_Dweller, _spawnI.GetLookTarg());
        //dweller pulls to his left AKA good delivery 
        P_B_2R = new TA_DwellerPullCoord_2R(_Bellhop, _Dweller);
        P_D_2L = new TA_DwellerPullCoord_2L(_Dweller, _Bellhop);


        A_D_Wave1 = new TA_DwellerAnimate(_Dweller, _gs.Wave1);
        A_D_Wave2 = new TA_DwellerAnimate(_Dweller, _gs.Wave2);
        A_D_Hello = new TA_DwellerAnimate(_Dweller, _gs.Hello);
        A_D_No = new TA_DwellerAnimate(_Dweller, _gs.No);
        A_D_Good = new TA_DwellerAnimate(_Dweller, _gs.Good);
        A_B_Good = new TA_DwellerAnimate(_Bellhop, _gs.Good);
        A_D_MainRoomAction = new TA_DwellerAnimate(_Dweller, _mainRoomActionI.argActionString);
        A_D_Toss = new TA_DwellerAnimate(_Dweller, _gs.Toss);
        A_B_Toss = new TA_DwellerAnimate(_Bellhop, _gs.Toss);
        I_D_showRight = new TA_InstantTaskHandShowHide(_Dweller, GameEnums.AnimalCharacterHands.Right, true);
        I_D_HideLeft = new TA_InstantTaskHandShowHide(_Dweller, GameEnums.AnimalCharacterHands.Left, false);

        F_B_Cam = new TA_DwellerFace(_Bellhop, Camera.main.transform);

        O_DoorsNow = new TA_TimedOpenDoor();
        T_moveOn = new TA_MoveOnTrig(_Dweller, 4f);
        T_GG = new TA_GameOver();
        Sequence_SimpleGreet = new List<ITaskAction>
        {
            W_D_ExchandePos,
            M_D_ExchandePos,
            I_D_showRight,
            F_D_Bell,
            F_B_Dweller,
            A_D_Toss,
            P_B_2R,
            M_D_DancePos,
            F_D_Cam,
            F_D_Bell,
            F_B_Dweller,
            A_B_Toss,
            P_D_2L,
            F_B_Cam
        };


        Sequence_U_FIRST = new List<ITaskAction>   {
            W_D_MAinRoomActionPos,
            T_moveOn,
            F_D_MainRoomActionLookat,
            O_DoorsNow,
            A_D_MainRoomAction,
            F_D_Bell,
            M_D_ExchandePos,

            I_D_showRight,
            F_D_Bell,
            F_B_Dweller,
            A_D_Toss, //needed for pull mirorred character
            P_B_2R,
            F_D_Cam,
            F_B_Cam,
        };
        Sequence_U_Long_goodfloor = new List<ITaskAction>   {
          W_D_MAinRoomActionPos,
          T_moveOn,
          F_D_MainRoomActionLookat,
          O_DoorsNow,
          A_D_MainRoomAction,
          F_D_Bell,
          M_D_ExchandePos,
          I_D_showRight,
            F_B_Dweller,
            F_D_Bell,
            A_D_Hello,
            A_B_Toss, //needed for pull mirorred character
            P_D_2L,
            A_D_Good,
            I_D_HideLeft,
            I_D_showRight,
            A_D_Toss,
            P_B_2R,
            F_B_Cam,


        };

        Sequence_U_short_goodfloor = new List<ITaskAction>   {
            W_D_ExchandePos,
            I_D_showRight,
            O_DoorsNow,
            F_B_Dweller,
            F_D_Bell,
            M_D_ExchandePos,
            F_B_Dweller,
            F_D_Bell,
            A_B_Toss, //needed for pull mirorred character
            P_D_2L,
            A_D_Good,
            I_D_HideLeft,
            I_D_showRight,
            A_D_Toss,
            P_B_2R,
            F_B_Cam,


        };


        Sequence_U_goodfloor_fromSpaw = new List<ITaskAction>   {
                    W_D_SpawnPos,
            F_D_SpawnLookat,
           I_D_showRight,
            O_DoorsNow,
             M_D_ExchandePos,

            F_B_Dweller,
            F_D_Bell,
            A_D_Hello,
            A_B_Toss, //needed for pull mirorred character
            P_D_2L,
            A_D_Good,
            I_D_HideLeft,
            I_D_showRight,
            A_D_Toss,
            P_B_2R,
            F_B_Cam,


        };

        Sequence_U_GameOver = new List<ITaskAction>   {
            W_D_MAinRoomActionPos,
            T_moveOn,
            F_D_MainRoomActionLookat,
              O_DoorsNow,
            A_D_MainRoomAction, //is running right after roomlookat , and hopes to get exited on time by O_DOOrnow
            F_D_Bell,
            A_D_Hello,
            A_B_Toss, //needed for pull mirorred character
            
            A_D_Good,
            A_B_Good,
            I_D_HideLeft,



            F_B_Cam,
            T_GG,

        };

        Sequence_U_WrongFloorLONG_walk = new List<ITaskAction>   {
            W_D_SpawnPos,
            F_D_SpawnLookat,
            O_DoorsNow,
            M_D_Outdoors,
        //    M_D_ExchandePos,
            F_B_Dweller,
            F_D_Bell,
            A_D_No,

            F_B_Cam,
             M_D_ExchandePos,
             F_D_Cam,
        };


        Sequence_Exc_DwellerToss1way_end = new List<ITaskAction>   {
            W_D_ExchandePos,
            O_DoorsNow,
            M_D_ExchandePos,
            F_D_Bell,
            F_B_Dweller,
            I_D_showRight,
            A_D_Toss, //needed for pull mirorred character
            P_B_2R,
            F_D_Cam,
            F_B_Cam,
        };
        Sequence_WrongFloor_Short = new List<ITaskAction>   {
            W_D_ExchandePos,

            O_DoorsNow,
            F_B_Dweller,
            F_D_Bell,
            A_D_No,

            F_B_Cam,

        };
        Sequence_WrongFloorLONG = new List<ITaskAction>   {
            //W_D_SpawnPos,
            //F_D_SpawnLookat,

            //O_DoorsNow,
            //M_D_ExchandePos,
            //F_B_Dweller,
            //F_D_Bell,
            //A_D_No,

            F_B_Cam,

        };




        if (argwrons == 0)
        {
            REF_correct = Sequence_U_short_goodfloor;
            REF_Wrong = Sequence_WrongFloor_Short;
        }
        else

        if (argwrons == 1)
        {
            REF_correct = Sequence_U_Long_goodfloor;
            REF_Wrong = Sequence_U_WrongFloorLONG_walk;
        }
        else
        {
            REF_correct = Sequence_U_goodfloor_fromSpaw;
            REF_Wrong = Sequence_U_WrongFloorLONG_walk;
        }











        if (argSequenceType == SequenceType.sq_FIRST)
        {
            Setup_Tasksystem(Sequence_U_FIRST);

        }
        else

             if (argSequenceType == SequenceType.sq_correct)
        {
            Setup_Tasksystem(REF_correct);
        }
        else
             if (argSequenceType == SequenceType.sq_GameOver)
        {
            Setup_Tasksystem(Sequence_U_GameOver);
        }
        else

        if (argSequenceType == SequenceType.sq_wrong)
        {
            Setup_Tasksystem(REF_Wrong);
        }
        else
        {
            Setup_Tasksystem(Sequence_WrongFloor_Short);
        }




        CharacterItemManager cim = _Dweller.GetComponent<CharacterItemManager>();
        if (cim != null)
        {
            cim.Show_LR(false, GameEnums.AnimalCharacterHands.Left);
        }
        else
            Debug.LogError("NO ItemMAnager ");

    }

    private void Start()
    {





    }


    void Setup_Tasksystem(List<ITaskAction> argListTasks)
    {

        foreach (ITaskAction it in argListTasks)
        {
            TaskSys.AddTask(it);
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        HeardTaskEnded();
    //    }
    //}
}
