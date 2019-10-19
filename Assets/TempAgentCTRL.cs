﻿using UnityEngine;

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

        if (Input.GetKeyDown(KeyCode.A))
        {

            isMain = !isMain;
            if (isMain)
            {
                Dweller.Plz_GOTO(ff.GetMainAction().transform, true);
            }
            else
            {
                Dweller.Plz_GOTO(ff.GetDanceAction().transform, true);
            }
        }
    }
}
