using UnityEngine;

public class InteractionCentral : MonoBehaviour
{

    Transform LookitHere;

    public string argActionString;
    void Start()
    {
        LookitHere = this.transform.GetChild(0);
    }


    public Transform GetLookTarg()
    {
        return this.LookitHere;
    }
}
