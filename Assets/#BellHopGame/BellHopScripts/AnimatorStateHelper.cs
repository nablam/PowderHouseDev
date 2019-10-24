//#define Debug_On
//#define oldway
using UnityEngine;

public class AnimatorStateHelper : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsTag("Done.Tag"))
        {
            ITaskable _itaskble = animator.gameObject.GetComponent<ITaskable>();
            _itaskble.TaskEnded();
        }
    }
}
