using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class DwellerNavigator : MonoBehaviour
{
    [SerializeField]
    GameObject[] waypoints;
    int ptr = 0;
    Transform target;
    ThirdPersonCharacter character;
    public Animator anim;


    public bool test;
    void Start()
    {
        anim = GetComponent<Animator>();
        character = GetComponent<ThirdPersonCharacter>();
        waypoints = GameObject.FindGameObjectsWithTag("Finish");
        target = waypoints[0].transform;


        act = Lookdir_Dividedby10;
        animname = "Run";
    }



    Action act;
    Action<string, int> act1;
    string animname;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 ");
            animname = "Run";



        }
        else
              if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2");
            animname = "Idle";


        }

        anim.PlayInFixedTime(animname, 0);
    }





    void NotTest()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1_Lookdir_Dividedby10");
            // 
            act = Lookdir_Dividedby10;


        }
        else
if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2_Lookdir_DividedBy4");
            act = LookdiFAde;
        }
        else
if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3_targpos_only");
            act = null;
        }
        else
if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("4_lookdir_Normalized");
            act = lookdir_Normalized;
        }
        else
if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("5_lookdir_Normalized_Dividedby2");
            act = lookdir_Normalized_Dividedby2;
        }

        act();
    }


    void Lookdir_Dividedby10()
    {
        Vector3 lookdir = target.position - transform.position;
        lookdir /= 10f;
        character.Move(lookdir, false, false);
    }
    void LookdiFAde()
    {
        // anim.CrossFade("Run", 0.5f, PlayMode.StopSameLayer);
        anim.Play("Run", 0);
    }
    void lookdir_Normalized()
    {
        Vector3 lookdir = target.position - transform.position;

        character.Move(lookdir.normalized, false, false);
    }


    void lookdir_Normalized_Dividedby2()
    {
        Vector3 lookdir = target.position - transform.position;

        character.Move(lookdir.normalized / 2f, false, false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            ptr++;
            if (ptr >= waypoints.Length)
            {
                ptr = 0;
            }
            target = waypoints[ptr].transform;

        }
    }
}
