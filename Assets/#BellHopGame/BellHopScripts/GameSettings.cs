using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    /// <summary>
    /// MAKE sure this class is instantiated first ! plz
    /// 
    /// </summary>
    public static GameSettings Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ActionNames = new List<string>(); //TODO: should be a dictionary if dataset gets longer 

            ActionNames.Add(_toss);
            ActionNames.Add(_catch);
            ActionNames.Add(_catch1);
            ActionNames.Add(_catch2);
            ActionNames.Add(_turn);
            ActionNames.Add(_unTurn);
            ActionNames.Add(_wave1);
            ActionNames.Add(_wave2);
            ActionNames.Add(_hello);
            ActionNames.Add(_dance1);
            ActionNames.Add(_dance2);
            ActionNames.Add(_bad);
            ActionNames.Add(_shrug);
            ActionNames.Add(_good);
            ActionNames.Add(_happy);
            ActionNames.Add(_come);
            ActionNames.Add(_no);
            ActionNames.Add(_openDoors);
            ActionNames.Add(_closeDoors);
            ActionNames.Add(_moveElevator);
        }
        else
            Destroy(this.gameObject);

        Master_Number_of_Floors = 5;
        if (Master_Number_of_Floors > _Master_max_Available_Dwellers_sofar)
            Master_Number_of_Floors = _Master_max_Available_Dwellers_sofar;

        ElevatorSpeed = 2.12f;
        // ElevatorSpeed = 1f;

        Debug.Log(nameof(_Master_max_Available_Dwellers_sofar));
    }

    public bool ShowDebugs = false;

    public List<string> ActionNames;
    #region ActionString

    string _toss = "Toss";
    string _catch = "Catch"; // goes into a looped pose
    string _catch1 = "Catch1";
    string _catch2 = "Catch2";
    string _turn = "Turn";
    string _unTurn = "UnTurn";
    string _wave1 = "Wave1";
    string _wave2 = "Wave2";
    string _hello = "Hello";
    string _dance1 = "Dance1";
    string _dance2 = "Dance2";
    string _bad = "Bad";
    string _shrug = "Shrug";
    string _good = "Good";
    string _happy = "Happy";
    string _come = "Come";
    string _idlesLong = "IdlesLong";
    string _idleMocap = "IdleMocap";
    string _openDoors = "OpenDoors"; //TODO: currently implemented separately
    string _closeDoors = "CloseDoors";//TODO: currently implemented separately
    string _moveElevator = "MoveElevator";//TODO: currently implemented separately according to old FLowElements

    string _palmpilot = "Palmpilot";
    string _investigateground = "Investigateground";
    string _searchground = "Searchground";
    string _answerphone = "Answerphone";
    string _slicebread = "Slicebread";
    string _typelaptop = "Typelaptop";
    string _raking = "Raking";
    string _shaving = "Shaving";
    string _cutonion = "Cutonion";
    string _eatsandwich = "Eatsandwich";
    string _playpiano = "Playpiano";
    string _dialphone = "Dialphone";
    string _brushteeth = "Brushteeth";

    string _sitChair = "SitChair";
    string _sitCCouch = "SitCouch";
    string _sitCross = "SitCross";
    string _bedtime = "Bedtime";
    string _no = "No";
    public string Toss { get => _toss; set => _toss = value; }
    public string Catch { get => _catch; set => _catch = value; }
    public string Catch1 { get => _catch1; set => _catch1 = value; }
    public string Catch2 { get => _catch2; set => _catch2 = value; }
    public string Turn { get => _turn; set => _turn = value; }
    public string UnTurn { get => _unTurn; set => _unTurn = value; }
    public string Wave1 { get => _wave1; set => _wave1 = value; }
    public string Wave2 { get => _wave2; set => _wave2 = value; }
    public string Hello { get => _hello; set => _hello = value; }
    public string Dance1 { get => _dance1; set => _dance1 = value; }
    public string Dance2 { get => _dance2; set => _dance2 = value; }
    public string Bad { get => _bad; set => _bad = value; }
    public string Shrug { get => _shrug; set => _shrug = value; }
    public string Good { get => _good; set => _good = value; }
    public string Happy { get => _happy; set => _happy = value; }
    public string Come { get => _come; set => _come = value; }
    public string No { get => _no; set => _no = value; }
    public string IdlesLong { get => _idlesLong; set => _idlesLong = value; }
    public string IdleMocap { get => _idleMocap; set => _idleMocap = value; }

    public float ElevatorSpeed { get => _elevatorSpeed; set => _elevatorSpeed = value; }
    public string OpenDoors { get => _openDoors; set => _openDoors = value; }
    public string CloseDoors { get => _closeDoors; set => _closeDoors = value; }
    public string MoveElevator { get => _moveElevator; set => _moveElevator = value; }

    public string Palmpilot { get => _palmpilot; set => _palmpilot = value; }
    public string Investigateground { get => _investigateground; set => _investigateground = value; }
    public string Searchground { get => _searchground; set => _searchground = value; }
    public string Answerphone { get => _answerphone; set => _answerphone = value; }
    public string Slicebread { get => _slicebread; set => _slicebread = value; }
    public string Typelaptop { get => _typelaptop; set => _typelaptop = value; }
    public string Raking { get => _raking; set => _raking = value; }
    public string Shaving { get => _shaving; set => _shaving = value; }
    public string Cutonion { get => _cutonion; set => _cutonion = value; }
    public string Eatsandwich { get => _eatsandwich; set => _eatsandwich = value; }
    public string Playpiano { get => _playpiano; set => _playpiano = value; }
    public string Dialphone { get => _dialphone; set => _dialphone = value; }
    public string Brushteeth { get => _brushteeth; set => _brushteeth = value; }
    public string SitChair { get => _sitChair; set => _sitChair = value; }
    public string SitCouch { get => _sitCCouch; set => _sitCCouch = value; }
    public string SitCross { get => _sitCross; set => _sitCross = value; }
    public string Bedtime { get => _bedtime; set => _bedtime = value; }


    #endregion

    int _master_Number_of_Floors;
    public int Master_Number_of_Floors { get => _master_Number_of_Floors; set => _master_Number_of_Floors = value; }





    //we only have 9 animals right now , we can create 18 animals male/female. lets keep the max at a power of 2 for now 
    //UPDATE: we only have 12 butons, so lets make this max 12 for now
    const int _Master_max_Available_Dwellers_sofar = 12;

    float _elevatorSpeed;

}


/*
 * 
"Toss";
"Catch"; // goes into a looped pose
"Catch2";
"Turn";
"UnTurn";
"Wave1";
 "Wave2";
_lo = "Hello";
_ce1 = "Dance1";
_ce2 = "Dance2";
   _ = "Bad";
    _ug = "Shrug";
    _d = "Good";
    _py = "Happy";
    _e = "Come";
 _ "OpenDoors"; 
  _ "CloseDoors"; 
    _ "MoveElevator";
 _ "Palmpilot";
 "Investigateground";
  "Searchground";
 "Answerphone";
  "Slicebread";
  _ "Typelaptop";
    _ing = "Raking";
    _ving = "Shaving";
_ "Cutonion";
   _ "Eatsandwich";
 _ "Playpiano";
 _ "Dialphone";
  _ "Brushteeth";
     
     */
