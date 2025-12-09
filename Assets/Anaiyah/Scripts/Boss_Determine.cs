using System.Collections;
using System.Collections.Generic;
using brolive;
using UnityEngine;

namespace Anaiyah
{
    public class Boss_Determine : StateMachineBehaviour
    {
        private Transform player;
        private Transform boss;
        Rigidbody rb;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            player = FindAnyObjectByType<PlayerLogic>().gameObject.transform;
            boss = BossScript.Instance.gameObject.transform;
            animator.GetComponent<Rigidbody>();

        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           float disToPlayer = Vector3.Distance(player.position, boss.position);
           if (disToPlayer <= BossScript.Instance.aggroDistance)
           {
               animator.SetBool("PlayerInRange", true);
           }
           else
           {
               animator.SetBool("PlayerInRange", false);
           }


        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }

}
    
