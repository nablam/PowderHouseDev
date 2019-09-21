using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerNavController : MonoBehaviour
{

    NavMeshAgent agent;
    Camera cam;
    ThirdPersonCharacter character;
    public bool IsMecanim;
    public Joystick Jstk;
    public bool IsJoyStick;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        character = GetComponent<ThirdPersonCharacter>();
    }

    private void Start()
    {
        //rotation is done by animated character
        if (IsMecanim)
        {
            agent.updateRotation = false;
        }

    }
    void Update()
    {
        if (IsJoyStick)
        {
            //let fixedupdate do it  UseJoyStick();
        }
        else
        {
            UseNavMeshAndClick();
        }
    }

    void UseNavMeshAndClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("cliik");
            //take mouse pos , and makes a ray in the direction of the cam out
            //store the ray 
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            print("ray at " + ray.origin.ToString());
            //before shooting the ray, need object to hold the hit 

            RaycastHit hit;
            //shout out the ray we want to shoot 
            if (Physics.Raycast(ray, out hit))
            {

                agent.SetDestination(hit.point);
                print("hit at " + hit.point.ToString());

            }

        }
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else //reached destination
        {
            character.Move(Vector3.zero, false, false);
        }
    }

    private bool _jumpPressed;
    private bool _fire;
    private bool _crouch;


    void UseJoyStick()
    {   // read user input: movement
        float h = Jstk.Horizontal;
        float v = Jstk.Vertical;


        // calculate move direction and magnitude to pass to character
        //  Vector3 camForward = new Vector3(_camTransform.forward.x, 0, _camTransform.forward.z).normalized;


        Vector3 move = v * Vector3.forward + h * Vector3.right;
        if (move.magnitude > 1)
            move.Normalize();

        // pass all parameters to the character control script
        character.Move(move, false, false);
    }


    private void FixedUpdate()
    {
        if (IsJoyStick)
            UseJoyStick();

    }


}
