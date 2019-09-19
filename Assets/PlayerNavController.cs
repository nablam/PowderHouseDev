using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerNavController : MonoBehaviour
{

    NavMeshAgent agent;
    Camera cam;
    ThirdPersonCharacter character;
    public bool IsMecanim;
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
            print("wt");
            character.Move(agent.desiredVelocity, false, false);
        }
        else //reached destination
        {
            print("ff");
            character.Move(Vector3.zero, false, false);
        }
    }
    /*
    // Update is called once per frame
    void Update()
    {
        RegisterCickandSetDestination();

        if (IsMecanim)
        {
            Update_MecanimAgentMoveRot();
        }

    }

    void RegisterCickandSetDestination()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("cliik");

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            print("ray at " + ray.origin.ToString());


            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                agent.SetDestination(hit.point);
                print("hit at " + hit.point.ToString());

            }

        }
    }

    void Update_MecanimAgentMoveRot()
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
    */

    /*
   private void Start()
   {
       //rotation is done by animated character
       agent.updateRotation = false;
   }

   // Update is called once per frame
   void Update()
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
   */
}
