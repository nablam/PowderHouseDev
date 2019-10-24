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





    List<ITaskAction> Sequence_SimpleGreet;
    ITaskAction _DwellerWarp_door;
    ITaskAction _DwellerFace_bell;
    ITaskAction _BellhopFace_dwell;
    ITaskAction _DwellerPull;
    ITaskAction _BellHopPull;
    ITaskAction _DwellerAnimate_wave2;
    ITaskAction _DwellerAnimate_Toss;

    List<ITaskAction> Sequence_Greet_fromRomm;
    ITaskAction _DwellerWarp_Room;
    ITaskAction _DwellerMove_door;

    //ITaskAction _DwellerFace_1;
    //ITaskAction _BellhopFace;
    //ITaskAction _DwellerPull;
    //ITaskAction _BellHopPull;
    //ITaskAction _DwellerAnimate_Toss;


    List<ITaskAction> Sequence_WrongFloor;
    ITaskAction _DwellerAnimate_NO;

    List<ITaskAction> Sequence_MovesAndAnims;
    ITaskAction _DwellerMove_Room;
    ITaskAction _rotLook;
    ITaskAction _DwellerMove_Dance;
    ITaskAction _DwellerAnimateRoom;

    List<ITaskAction> Sequence_BellPulls;

    List<ITaskAction> Sequence_DwellerPulls;


    private void Start()
    {
        _gs = GameSettings.Instance;
        Sequence_SimpleGreet = new List<ITaskAction>();

        _DwellerWarp_door = new TA_DwellerWarp(_Dweller, _exhangeI.GetActionPos());
        _DwellerFace_bell = new TA_DwellerFace(_Dweller, _Bellhop.transform);
        _BellhopFace_dwell = new TA_DwellerFace(_Bellhop, _Dweller.transform);
        _DwellerAnimate_wave2 = new TA_DwellerAnimate(_Dweller, _gs.Wave2);
        _DwellerAnimate_Toss = new TA_DwellerAnimate(_Dweller, _gs.Toss); //as soon as thats done next task is bellhop pull
        _BellHopPull = new TA_BellHopPull(_Bellhop, _Dweller.GetComponent<CharacterItemManager>());
        _DwellerWarp_Room = new TA_DwellerWarp(_Dweller, _couchI.GetActionPos());
        _DwellerMove_door = new TA_DwellerMoveTo(_Dweller, _exhangeI.GetActionPos());
        Sequence_SimpleGreet.Add(_DwellerWarp_door);
        Sequence_SimpleGreet.Add(_DwellerFace_bell);
        Sequence_SimpleGreet.Add(_BellhopFace_dwell);
        Sequence_SimpleGreet.Add(_DwellerAnimate_wave2);
        Sequence_SimpleGreet.Add(_DwellerAnimate_Toss);
        Sequence_SimpleGreet.Add(_BellHopPull);


        Sequence_Greet_fromRomm = new List<ITaskAction>();
        Sequence_Greet_fromRomm.Add(_DwellerWarp_Room);
        Sequence_Greet_fromRomm.Add(_DwellerMove_door);
        Sequence_Greet_fromRomm.AddRange(Sequence_SimpleGreet);

        Sequence_WrongFloor = new List<ITaskAction>();
        _DwellerAnimate_NO = new TA_DwellerAnimate(_Dweller, _gs.No);

        Sequence_MovesAndAnims = new List<ITaskAction>();
        _DwellerMove_Room = new TA_DwellerMoveTo(_Dweller, _couchI.GetActionPos());
        _rotLook = new TA_DwellerFace(_Dweller, _couchI.GetLookTarg());
        _DwellerAnimateRoom = new TA_DwellerAnimate(_Dweller, _couchI.argActionString);

        Sequence_MovesAndAnims.Add(_DwellerWarp_door);
        Sequence_MovesAndAnims.Add(_DwellerMove_Room);
        Sequence_MovesAndAnims.Add(_rotLook);
        Sequence_MovesAndAnims.Add(_DwellerAnimateRoom);
    }

}
