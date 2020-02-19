using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy_Follow : StateMachineBehaviour
{

    public float speed;
    public float rangedAttackRange;
    public float damage;
    private Transform PlayerPos;
    private bool isFacingRight = true;
    private Vector3 lookDir;
    private float angle;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyBase eb = animator.GetComponent<EnemyBase>();
        PlayerPos = EnemyBase.PlayerPos;
        speed = eb.speed;
        rangedAttackRange = eb.longRange;
        damage = eb.longDamage;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, PlayerPos.position, speed * Time.deltaTime);

        lookDir = PlayerPos.position - animator.transform.GetChild(0).position;

        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        animator.transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, angle + Random.Range(-15f, 15f)));

        if (PlayerPos.position.x > animator.transform.position.x && isFacingRight == false
                || PlayerPos.position.x < animator.transform.position.x && isFacingRight == true)
        {
            animator.transform.Rotate(0f, 180f, 0f);
            isFacingRight = !isFacingRight;
        }

        if (Vector2.Distance(animator.transform.position, PlayerPos.position) < rangedAttackRange)
        {
            animator.SetTrigger("isRangedAttacking");
        }

    }
}
