using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiWaveyAnimation : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Wavey", false);
    }
}
