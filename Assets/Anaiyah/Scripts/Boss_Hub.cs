using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Anaiyah
{
    public class Boss_Hub : StateMachineBehaviour
    {
        private BossScript Lucian;
        float elapsed = 0;
        
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           Lucian = FindObjectOfType<BossScript>();
           animator.SetTrigger("MoveForward");
           animator.SetBool("isMoving", true);
           elapsed = 0;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float distanceToPlayer = Vector3.Distance(Lucian.transform.position, Lucian.player.position);
            if(animator.GetBool("PlayerInRange") )
            {
                animator.SetBool("isMoving", false);
                animator.SetTrigger("Attack");
            }
            else
            {
                animator.SetBool("isMoving", true);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
