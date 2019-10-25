using UnityEngine;
using UnityEngine.SceneManagement;

public class TempAgentCTRL : MonoBehaviour
{

    public DwellerMeshComposer Dweller;

    public InteractionCentral MainInteraction;
    public InteractionCentral SecondaryInteraction;
    // Start is called before the first frame update
    public FloorFurnisher ff;
    bool isMain;
    void Start()
    {

        Dweller.Invoke("Activate_NAvAgent", 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    isMain = !isMain;
        //    if (isMain)
        //    {
        //        Dweller.Plz_GOTO(MainInteraction.transform, true);
        //    }
        //    else
        //    {
        //        Dweller.Plz_GOTO(SecondaryInteraction.transform, true);
        //    }


        //}

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("FurnitureLivingRoom");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ff.BuildRoomType(GameEnums.RoomType.Kitchen);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ff.BuildRoomType(GameEnums.RoomType.Bedroom);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ff.BuildRoomType(GameEnums.RoomType.Livingroom);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ff.BuildRoomType(GameEnums.RoomType.Lab);
        }


    }
}
