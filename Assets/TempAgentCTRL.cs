using UnityEngine;

public class TempAgentCTRL : MonoBehaviour
{

    public DwellerMeshComposer Dweller;

    public InteractionCentral MainInteraction;
    public InteractionCentral SecondaryInteraction;
    // Start is called before the first frame update

    bool isMain;
    void Start()
    {


        Dweller.Invoke("Start_Agent", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isMain = !isMain;
            if (isMain)
            {
                Dweller.MoveAgentTo(MainInteraction.transform, true);
            }
            else
            {
                Dweller.MoveAgentTo(SecondaryInteraction.transform, true);
            }


        }
    }
}
