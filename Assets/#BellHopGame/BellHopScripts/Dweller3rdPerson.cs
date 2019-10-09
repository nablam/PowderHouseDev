using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Dweller3rdPerson : ThirdPersonCharacter
{
    [SerializeField]
    GameObject[] waypoints;
    int ptr = 0;
    Transform target;



    void Update()
    {

    }

    public new void Move(Vector3 move, bool crouch, bool jump)
    {
        base.Move(move, false, false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {


        }
    }
}
