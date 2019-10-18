using UnityEngine;

public class InteractionCentral : MonoBehaviour
{

    Transform LookitHere;
    Transform ActionPos;




    public string argActionString;



    void Start()
    {
        ActionPos = this.transform.GetChild(0);
        LookitHere = ActionPos.GetChild(0);
        string output = "";
        if (argActionString == "Dance")
        {
            int rand = Random.Range(0, 13);
            if (rand < 10)
            {
                output = "0" + rand.ToString();
            }
            else
                output = rand.ToString();

            argActionString += output;
        }
    }


    public Transform GetLookTarg()
    {
        return this.LookitHere;
    }

    public Transform GetActionPos()
    {
        return this.LookitHere;
    }
}
