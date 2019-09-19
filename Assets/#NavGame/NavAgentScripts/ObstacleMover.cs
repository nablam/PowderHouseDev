using System.Collections;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = .2f;
    public float strength = 9f;

    private float randomOffset;
    private Vector3 LowCation;
    private Vector3 HighCation;
    private float step;

    // Use this for initialization
    void Start()
    {
        randomOffset = Random.Range(0f, 2f);
    }
    public void InitializeObstacleMover() { }
    void CalculateLowHigh()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Sin(Time.time * speed + randomOffset) * strength;
        transform.position = pos;
    }


    public IEnumerator MovementCoroutine()
    {
        bool arrived = false;
        while (!arrived)
        {
            Vector3.MoveTowards(LowCation, HighCation, step);
            if (Vector3.Distance(LowCation, HighCation) == 0) arrived = true;
            yield return null;
        }
        if (arrived)
        {
            //do something when it arrives
        }

    }
}


