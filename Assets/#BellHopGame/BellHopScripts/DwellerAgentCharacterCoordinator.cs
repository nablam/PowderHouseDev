using UnityEngine;
using UnityEngine.AI;

public class DwellerAgentCharacterCoordinator : MonoBehaviour
{

    NavMeshAgent agent;
    bool AgentIsAwake = false;
    Dweller3rdPerson character;
    ICharacterAnim _myIchar;
    bool IsMecanim;
    //bool _activateUpdate;
    AgentStates _mystate;
    Transform CahsedDESTINATION = null;
    InteractionCentral IC;
    enum AgentStates
    {

        NotInitialized,
        Initialized,
        MovingToTarget,
        ReachedTargetAndDoinstuff,
        WaitingForNextTask,
    }
    private void OnEnable()
    {
        _mystate = AgentStates.NotInitialized;
        _myIchar = GetComponent<ICharacterAnim>();
        character = GetComponent<Dweller3rdPerson>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Awake()
    {
        IsMecanim = true;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //rotation is done by animated character
        if (IsMecanim) agent.updateRotation = false;
        agent.enabled = false;
    }

    public void ActivateAgent()
    {
        agent.enabled = true;
        if (agent.isActiveAndEnabled) print("ACTIVIA");
        else
            print("POOP");

        AgentIsAwake = true;
    }
    void FixedUpdate()
    {
        if (CahsedDESTINATION == null) return;
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
        Vector3 Direction = CahsedDESTINATION.position - this.transform.position;
        character.JustTurn(Direction.normalized);
        Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), Direction, Color.red, 0.1f);
    }
    void USeAI()
    {



        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else //reached destination
        {
            character.Move(Vector3.zero, false, false);
        }

    }

    public void Set_TargetTRans(Transform artT)
    {
        CahsedDESTINATION = artT;

    }
    void Set_Destination(Transform argDest)
    {

        agent.SetDestination(argDest.position);
    }

    public void WarpMeAgentto(Transform argDest)
    {
        agent.Warp(argDest.position);
    }






}


