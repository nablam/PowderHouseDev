using System;
using UnityEngine;
using UnityEngine.AI;
using static GameEnums;

public class DwellerAgentCharacterCoordinator : MonoBehaviour
{

    NavMeshAgent agent;
    bool AgentIsAwake = false;
    Dweller3rdPerson character;
    ICharacterAnim _myIchar;
    bool IsMecanim;
    //bool _activateUpdate;
    CharCoordinatorStates _mystate;
    [SerializeField]
    Transform CahsedDESTINATION = null;
    InteractionCentral IC;
    public bool DoUseAi;
    bool NOtStartedWalking;
    bool arrived;

    Action OnRotationComplete;

    public CharCoordinatorStates Mystate() { return _mystate; }

    void RotationCompleted()
    {
        print("fin trn");
        _mystate = CharCoordinatorStates.Interacting;

    }
    private void OnEnable()
    {
        _mystate = CharCoordinatorStates.NotInitialized;
        _myIchar = GetComponent<ICharacterAnim>();
        character = GetComponent<Dweller3rdPerson>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Awake()
    {
        IsMecanim = true;
        agent = GetComponent<NavMeshAgent>();
        if (IsMecanim) agent.updateRotation = false;

    }

    private void Start()
    {
        //rotation is done by animated character
        // agent.enabled = false;

    }

    public void ActivateAgent()
    {
        agent.enabled = true;
        if (agent.isActiveAndEnabled)
        {
            // print("ACTIVIA");
        }
        else
        {
            // print("POOP");
        }
        AgentIsAwake = true;

        _mystate = CharCoordinatorStates.Initialized;
    }
    void FixedUpdate()
    {
        if (CahsedDESTINATION == null) return;

        if (DoUseAi) USeAI();
        else
            TempOnlyTurnBackCharacter();

    }

    void TempOnlyUseCahar()
    {
        Vector3 Direction = CahsedDESTINATION.position - this.transform.position;
        character.Move(Direction.normalized, false, false);
        Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), Direction, Color.red, 0.1f);
    }

    void TempOnlyTurnBackCharacter()
    {
        IC = CahsedDESTINATION.gameObject.GetComponentInParent<InteractionCentral>();
        if (IC == null)
        {
            Debug.Log("NOT AN INTERACTION CENTRAL!");
        }
        Vector3 Direction = IC.GetLookTarg().position - this.transform.position;
        character.JustTurn(Direction.normalized, IC.argActionString, RotationCompleted);
        Debug.DrawRay(new Vector3(transform.position.x, 1f, transform.position.z), new Vector3(Direction.x, 0f, Direction.z), Color.red, 0.1f);
    }
    void USeAI()
    {

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else //reached destination
        {
            if (!NOtStartedWalking)
            {
                print("started Nav To TArget");
                _mystate = CharCoordinatorStates.MovingToTarget;
                NOtStartedWalking = true;
            }
            character.Move(Vector3.zero, false, false);
            //
        }



        CheckIfReached();

    }

    void CheckIfReached()
    {

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if (!arrived)
                    {
                        print("NOW WE HERE");
                        DoUseAi = false;
                        arrived = true;
                        character.Reset_ReachedRot();
                        _mystate = CharCoordinatorStates.RotatingToPlace;
                    }
                }
            }
        }
    }

    public void Set_TargetTRans(Transform artT)
    {
        CahsedDESTINATION = artT;


    }
    public void Set_Destination(Transform argDest)
    {
        NOtStartedWalking = false;
        arrived = false;
        agent.SetDestination(argDest.position);
    }

    public void WarpMeAgentto(Transform argDest)
    {
        //  print("_A_0_C WarpMeAgentto to " + argDest.parent.name);
        agent.Warp(argDest.position);
    }

    public void ResetAgent()
    {
        agent.ResetPath();
    }





}


