﻿//define DebugOn

using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class DwellerMeshComposer : MonoBehaviour, ICharacterAnim
{


    enum AgentStates
    {

        NotInitialized,
        Initialized,
        MovingToTarget,
        ReachedTargetAndDoinstuff,
        WaitingForNextTask,
    }
    #region PublicVars
    public string AnimalName;
    public string AnimalType;
    public GameObject Body;

    public GameObject ShirtLong;
    public GameObject ShirtShort;
    public GameObject PantsLong;
    public GameObject PantsShort;
    public GameObject FeetCat;
    public GameObject FeetChiken;
    public GameObject FeetBoots;
    public GameObject FeetCartoon;
    public GameObject Gloves;




    public GameObject HeadCat;
    public GameObject HeadDuck;
    public GameObject HeadMole;
    public GameObject HeadMonkey;
    public GameObject HeadMouse;
    public GameObject HeadPenguin;
    public GameObject HeadPig;
    public GameObject HeadRabbit;
    public GameObject HeadSheep;

    public Material MatCat;
    public Material MatDuck;
    public Material MatMole;
    public Material MatMonkey;
    public Material MatMouse;
    public Material MatPenguin;
    public Material MatPig;
    public Material MatRabbit;
    public Material MatSheep;


    public Material White;
    public Material Red;
    public Material Pink;
    public Material Orange;
    public Material Yellow;
    public Material Green;
    public Material Blue;
    public Material Purple;
    public Material Black;
    public Material Grey;
    public Material Brown;



    public Transform RightHandHoldPos;
    public Transform LeftHandHoldPos;
    #endregion

    #region Public Props
    int _id;
    public int Id { get => _id; set => _id = value; }
    [SerializeField]
    int _myFinalResidenceFloorNumber;
    public int MyFinalResidenceFloorNumber { get => _myFinalResidenceFloorNumber; set => _myFinalResidenceFloorNumber = value; }

    GameEnums.Gender _gender;
    public GameEnums.Gender Gender { get => _gender; set => _gender = value; }
    #endregion

    #region BodySetup

    SkinnedMeshRenderer BodyRenderer;

    GameObject _CurHeldObject = null;
    GameObject _CurReceivedObject = null;

    GameObject ShirtPtr;
    GameObject PantsPtr;





    Material GetMatByEnum(GameEnums.MatColors argMatcolor)
    {

        switch (argMatcolor)
        {
            case GameEnums.MatColors.black:
                return Black;
            case GameEnums.MatColors.white:
                return White;
            case GameEnums.MatColors.red:
                return Red;
            case GameEnums.MatColors.pink:
                return Pink;
            case GameEnums.MatColors.orange:
                return Orange;
            case GameEnums.MatColors.yellow:
                return Yellow;
            case GameEnums.MatColors.green:
                return Green;
            case GameEnums.MatColors.blue:
                return Blue;
            case GameEnums.MatColors.purple:
                return Purple;
            case GameEnums.MatColors.grey:
                return Grey;
            case GameEnums.MatColors.brown:
                return Brown;

            default:
                return White;

        }

    }

    void HideHeads()
    {
        HeadCat.SetActive(false);
        HeadDuck.SetActive(false);
        HeadMole.SetActive(false);
        HeadMonkey.SetActive(false);
        HeadMouse.SetActive(false);
        HeadPenguin.SetActive(false);
        HeadPig.SetActive(false);
        HeadRabbit.SetActive(false);
        HeadSheep.SetActive(false);

    }
    void ShowHead(string argHeadName)
    {
        HideHeads();
        //Debug.Log("show head " + argHeadName);
        switch (argHeadName)
        {
            case "cat":
                HeadCat.SetActive(true);
                break;
            case "duck":
                HeadDuck.SetActive(true);
                break;
            case "mole":
                HeadMole.SetActive(true);
                break;
            case "monkey":
                HeadMonkey.SetActive(true);
                break;

            case "mouse":
                HeadMouse.SetActive(true);
                break;




            case "penguin":
                HeadPenguin.SetActive(true);
                break;



            case "pig":
                HeadPig.SetActive(true);
                break;

            case "rabbit":
                HeadRabbit.SetActive(true);
                break;

            case "sheep":
                HeadSheep.SetActive(true);
                break;
            default:
                Debug.LogError("no head match");
                break;
        }
    }

    void HideFeet()
    {
        FeetCat.SetActive(false);
        FeetChiken.SetActive(false);
        FeetBoots.SetActive(false);
        FeetCartoon.SetActive(false);
    }
    void HideShirts()
    {
        ShirtPtr = null;
        ShirtLong.SetActive(false);
        ShirtShort.SetActive(false);

    }
    void HidePants()
    {
        PantsPtr = null;
        PantsLong.SetActive(false);
        PantsShort.SetActive(false);
    }
    void ShowFeet(string argFeetName)
    {
        HideFeet();
        switch (argFeetName)
        {
            case "feetcat":
                FeetCat.SetActive(true);
                break;
            case "feetchicken":
                FeetChiken.SetActive(true);
                break;
            case "feetboots":
                FeetBoots.SetActive(true);
                break;
            case "feetcartoon":
                FeetCartoon.SetActive(true);
                break;

            default:
                Debug.LogError("no feet match");
                break;
        }
    }
    void ShowShirts(string argShirtsName)
    {
        HideShirts();
        switch (argShirtsName)
        {
            case "shirtlong":
                ShirtLong.SetActive(true);
                ShirtPtr = ShirtLong;
                break;
            case "shirtshort":
                ShirtShort.SetActive(true);
                ShirtPtr = ShirtShort;
                break;
            case "none":
                ShirtPtr = null;
                ShirtShort.SetActive(false);
                ShirtLong.SetActive(false);
                break;


            default:
                Debug.LogError("no shirt match");
                break;
        }
    }
    void ShowPants(string agPantsNAme)
    {
        HidePants();
        switch (agPantsNAme)
        {
            case "pantslong":
                PantsLong.SetActive(true);
                PantsPtr = PantsLong;
                break;
            case "pantsshort":
                PantsShort.SetActive(true);
                PantsPtr = PantsShort;
                break;
            case "none":
                PantsPtr = null;
                PantsLong.SetActive(false);
                PantsShort.SetActive(false);
                break;


            default:
                Debug.LogError("no feet match");
                break;
        }
    }



    void SetBodyMat(string argAnimalName)
    {

        switch (argAnimalName)
        {
            case "cat":
                BodyRenderer.material = MatCat;
                break;
            case "duck":
                BodyRenderer.material = MatDuck;
                break;
            case "mole":
                BodyRenderer.material = MatMole;
                break;
            case "monkey":
                BodyRenderer.material = MatMonkey;
                break;

            case "mouse":
                BodyRenderer.material = MatMouse;
                break;




            case "penguin":
                BodyRenderer.material = MatPenguin;
                break;



            case "pig":
                BodyRenderer.material = MatPig;
                break;

            case "rabbit":
                BodyRenderer.material = MatRabbit;
                break;

            case "sheep":
                BodyRenderer.material = MatSheep;
                break;
            default:
                Debug.LogError("no body match");
                break;
        }

    }

    void SetFeetMat()
    {
        FeetCat.GetComponent<SkinnedMeshRenderer>().material = BodyRenderer.material;
        FeetBoots.GetComponent<SkinnedMeshRenderer>().material = BodyRenderer.material;
        FeetCartoon.GetComponent<SkinnedMeshRenderer>().material = BodyRenderer.material;
        FeetChiken.GetComponent<SkinnedMeshRenderer>().material = BodyRenderer.material;
    }

    void ComposeMesh(GameEnums.DynAnimal arganimal, GameEnums.Shirts argShirt, GameEnums.MatColors argshirtcolor, GameEnums.Pants argpants, GameEnums.MatColors argPantscolor, GameEnums.Shoes argshoes, GameEnums.MatColors argshoecolor)
    {
        SetBodyMat(arganimal.ToString());
        ShowHead(arganimal.ToString());
        SetFeetMat();
        ShowShirts(argShirt.ToString());
        ShowPants(argpants.ToString());
        ShowFeet(argshoes.ToString());

        if (ShirtPtr != null)
        {
            ShirtPtr.GetComponent<SkinnedMeshRenderer>().material = GetMatByEnum(argshirtcolor);
        }

        if (PantsPtr != null)
        {
            PantsPtr.GetComponent<SkinnedMeshRenderer>().material = GetMatByEnum(argPantscolor);
        }
    }


    GameEnums.Shirts GetRandShirt() { return (GameEnums.Shirts)Random.Range(0, Enum.GetNames(typeof(GameEnums.Shirts)).Length); }
    GameEnums.Pants GetRandPants() { return (GameEnums.Pants)Random.Range(0, Enum.GetNames(typeof(GameEnums.Pants)).Length); }
    GameEnums.MatColors GetRandColor() { return (GameEnums.MatColors)Random.Range(0, Enum.GetNames(typeof(GameEnums.MatColors)).Length); }
    GameEnums.Shoes GetRandShoes() { return (GameEnums.Shoes)Random.Range(0, Enum.GetNames(typeof(GameEnums.Shoes)).Length); }

    public void Make(GameEnums.DynAnimal arganimal, string argMyName)
    {

        HideHeads();
        HideFeet();
        HidePants();
        HideShirts();


        ComposeMesh(arganimal, GetRandShirt(), GetRandColor(), GetRandPants(), GetRandColor(), GetRandShoes(), GetRandColor());

        AnimalName = argMyName;
        AnimalType = arganimal.ToString(); ;
    }


    #endregion


    #region NonAgent

    DeliveryItem _initiallyAssignedAtSceneBuild;

    Animator _MyAnimator;
    NavMeshAgent agent;
    bool AgentIsAwake = false;
    bool AgentReachedDestination = false;

    Dweller3rdPerson DWell_3rd_perCTRL;



    private void OnEnable()
    {
        _mystate = AgentStates.NotInitialized;
        Awake_agent();
    }

    private void Awake()
    {
        BodyRenderer = Body.GetComponent<SkinnedMeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

        agent.enabled = false;
        _MyAnimator = GetComponent<Animator>();
    }


    Quaternion OriginalRot;
    public DeliveryItem GetInitialDeliveryItem()
    {
        try
        {


        }
        catch (Exception e)
        {
            print("error");

        }

        if (_initiallyAssignedAtSceneBuild == null) Debug.LogError("! Dweller: i haz no  initial obj, or it haz been changd ");
        return _initiallyAssignedAtSceneBuild;
    }


    public void InitializeHeldObject(GameObject argItemInHand)
    {
        _initiallyAssignedAtSceneBuild = argItemInHand.GetComponent<DeliveryItem>();
        _CurHeldObject = argItemInHand;
        _CurHeldObject.transform.position = new Vector3(RightHandHoldPos.position.x, RightHandHoldPos.position.y, RightHandHoldPos.position.z);
        _CurHeldObject.transform.parent = RightHandHoldPos.transform;
        OriginalRot = transform.rotation;
    }

    public DwellerMeshComposer GetMyHeldObjectDestination()
    {
        if (_CurHeldObject != null)
        {


            return _CurHeldObject.GetComponent<DeliveryItem>().GetDestFloorDweller();
        }
        else
            return null;
    }

    public DeliveryItem HELP_firstGuyOut()
    {
        return _CurHeldObject.GetComponent<DeliveryItem>();
    }



    #endregion

    #region InterfaceForAnimatorListen

    Action MidTossCallBAck;
    Action MidCatchCallBAck; //may not be needed
    //animate
    public void AnimateToss(Action argTossAimeEvnet)
    {
        MidTossCallBAck = argTossAimeEvnet;
        _MyAnimator.SetTrigger("TrigToss");
    }

    public void AnimateCatch(Action argCatchAimeEvnet)
    {
        MidCatchCallBAck = argCatchAimeEvnet;
        _MyAnimator.SetTrigger("TrigCatch");
    }


    //anim event handler
    public void AnimTossPeack()
    {
#if DebugOn
        Debug.Log("Dweller: TOSS APEXXX");
#endif
        MidTossCallBAck();
    }

    public void AnimCatchPeack()
    {
#if DebugOn
        Debug.Log("Dweller: CATCH APEXXX");
#endif

        MidCatchCallBAck();
    }


    public void ReleaseObj_CalledExternally() //after anim event handled
    {
        _CurHeldObject.transform.parent = null;
        _initiallyAssignedAtSceneBuild = null;
        _CurHeldObject = null;
    }


    public Transform GetMyRightHandHold() { return this.RightHandHoldPos; }
    public Transform GetMyLeftHandHold() { return this.LeftHandHoldPos; }



    public void AnimTrigger(string argTrig)
    {
        //print("sanitycheck Dweller anim " + "Trig" + argTrig);
        _MyAnimator.SetTrigger("Trig" + argTrig);
    }

    string PlayingAnimState = "";
    Action WhatToDoWhenThisAnimStateEnds = null;
    public void AnimateNamedAction(string argactionNAme, Action OnEnded_slash_ArrivedAtPos_Callback = null)
    {
        PlayingAnimState = argactionNAme;
        WhatToDoWhenThisAnimStateEnds = OnEnded_slash_ArrivedAtPos_Callback;
        AnimTrigger(argactionNAme); //ads trig to it 
    }


    public void NotifyMeWheanAnimationStateExit()
    {
#if DebugOn
        Debug.Log("Dweller: stateExit " + PlayingAnimState);
#endif
        WhatToDoWhenThisAnimStateEnds();
    }

    public GameObject TemMyGO()
    {
        return this.gameObject;
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------>DWELL 3rdPErson

    public void OnAnimationstateTaggedDoneExit()
    {
        // DWell_3rd_perCTRL.SpecialDoeWasDone();
        agent.isStopped = false;
        // MoveAgentTo(CahsedDESTINATION, true);
    }

    #endregion


    #region AGENT
    AgentStates _mystate;

    void Update()
    {
        if (_mystate == AgentStates.MovingToTarget)
            DoNav();

        else

            if (_mystate == AgentStates.ReachedTargetAndDoinstuff)
            Dostuff();
    }


    public void WarpAgent(Transform artT)
    {
        agent.Warp(artT.position);
    }

    Transform CahsedDESTINATION;
    InteractionCentral IC;
    public void MoveAgentTo(Transform artT, bool argDoWalk)
    {
        _mystate = AgentStates.MovingToTarget;
        AgentReachedDestination = false;
        IsMecanim = true;
        if (IsMecanim)
        {
            agent.updateRotation = false;

        }
        CahsedDESTINATION = artT;
        IC = CahsedDESTINATION.gameObject.GetComponent<InteractionCentral>();
        agent.SetDestination(artT.position);

    }
    /*public*/
    bool IsMecanim;


    private void Awake_agent()
    {

        agent = GetComponent<NavMeshAgent>();

        DWell_3rd_perCTRL = GetComponent<Dweller3rdPerson>();
    }

    public void Start_Agent()
    {

        IsMecanim = true;

        agent.enabled = true;
        if (agent.isActiveAndEnabled)
        {
            print("ACTIVIA");
        }
        else
        {
            print("POOP");

        }

        //rotation is done by animated character

        if (IsMecanim)
        {
            agent.updateRotation = false;

        }

        AgentIsAwake = true;
        _mystate = AgentStates.Initialized;
    }


    #endregion





    void DoNav()
    {
        agent.updateRotation = false;
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            DWell_3rd_perCTRL.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            _mystate = AgentStates.ReachedTargetAndDoinstuff;
        }
    }

    void Dostuff()
    {
        DWell_3rd_perCTRL.Move(Vector3.zero, false, false);
        agent.updateRotation = true;
        if (IC != null)
        {

            transform.LookAt(IC.GetLookTarg());

            AnimatorPlay(IC.argActionString);
        }
    }




    void Navigate()
    {
        if (!AgentIsAwake) return;
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            DWell_3rd_perCTRL.Move(agent.desiredVelocity, false, false);
        }
        else //reached destination
        {
            DWell_3rd_perCTRL.Move(Vector3.zero, false, false);
            if (!AgentReachedDestination)
            {
                print("reached once");


                IsMecanim = false;
                if (IsMecanim)
                {
                    agent.updateRotation = true;

                }



                AgentReachedDestination = true;
            }
            if (!IsMecanim)
            {
                if (IC != null)
                {

                    transform.LookAt(IC.GetLookTarg());

                    AnimatorPlay(IC.argActionString);
                }
            }

        }

    }




    public void AnimatorPlay(string argname)
    {
        print("play" + argname);
        _MyAnimator.Play(argname, 0);

    }

    public void ResumAgent()
    {

    }
}

