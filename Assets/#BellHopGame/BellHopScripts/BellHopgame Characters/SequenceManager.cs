﻿using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class SequenceManager : MonoBehaviour
{
    GameSettings _gs;
    [SerializeField]
    public AnimalCentralCommand _Bellhop;
    public AnimalCentralCommand _Dweller;
    public DeliveryItem _theItem;

    public InteractionCentral _exhangeI;
    public InteractionCentral _danceI;
    public Transform _curCamPlace;
    public InteractionCentral _bedI;
    public InteractionCentral _couchI;
    public InteractionCentral _BunnyPos;






    TA_DwellerWarp W_D_ExchandePos;
    TA_DwellerWarp W_D_RoomPos;
    TA_DwellerWarp W_D_DancePos;

    TA_DwellerMoveTo M_D_ExchandePos;
    TA_DwellerMoveTo M_D_RoomPos;
    TA_DwellerMoveTo M_D_DancePos;

    TA_DwellerFace F_D_Cam;
    TA_DwellerFace F_D_Bell;
    TA_DwellerFace F_D_RoomLookat;
    TA_DwellerFace F_B_Dweller;
    TA_DwellerFace F_B_Cam;

    TA_DwellerAnimate A_D_Wave1;
    TA_DwellerAnimate A_D_Wave2;
    TA_DwellerAnimate A_D_Hello;
    TA_DwellerAnimate A_D_No;
    TA_DwellerAnimate A_D_Good;
    TA_DwellerAnimate A_B_Good;
    TA_DwellerAnimate A_D_RoomAction;
    TA_DwellerAnimate A_D_Toss;
    //TA_DwellerAnimate A_D_Catch1;//NOT NEEDED , the coordinator runs it
    TA_DwellerAnimate A_B_Toss;
    TA_DwellerPullCoord_2L P_D_2L;
    TA_DwellerPullCoord_2R P_B_2R;

    TA_InstantTaskHandShowHide I_D_showRight;
    TA_InstantTaskHandShowHide I_D_HideLeft;


    List<ITaskAction> Sequence_SimpleGreet;


    List<ITaskAction> Sequence_ShortDwellerToss;

    List<ITaskAction> Sequence_Short_goodfloor;


    List<ITaskAction> Sequence_WrongFloor;
    ITaskAction _DwellerAnimate_NO;

    List<ITaskAction> Sequence_MovesAndAnims;

    List<ITaskAction> Sequence_BellPulls;

    List<ITaskAction> Sequence_DwellerPulls;

    BHG_TaskSystem TaskSys;

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

    public void InitAllPointsAccordingToCurFloor(HotelFloor argHF, AnimalCentralCommand argBEllHop, SequenceType argSequenceType)
    {
        _Bellhop = argBEllHop;
        _Dweller = argHF.FloorDweller;
        _exhangeI = argHF.Greetings;
        _danceI = argHF.Dance;
        _couchI = argHF.Mainaction;


        _Bellhop.ActivateAgent();
        //_Dweller.ActivateAgent();

        Make_newSystem(argSequenceType);
    }


    int cntx = 0;
    void Make_newSystem(SequenceType argSequenceType)
    {



        TaskSys = new BHG_TaskSystem();
        _gs = GameSettings.Instance;

        W_D_ExchandePos = new TA_DwellerWarp(_Dweller, _exhangeI.GetActionPos());
        W_D_RoomPos = new TA_DwellerWarp(_Dweller, _couchI.GetActionPos());
        W_D_DancePos = new TA_DwellerWarp(_Dweller, _danceI.GetActionPos());


        M_D_ExchandePos = new TA_DwellerMoveTo(_Dweller, _exhangeI.GetActionPos());
        M_D_RoomPos = new TA_DwellerMoveTo(_Dweller, _couchI.GetActionPos());
        M_D_DancePos = new TA_DwellerMoveTo(_Dweller, _danceI.GetActionPos());


        F_D_Cam = new TA_DwellerFace(_Dweller, Camera.main.transform);
        F_B_Cam = new TA_DwellerFace(_Bellhop, Camera.main.transform);
        F_D_Bell = new TA_DwellerFace(_Dweller, _Bellhop.transform);
        F_D_RoomLookat = new TA_DwellerFace(_Dweller, _couchI.GetLookTarg());
        F_B_Dweller = new TA_DwellerFace(_Bellhop, _Dweller.transform);

        //dweller pulls to his left AKA good delivery 
        P_B_2R = new TA_DwellerPullCoord_2R(_Bellhop, _Dweller);
        P_D_2L = new TA_DwellerPullCoord_2L(_Dweller, _Bellhop);


        A_D_Wave1 = new TA_DwellerAnimate(_Dweller, _gs.Wave1);
        A_D_Wave2 = new TA_DwellerAnimate(_Dweller, _gs.Wave2);
        A_D_Hello = new TA_DwellerAnimate(_Dweller, _gs.Hello);
        A_D_No = new TA_DwellerAnimate(_Dweller, _gs.No);
        A_D_Good = new TA_DwellerAnimate(_Dweller, _gs.Good);
        A_B_Good = new TA_DwellerAnimate(_Bellhop, _gs.Good);
        A_D_RoomAction = new TA_DwellerAnimate(_Dweller, _couchI.argActionString);
        A_D_Toss = new TA_DwellerAnimate(_Dweller, _gs.Toss);
        A_B_Toss = new TA_DwellerAnimate(_Bellhop, _gs.Toss);
        I_D_showRight = new TA_InstantTaskHandShowHide(_Dweller, GameEnums.AnimalCharacterHands.Right, true);
        I_D_HideLeft = new TA_InstantTaskHandShowHide(_Dweller, GameEnums.AnimalCharacterHands.Left, false);

        F_B_Cam = new TA_DwellerFace(_Bellhop, Camera.main.transform);

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

        Sequence_ShortDwellerToss = new List<ITaskAction>   {
            W_D_ExchandePos,
                M_D_ExchandePos,
            F_B_Dweller,
            I_D_showRight,
            F_D_Bell,
            A_D_Toss, //needed for pull mirorred character
            P_B_2R,
            F_D_Cam,
            F_B_Cam,
        };

        Sequence_Short_goodfloor = new List<ITaskAction>   {
            W_D_DancePos,
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

        Sequence_WrongFloor = new List<ITaskAction>   {
            W_D_ExchandePos,

            F_B_Dweller,
            F_D_Bell,
            A_D_No,

            F_B_Cam,

        };


        if (argSequenceType == SequenceType.DwellerToss_short)
        {
            Setup_Tasksystem(Sequence_ShortDwellerToss);

        }
        else
             if (argSequenceType == SequenceType.GoodFloor_short)
        {
            Setup_Tasksystem(Sequence_Short_goodfloor);
        }
        else
        {
            Setup_Tasksystem(Sequence_WrongFloor);
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
