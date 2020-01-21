using UnityEngine;

public class SlopeStye : MonoBehaviour
{

    // Vector3.ProjectOnPlane - example

    // Generate a random plane in xy. Show the position of a random
    // vector and a connection to the plane. The example shows nothing
    // in the Game view but uses Update(). The script reference example
    // uses Gizmos to show the positions and axes in the Scene.


    private Vector3 vector, planeNormal;
    private Vector3 response;
    private float radians;
    private float degrees;
    public float timer = 12345.0f;

    // Generate the values for all the examples.
    // Change the example every two seconds.
    void Update()
    {
        if (timer > 2.0f)
        {
            // Generate a position inside xy space.
            vector = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);

            // Compute a normal from the plane through the origin.
            degrees = Random.Range(-45.0f, 45.0f);
            radians = degrees * Mathf.Deg2Rad;
            planeNormal = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0.0f);

            // Obtain the ProjectOnPlane result.
            response = Vector3.ProjectOnPlane(vector, planeNormal);

            // Reset the timer.
            timer = 0.0f;
        }
        timer += Time.deltaTime;
    }

    // Show a Scene view example.
    void OnDrawGizmosSelected()
    {
        // Left/right and up/down axes.
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position - new Vector3(2.25f, 0, 0), transform.position + new Vector3(2.25f, 0, 0));
        Gizmos.DrawLine(transform.position - new Vector3(0, 1.75f, 0), transform.position + new Vector3(0, 1.75f, 0));

        // Display the plane.
        Gizmos.color = Color.green;
        Vector3 angle = new Vector3(-1.75f * Mathf.Sin(radians), 1.75f * Mathf.Cos(radians), 0.0f);
        Gizmos.DrawLine(transform.position - angle, transform.position + angle);

        // Show a connection between vector and response.
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(vector, response);

        // Now show the input position.
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(vector, 0.05f);

        // And finally the resulting position.
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(response, 0.05f);
    }
    /*

    private void OnDrawGizmos()
    {

        if (true)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, transform.forward);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.up);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.right);

            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, Vector3.down);

        }


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit))
        {
            // Find the line from the gun to the point that was clicked.
            Vector3 incomingVec = hit.point - transform.position;

            // Use the point's normal to calculate the reflection vector.
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);

            // Draw lines to show the incoming "beam" and the reflection.
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.DrawRay(hit.point, reflectVec, Color.green);

        }
    }
    */
}
