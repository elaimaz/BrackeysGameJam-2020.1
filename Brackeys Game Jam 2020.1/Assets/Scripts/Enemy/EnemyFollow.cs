using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : StateMachineBehaviour
{
    [Header("Basic Functions")]
    public bool CanDoMelee;
    public bool CanDoRanged;
    public float tFlip;

    private float speed;
    private float meleeAttackRange;
    private float rangedAttackRange;

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
        if(CanDoMelee)
        {
            meleeAttackRange = eb.meleeRange;
        }
        if(CanDoRanged)
        {
            rangedAttackRange = eb.longRange;
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, PlayerPos.position, speed * Time.deltaTime);

        if(CanDoRanged)
        {
            lookDir = PlayerPos.position - animator.transform.GetChild(0).position;
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            animator.transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, angle + Random.Range(-15f, 15f)));
        }
        

        if (PlayerPos.position.x > animator.transform.position.x && isFacingRight == false
                || PlayerPos.position.x < animator.transform.position.x && isFacingRight == true)
        {
            isFacingRight = !isFacingRight;
            animator.GetComponent<MonoBehaviour>().StartCoroutine(Flip(animator,tFlip));
        }

        if(CanDoMelee)
        {
            if (Vector2.Distance(animator.transform.position, PlayerPos.position) < meleeAttackRange)
            {
                animator.SetTrigger("isMeleeAttacking");
                return;
            }
        }
        
        if(CanDoRanged)
        {
            if (Vector2.Distance(animator.transform.position, PlayerPos.position) < rangedAttackRange)
            {
                animator.SetTrigger("isRangedAttacking");
            }
        }
        

    }

    private IEnumerator Flip(Animator animator,float t)
    {
        yield return new WaitForSecondsRealtime(t);
        animator.transform.Rotate(0f, 180f, 0f);
    }
}
