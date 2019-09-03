using System.Collections.Generic;
using UnityEngine;

public class VisColsMover : MonoBehaviour
{

    float speed = 0.1f;//0.06f;


    void DoScroll_BAckGround_Right_toLoeft(List<GameObject> argList, float speed)
    {
        foreach (GameObject go in argList)
        {
            if (go == null) continue;
            go.transform.Translate(Vector2.left * speed);
        }
    }
    void DoScroll_BAckGrounds_Left_toRight(List<GameObject> argList, float speed)
    {
        foreach (GameObject go in argList)
        {
            if (go == null) continue;
            go.transform.Translate(Vector2.right * speed);
        }
    }
    void DoScroll_Right_toLoeft()
    {

    }
    void DoScroll_Left_toRight()
    {

    }

    void DoNothing()
    {

    }

    void ALL_LEFTWARD()
    {
        DoScroll_Left_toRight();
    }

    void ALL_RIGHTYTHEN()
    {
        DoScroll_Right_toLoeft();
    }


    public void MoveGameLEFT() { ALL_LEFTWARD(); }
    public void MoveGameRIGHT() { ALL_RIGHTYTHEN(); }
    public void MoveGameSTOP()
    {
        DoNothing();
    }

    public void MoveJumpDino() { }

    void Update()
    {
        //if (Input.GetKey(KeyCode.LeftArrow)) { MoveGameLEFT(); }
        //else
        //   if (Input.GetKey(KeyCode.RightArrow)) { MoveGameRIGHT(); }
        //else
        //{
        //    MoveGameSTOP();
        //}
    }
}
