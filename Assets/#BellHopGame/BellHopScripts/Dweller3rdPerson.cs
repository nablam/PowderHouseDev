//#define WayPoints
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

    ICharacterAnim _myI;

    string[] AnimStr = new string[13];
    public Material SpecMat;

#if WayPoints
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

        waypoints[0].IsNavTarget = false;
        waypoints[0].gameObject.GetComponent<Renderer>().material = SpecMat;
        waypoints[0].AnimToPlay = AnimStr[7];
        waypoints[1].IsNavTarget = false;
        waypoints[1].gameObject.GetComponent<Renderer>().material = SpecMat;
        waypoints[1].AnimToPlay = AnimStr[9];
        waypoints[3].IsNavTarget = false;
        waypoints[3].gameObject.GetComponent<Renderer>().material = SpecMat;
        waypoints[3].AnimToPlay = AnimStr[12];
        target = waypoints[0].transform;
    }
#endif

    private new void Start()
    {
#if WayPoints
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
#endif
        _myI = GetComponent<ICharacterAnim>();
        base.Start();
    }

    void Update()
    {
#if WayPoints
        if (!DoMove || target == null)
        {
            Move(Vector3.zero, false, false);

            return;
        }
        Vector3 lookdir = target.position - transform.position;

        //Move(lookdir.normalized / 2.5f, false, false);
        Move(lookdir.normalized, false, false);
#endif
    }

    public new void Move(Vector3 move, bool crouch, bool jump, bool UseBAse)
    {
        if (UseBAse)
            base.Move(move, false, false);
    }


#if WayPoints
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
#endif
    public void ManualStartAnim(string animationName)
    {

        StartCoroutine(DoTheAnim(animationName));
    }
    IEnumerator DoTheAnim(string arganimname)
    {


        _myI.AnimatorPlay(arganimname);

        yield return null;



    }


    DwellerTarget savedT;
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Finish"))
        //{

        print("bang" + other.name);
        DwellerTarget t;


        t = other.gameObject.GetComponent<DwellerTarget>();

        if (savedT == null)
        {
            print("no saved t must be the first");
            StartCoroutine(DoTheAnim(t.AnimToPlay));
        }
        else
        {
            if (t.GetHashCode() != savedT.GetHashCode())
            {

                StartCoroutine(DoTheAnim(t.AnimToPlay));
            }
            else
            {
                print("Touched Same");
            }

        }
        savedT = t;

        try
        {

            //if (t.TargId == target.GetComponent<DwellerTarget>().TargId)
            //{

            //    GetNextPoint();

            //    if (!t.IsNavTarget)

            //    {

            //        //  StartCoroutine(DoTheAnim(t.AnimToPlay));

            //    }

            //}
        }
        catch (System.Exception e)
        {
            Debug.Log("YO no target id");
            throw e;
        }


        // }
    }
}
