using UnityEngine;

public class AnimalDweller : MonoBehaviour
{
    public GameEnums.StoryObjects _HeldItem = GameEnums.StoryObjects.None;
    GameObject _CurHeldObject = null;
    Transform MyHandPos;
    StoryPacket _StoryPacket;

    void Awake()
    {
        MyHandPos = transform.GetChild(2);
    }
    public void InitMyPacket(StoryPacket argPacket)
    {
        _StoryPacket = argPacket;
    }

    public void UpdateHeldObject(GameObject argObj, GameEnums.StoryObjects argObjEnum)
    {
        _HeldItem = argObjEnum;
        if (_CurHeldObject == null)
        {
            _CurHeldObject = argObj;
            _CurHeldObject.transform.position = MyHandPos.position;
            _CurHeldObject.transform.parent = MyHandPos;
        }

    }

}
