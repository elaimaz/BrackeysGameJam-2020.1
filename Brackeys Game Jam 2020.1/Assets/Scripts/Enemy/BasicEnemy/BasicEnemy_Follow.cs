using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy_Follow : StateMachineBehaviour
{

    public float speed;
    public float meleeAttackRange;
    public int damage;
    private Transform PlayerPos;
    private bool isFacingRight = true;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyBase eb = animator.GetComponent<EnemyBase>();
        PlayerPos = EnemyBase.PlayerPos;
        speed = eb.speed;
        meleeAttackRange = eb.meleeRange;
        damage = eb.meleeDamage;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, PlayerPos.position, speed * Time.deltaTime);
        if(PlayerPos.position.x > animator.transform.position.x && isFacingRight == false
                || PlayerPos.position.x < animator.transform.position.x && isFacingRight == true)
        {
            animator.transform.Rotate(0f, 180f, 0f);
            isFacingRight = !isFacingRight;
        }

        if (Vector2.Distance(animator.transform.position,PlayerPos.position) < meleeAttackRange)
        {
            //Debug.Log("Attacking");
            animator.SetTrigger("isMeleeAttacking");
            //TODO: Call DamageTaken(damage)
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
