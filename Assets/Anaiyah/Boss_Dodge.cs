using UnityEngine;

public class Boss_Dodge : StateMachineBehaviour
{   
    public float dodgeSpeed = 5f;
    public float dodgeDistance = 0.5f;

    private float distanceMoved = 0f;
    
    private Vector3 dodgeDirection;
    private Rigidbody rb;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody>();

        
        string animName = stateInfo.IsTag("DodgeLeft")? "Left" :
            stateInfo.IsTag("DodgeRight")? "Right" :
            "Back";

        if (animName == "Left")
        {
            dodgeDirection = -animator.transform.right;   
        }
        else if (animName == "Right")
        {
            dodgeDirection = animator.transform.right;     
        }
        else
        {
            dodgeDirection = -animator.transform.forward;  
        }

        distanceMoved = 0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (distanceMoved < dodgeDistance)
        {
            float step = dodgeSpeed * Time.deltaTime;
            rb.MovePosition(rb.position +  dodgeDirection * step);
            distanceMoved += step;
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossScript boss = animator.GetComponent<BossScript>();
        if (boss != null)
            boss.OnDodge(); 
    }
}
