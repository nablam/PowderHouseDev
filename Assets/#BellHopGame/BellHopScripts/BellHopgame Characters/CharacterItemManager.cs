using UnityEngine;
using static GameEnums;

public class CharacterItemManager : MonoBehaviour
{
    [Tooltip("must be assigned manually")]
    public Transform RightHandHoldPos;
    [Tooltip("must be assigned manually")]
    public Transform LeftHandHoldPos;
    [SerializeField]
    DeliveryItem _deliveryItem_R = null;
    [SerializeField]
    DeliveryItem _deliveryItem_L = null;


    public void AttachItem(DeliveryItem argDeliveryItem, AnimalCharacterHands argLeftright)
    {
        if (!HasItem(argLeftright))
        {
            if (argLeftright == AnimalCharacterHands.Right)
            {
                _deliveryItem_R = argDeliveryItem;
                _deliveryItem_R.transform.position = new Vector3(RightHandHoldPos.position.x, RightHandHoldPos.position.y, RightHandHoldPos.position.z);
                _deliveryItem_R.transform.parent = RightHandHoldPos.transform;
            }
            else
            {
                _deliveryItem_L = argDeliveryItem;
                _deliveryItem_L.transform.position = new Vector3(LeftHandHoldPos.position.x, LeftHandHoldPos.position.y, LeftHandHoldPos.position.z);
                _deliveryItem_L.transform.parent = LeftHandHoldPos.transform;
            }

        }
        else
            Debug.Log("already child on " + argLeftright.ToString());

    }

    public void DetachItem(AnimalCharacterHands argLeftright)
    {
        if (HasItem(argLeftright))
        {
            if (argLeftright == AnimalCharacterHands.Right)
            {
                _deliveryItem_R.transform.parent = null;
                _deliveryItem_R = null;
            }
            else
            {

                _deliveryItem_L.transform.parent = null;
                _deliveryItem_L = null;
            }
        }
        else
            Debug.Log("Nochild to detach on " + argLeftright.ToString());

    }

    public void Show_LR(bool argShowHide, AnimalCharacterHands argLeftright)
    {
        if (HasItem(argLeftright))
        {

            if (argLeftright == AnimalCharacterHands.Right)
            {
                _deliveryItem_R.ShowMe(argShowHide);
            }
            else
            {
                _deliveryItem_L.ShowMe(argShowHide);
            }
        }
        else
            Debug.Log("Nochild to show oor hid " + argLeftright.ToString());

    }

    public bool HasItem(AnimalCharacterHands argLeftright)
    {
        if (argLeftright == AnimalCharacterHands.Right)
        {
            if (RightHandHoldPos.childCount > 0)
            {
                return true;
            }
            else
                return false;
        }
        else
        {
            if (LeftHandHoldPos.childCount > 0)
            {
                return true;
            }
            else
                return false;


        }

    }

    /// <summary>
    /// Please use HasItem(LR) externally first before asking for delivery item , to avoid a null
    /// </summary>
    /// <param name="argLeftright"></param>
    /// <returns></returns>
    public DeliveryItem GetItem_LR(AnimalCharacterHands argLeftright)
    {

        if (argLeftright == AnimalCharacterHands.Right)
        {
            return _deliveryItem_R;

        }
        else
        {

            return _deliveryItem_L;
        }



    }
}
