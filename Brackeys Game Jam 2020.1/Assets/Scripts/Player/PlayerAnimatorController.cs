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

    public void Move(bool t)
    {
        animator.SetBool("isMoving", t);
    }
    public void Flip(int m)
    {
        animator.SetFloat("Multipler", m);
    }
    public void Jump(bool jumping)
    {
        animator.SetBool("Jumping", jumping);
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }

    public void JumpPortal()
    {
        animator.SetTrigger("JumpPortal");
    }
}
