using UnityEngine;

public class CrossPRoTest : MonoBehaviour
{

    public Transform A;
    public Transform B;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Dot(B.position, A.position));
    }
}
