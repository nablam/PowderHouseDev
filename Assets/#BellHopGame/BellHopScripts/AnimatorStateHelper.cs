#define Debug_On
using UnityEngine;

public class AnimatorStateHelper : StateMachineBehaviour
{



    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        foreach (string actionstr in GameSettings.Instance.ActionNames)
        {
            if (stateInfo.IsTag(actionstr + ".Tag"))
            {
                if (animator.gameObject.CompareTag("Player"))
                {
#if Debug_On
                    Debug.Log("Player finished anim.tag " + actionstr);
#endif
                }
                else
                     if (animator.gameObject.CompareTag("Dweller"))
                {
#if Debug_On
                    Debug.Log("Dweller finished anim.tag " + actionstr);
#endif

                }
                //TODO< Items are tagged but have no animator. could adde coo effetcts when needed in this way"
                //add animator, tag anims win the same format as above . can animate scale fluctuations, rotations ..etc
                //else
                //     if (animator.gameObject.CompareTag("DeliveryItem"))
                //{


                //}
                else
                {
#if Debug_On
                    Debug.Log(actionstr + "finished , but object is neither Player nor Dweller");
#endif

                }

            }


        }






    }
}
