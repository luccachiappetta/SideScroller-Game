using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Idle : StateMachineBehaviour
{
    private Enemies m_Enemy;
    private float PlayerDirX;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Enemy = animator.GetComponent<Enemies>();
        // m_Enemy.Idle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Enemy.Idle();
        animator.SetBool("CanSeePlayer", m_Enemy.CanSeePlayer());
        
        animator.SetBool("Death", m_Enemy.isDead);
        animator.SetBool("Hit", m_Enemy.isHit);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     m_Enemy.RunToPlayer();
    // }

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
