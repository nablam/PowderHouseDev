using UnityEngine;

public class NumPadCTRL : MonoBehaviour
{
    public void ReadButtonTouch(int argnum)
    {
        Debug.Log("pressed " + argnum);
    }
    public void UnTouch(int argnum)
    {
        Debug.Log("UNpressed " + argnum);

    }
}
