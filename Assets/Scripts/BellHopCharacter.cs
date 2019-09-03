using UnityEngine;

public class BellHopCharacter : MonoBehaviour
{

    TextMesh m_HeldObjectText;

    public GameEnums.StoryObjects _HeldObject;
    StoryItem ItemToToss;


    // Start is called before the first frame update
    void Start()
    {
        m_HeldObjectText = this.transform.GetChild(0).GetComponent<TextMesh>();
        m_HeldObjectText.text = "";
        m_HeldObjectText = this.transform.GetChild(1).GetComponent<TextMesh>();
        // UpdateHeldObject("None");
        _HeldObject = GameEnums.StoryObjects.None;
    }

    //public void UpdateHeldObject(string argObjName)
    //{

    //    GameEnums.StoryObjects tempHeldObj;
    //    if (Enum.TryParse(argObjName, true, out tempHeldObj))
    //    {
    //        if (Enum.IsDefined(typeof(GameEnums.StoryObjects), tempHeldObj) | tempHeldObj.ToString().Contains(","))
    //        {
    //            Debug.LogFormat("Converted '{0}' to {1}.", argObjName, tempHeldObj.ToString());

    //        }
    //        else
    //        {
    //            Debug.LogFormat("{0} is not an underlying value of the StoryObjects enumeration.", argObjName);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogFormat("{0} is not a member of the StoryObjects enumeration.", argObjName);
    //    }
    //    // _HeldObject = (GameEnums.StoryObjects)Enum.TryParse(typeof(GameEnums.StoryObjects), argObjName, true); //true ->  case insensitive
    //    m_HeldObjectText.text = argObjName;
    //}

    public void Set_ItemReached(GameObject ItemObj)
    {
        ItemToToss = ItemObj.GetComponent<StoryItem>();
        _HeldObject = ItemToToss.MyType;
    }

    public void TossToDwellerHand(Transform argHand)
    {
        ItemToToss.MoveTO(transform.GetChild(0), argHand, false);
    }
}
