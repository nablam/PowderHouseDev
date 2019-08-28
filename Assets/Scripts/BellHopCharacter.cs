using System;
using UnityEngine;

public class BellHopCharacter : MonoBehaviour
{

    TextMesh m_HeldObjectText;

    public GameEnums.StoryObjects _HeldObject;

    // Start is called before the first frame update
    void Start()
    {
        m_HeldObjectText = this.transform.GetChild(1).GetComponent<TextMesh>();
    }

    public void UpdateHeldObject(string argObjName)
    {

        GameEnums.StoryObjects tempHeldObj;
        if (Enum.TryParse(argObjName, true, out tempHeldObj))
        {
            if (Enum.IsDefined(typeof(GameEnums.StoryObjects), tempHeldObj) | tempHeldObj.ToString().Contains(","))
            {
                Debug.LogFormat("Converted '{0}' to {1}.", argObjName, tempHeldObj.ToString());

            }
            else
            {
                Debug.LogFormat("{0} is not an underlying value of the StoryObjects enumeration.", argObjName);
            }
        }
        else
        {
            Debug.LogFormat("{0} is not a member of the StoryObjects enumeration.", argObjName);
        }
        // _HeldObject = (GameEnums.StoryObjects)Enum.TryParse(typeof(GameEnums.StoryObjects), argObjName, true); //true ->  case insensitive
        m_HeldObjectText.text = argObjName;
    }

    public GameEnums.StoryObjects GetHeldObj() { return this._HeldObject; }
    // Update is called once per frame
    void Update()
    {

    }
}
