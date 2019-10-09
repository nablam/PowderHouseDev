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

    string[] AnimStr = new string[13];
    public Material SpecMat;
    void Init()
    {
        //print("init");
        waypoints = GameObject.FindObjectsOfType<DwellerTarget>();
        TRANSs = new Transform[waypoints.Length];
        for (int x = 0; x < waypoints.Length; x++)
        {
            waypoints[x].TargId = x;
            waypoints[x].IsNavTarget = true;
            waypoints[x].AnimToPlay = AnimStr[x];
            TRANSs[x] = waypoints[x].transform;
        }

        waypoints[2].IsNavTarget = false;
        waypoints[2].gameObject.GetComponent<Renderer>().material = SpecMat;
        waypoints[5].IsNavTarget = false;
        waypoints[5].gameObject.GetComponent<Renderer>().material = SpecMat;
        waypoints[7].IsNavTarget = false;
        waypoints[7].gameObject.GetComponent<Renderer>().material = SpecMat;
        target = waypoints[0].transform;
    }

    private new void Start()
    {
        AnimStr[0] = GameSettings.Instance.Palmpilot;
        AnimStr[1] = GameSettings.Instance.Investigateground;
        AnimStr[2] = GameSettings.Instance.Searchground;
        AnimStr[3] = GameSettings.Instance.Answerphone;
        AnimStr[4] = GameSettings.Instance.Slicebread;
        AnimStr[5] = GameSettings.Instance.Typelaptop;
        AnimStr[6] = GameSettings.Instance.Raking;
        AnimStr[7] = GameSettings.Instance.Shaving;
        AnimStr[8] = GameSettings.Instance.Cutonion;
        AnimStr[9] = GameSettings.Instance.Eatsandwich;
        AnimStr[10] = GameSettings.Instance.Playpiano;
        AnimStr[11] = GameSettings.Instance.Dialphone;
        AnimStr[12] = GameSettings.Instance.Brushteeth;
        Init();
        base.Start();
    }

    void Update()
    {
        if (!DoMove) return;
        Vector3 lookdir = target.position - transform.position;

        //Move(lookdir.normalized / 2.5f, false, false);
        Move(lookdir.normalized, false, false);
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

                    StartCoroutine(DoTheAnim(t.AnimToPlay));

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
    IEnumerator DoTheAnim(string arganimname)
    {

        DoMove = false;
        Get_myAnimator().Play(arganimname);




        //float lengthofClip = Get_myAnimator().GetNextAnimatorStateInfo(0).length;
        //Debug.Log("LEN ");
        yield return null;



    }
}
