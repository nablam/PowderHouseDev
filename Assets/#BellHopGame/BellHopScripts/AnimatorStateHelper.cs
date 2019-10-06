//#define Debug_On
using UnityEngine;

public class AnimatorStateHelper : StateMachineBehaviour
{



    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // string str = animator.GetCurrentAnimatorStateInfo(0).  GetCurrentAnimatorClipInfo(0).
        if (stateInfo.IsTag("Standard"))
        {
            if (animator.gameObject.CompareTag("Player"))
            {
#if Debug_On
                Debug.Log("bunny finished come wave");
#endif
                BellHopGameEventManager.Instance.Call_CurSequenceChanged(GameEnums.GameSequenceType.BunnyReactionEnd);
            }
            else
            {
#if Debug_On
                Debug.Log("dweller finished come wave");
#endif
            }
        }

    }
}
