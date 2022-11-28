using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyState_RunToPlayer : StateMachineBehaviour
{
    private Enemies m_Enemy;

    private float PlayerDirX;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Enemy = animator.GetComponent<Enemies>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerDirX = m_Enemy.GetPlayerDirX();
        m_Enemy.RunToPlayer();

        if (m_Enemy.CanAttack())
        {
            animator.SetTrigger("AttackPlayer");
        }

        if (!m_Enemy.CanSeePlayer())
        {
            animator.SetTrigger("Idle");
        }
        
        animator.SetBool("Death", m_Enemy.isDead);
        animator.SetBool("Hit", m_Enemy.isHit);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     m_Enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
