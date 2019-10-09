using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Dweller3rdPerson : ThirdPersonCharacter
{
    [SerializeField]
    DwellerTarget[] waypoints;
    public Transform[] TRANSs;
    int ptr = 0;
    Transform target;
    bool DoMove = true;
    void Init()
    {
        //print("init");
        waypoints = GameObject.FindObjectsOfType<DwellerTarget>();
        TRANSs = new Transform[waypoints.Length];
        for (int x = 0; x < waypoints.Length; x++)
        {
            waypoints[x].TargId = x;
            waypoints[x].IsNavTarget = true;
            TRANSs[x] = waypoints[x].transform;
        }

        waypoints[2].IsNavTarget = false;
        target = waypoints[0].transform;
    }

    private new void Start()
    {
        Init();
        base.Start();
    }

    void Update()
    {
        if (!DoMove) return;
        Vector3 lookdir = target.position - transform.position;

        Move(lookdir.normalized / 2.5f, false, false);
    }

    public new void Move(Vector3 move, bool crouch, bool jump)
    {
        base.Move(move, false, false);

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Finish"))
        //{
        DwellerTarget t;
        try
        {

            // print("if at first you dont succeed");
            t = other.gameObject.GetComponent<DwellerTarget>();

            if (t.TargId == target.GetComponent<DwellerTarget>().TargId)
            {

                GetNextPoint();

                if (!t.IsNavTarget)

                {

                    StartCoroutine(DoTheAnim());

                }

            }
        }
        catch (System.Exception e)
        {
            Debug.Log("YO no target");
            throw;
        }


        // }
    }

    void GetNextPoint()
    {
        ptr++;
        if (ptr >= waypoints.Length)
        {
            ptr = 0;
        }
        target = waypoints[ptr].transform;
    }

    public void SpecialDoeWasDone()
    {

        DoMove = true;
    }
    IEnumerator DoTheAnim()
    {

        DoMove = false;
        Get_myAnimator().Play("Run");




        //float lengthofClip = Get_myAnimator().GetNextAnimatorStateInfo(0).length;
        Debug.Log("LEN ");
        yield return null;



    }
}
