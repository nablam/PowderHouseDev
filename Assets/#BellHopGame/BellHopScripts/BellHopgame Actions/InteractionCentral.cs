using UnityEngine;

public class InteractionCentral : MonoBehaviour
{

    Transform LookitHere;

    public string argActionString;
    void Start()
    {
        LookitHere = this.transform.GetChild(0);
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
}
