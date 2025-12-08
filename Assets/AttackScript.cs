using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Anaiyah
{
    public class AttackScript : StateMachineBehaviour
    {
        private BossScript Lucian;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Lucian = BossScript.Instance;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector3 directionToPlayer = (Lucian.transform.position - animator.transform.position);
            directionToPlayer.y = 0;

            Lucian.transform.rotation = Quaternion.Slerp(Lucian.transform.rotation,
                Quaternion.LookRotation(directionToPlayer.normalized), Lucian.turnSpeed * 3 * Time.deltaTime);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

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