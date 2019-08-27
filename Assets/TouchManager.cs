using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

    public static bool guiTouch = false;
    public void TouchInput(Collider2D collider) {
        if (Input.touchCount > 0) { TouchPhases_0(collider); }
        if (Input.touchCount > 1) { TouchPhases_1(collider); }
    }

    void TouchPhases_0(Collider2D collider) {
        if (collider == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)) )
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    SendMessage("OnFirstTouchBegan", SendMessageOptions.DontRequireReceiver);
                    SendMessage("OnFirstTouch", SendMessageOptions.DontRequireReceiver);
                    guiTouch = true;
                    break;

                case TouchPhase.Stationary:
                    SendMessage("OnFirstTouchStayed", SendMessageOptions.DontRequireReceiver);
                    SendMessage("OnFirstTouch", SendMessageOptions.DontRequireReceiver);
                    guiTouch = true;
                    break;
                case TouchPhase.Moved:
                    SendMessage("OnFirstTouchMoved", SendMessageOptions.DontRequireReceiver);
                    SendMessage("OnFirstTouch", SendMessageOptions.DontRequireReceiver);
                    guiTouch = true;
                    break;
                case TouchPhase.Ended:
                    SendMessage("OnFirstTouchEnded", SendMessageOptions.DontRequireReceiver);
                    guiTouch = false;
                    break;
            }
        }
    }
    void TouchPhases_1(Collider2D collider)
    {
        if (collider == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)))
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    SendMessage("OnFirstTouchBegan", SendMessageOptions.DontRequireReceiver);
                    SendMessage("OnFirstTouch", SendMessageOptions.DontRequireReceiver);
                    break;

                case TouchPhase.Stationary:
                    SendMessage("OnFirstTouchStayed", SendMessageOptions.DontRequireReceiver);
                    SendMessage("OnFirstTouch", SendMessageOptions.DontRequireReceiver);
                    break;
                case TouchPhase.Moved:
                    SendMessage("OnFirstTouchMoved", SendMessageOptions.DontRequireReceiver);
                    SendMessage("OnFirstTouch", SendMessageOptions.DontRequireReceiver);
                    break;
                case TouchPhase.Ended:
                    SendMessage("OnFirstTouchEnded", SendMessageOptions.DontRequireReceiver);
                    break;
            }
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
