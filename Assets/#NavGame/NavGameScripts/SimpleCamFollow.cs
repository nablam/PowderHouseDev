using UnityEngine;

public class SimpleCamFollow : MonoBehaviour
{
    public Transform player;
    float curZ;
    // Start is called before the first frame update
    void Start()
    {
        curZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z + curZ);
    }
}
