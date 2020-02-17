using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(float move)
    {
        animator.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        animator.SetBool("Jumping", jumping);
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }
}
