using UnityEngine;

public class AnimalDweller : MonoBehaviour
{
    public GameEnums.StoryObjects _HeldItem = GameEnums.StoryObjects.Ball;
    public GameEnums.AnimalCharcter My_type = GameEnums.AnimalCharcter.Alligator;
    GameObject _CurHeldObject = null;
    Transform MyHandPos;
    GameObject TextBoxName;
    GameObject TextBoxObjNeeded;
    StoryPacket _StoryPacket;

    void Awake()
    {
        TextBoxName = transform.GetChild(0).gameObject;
        TextBoxObjNeeded = transform.GetChild(1).gameObject;
        MyHandPos = transform.GetChild(2);

    }
    ////forBuilding the resorce prefab only. keep while grayboxing
    //public void InitializeMe(GameEnums.AnimalCharcter argAnimal)
    //{

    //}
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
