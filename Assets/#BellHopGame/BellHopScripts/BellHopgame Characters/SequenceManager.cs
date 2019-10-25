using System.Collections.Generic;
using UnityEngine;

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






    TA_DwellerWarp W_D_ExchandePos;
    TA_DwellerWarp W_D_RoomPos;
    TA_DwellerWarp W_D_DancePos;

    TA_DwellerMoveTo M_D_ExchandePos;
    TA_DwellerMoveTo M_D_RoomPos;
    TA_DwellerMoveTo M_D_DancePos;

    TA_DwellerFace F_D_Camera;
    TA_DwellerFace F_D_Bell;
    TA_DwellerFace F_D_RoomLookat;
    TA_DwellerFace F_B_Dweller;

    TA_DwellerAnimate A_D_Wave1;
    TA_DwellerAnimate A_D_Wave2;
    TA_DwellerAnimate A_D_Hello;
    TA_DwellerAnimate A_D_No;
    TA_DwellerAnimate A_D_Good;
    TA_DwellerAnimate A_D_RoomAction;
    TA_DwellerAnimate A_D_Toss;
    //TA_DwellerAnimate A_D_Catch1;//NOT NEEDED , the coordinator runs it
    TA_DwellerAnimate A_B_Toss;
    TA_DwellerPullCoord_2L P_D_2L;
    TA_DwellerPullCoord_2R P_B_2R;



    List<ITaskAction> Sequence_SimpleGreet;




    List<ITaskAction> Sequence_Greet_fromRomm;


    List<ITaskAction> Sequence_WrongFloor;
    ITaskAction _DwellerAnimate_NO;

    List<ITaskAction> Sequence_MovesAndAnims;

    List<ITaskAction> Sequence_BellPulls;

    List<ITaskAction> Sequence_DwellerPulls;

    BHG_TaskSystem TaskSys;

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
    }

    public void InitAllPointsAccordingToCurFloor(HotelFloor argHF)
    {
        _Dweller = argHF.FloorDweller;
        _exhangeI = argHF.Greetings;
        _danceI = argHF.Dance;
        _couchI = argHF.Mainaction;
        Make_newSystem();
    }

    void Make_newSystem()
    {
        _Bellhop.ActivateAgent();
        _Dweller.ActivateAgent();


        TaskSys = new BHG_TaskSystem();
        _gs = GameSettings.Instance;
        Sequence_SimpleGreet = new List<ITaskAction>();
        W_D_ExchandePos = new TA_DwellerWarp(_Dweller, _exhangeI.GetActionPos());
        W_D_RoomPos = new TA_DwellerWarp(_Dweller, _couchI.GetActionPos());
        W_D_DancePos = new TA_DwellerWarp(_Dweller, _danceI.GetActionPos());


        M_D_ExchandePos = new TA_DwellerMoveTo(_Dweller, _exhangeI.GetActionPos());
        M_D_RoomPos = new TA_DwellerMoveTo(_Dweller, _couchI.GetActionPos());
        M_D_DancePos = new TA_DwellerMoveTo(_Dweller, _danceI.GetActionPos());


        F_D_Camera = new TA_DwellerFace(_Dweller, Camera.main.transform);
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
        A_D_RoomAction = new TA_DwellerAnimate(_Dweller, _couchI.argActionString);
        A_D_Toss = new TA_DwellerAnimate(_Dweller, _gs.Toss);
        A_B_Toss = new TA_DwellerAnimate(_Bellhop, _gs.Toss);

        Sequence_SimpleGreet.Add(W_D_RoomPos);
        Sequence_SimpleGreet.Add(F_D_Bell);
        Sequence_SimpleGreet.Add(F_B_Dweller);
        Sequence_SimpleGreet.Add(A_D_Toss);
        Sequence_SimpleGreet.Add(P_B_2R);
        Sequence_SimpleGreet.Add(M_D_DancePos);
        Sequence_SimpleGreet.Add(F_D_Camera);

        Sequence_SimpleGreet.Add(F_D_Bell);
        Sequence_SimpleGreet.Add(F_B_Dweller);
        Sequence_SimpleGreet.Add(A_B_Toss);
        Sequence_SimpleGreet.Add(P_D_2L);


        Setup_Tasksystem(Sequence_SimpleGreet);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HeardTaskEnded();
        }
    }
}
