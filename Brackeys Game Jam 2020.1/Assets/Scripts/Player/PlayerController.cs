using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;
    
    [Range(1, 10)]
    public float moveSpeed = 3f;
    [Range(1, 10)]
    public float jumpVelocity = 6f;
    [Range(1, 10)]
    public float fallMultiplier = 3f;
    [Range(1, 10)]
    public float lowJumpMultiplier = 2f;

    public LayerMask groundLayer;

    Rigidbody2D rb;
    Vector3 movement;
    [SerializeField]
    bool isGrounded = false;
    
    private PlayerAnimatorController playerAnimator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<PlayerAnimatorController>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        Flip(movement.x);
        if (canMove == true)
        {
            if (Input.GetButtonDown("Jump") && isGrounded == true)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
                playerAnimator.Jump(true);
            }
        }
        
        //Just for test, when we have a proper death mechanic change it. Right now it is just to show player death.
        if (Input.GetKeyDown(KeyCode.H))
        {
            playerAnimator.Death();
        }

        //Just for test, when we have a proper portal mechanic change it. Right now it is just to show player jump portal animation.
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerAnimator.JumpPortal();
        }
    }

    private void FixedUpdate()
    {
        if (canMove == true)
        {
            transform.position += movement * moveSpeed * Time.fixedDeltaTime;

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

            CheckGround();

            playerAnimator.Move(movement.x);
        }
    }

    private void CheckGround()
    {
        if ((Physics2D.OverlapCircle(transform.GetChild(0).position, 0.2f, groundLayer) != null ))
        {
            if(isGrounded == false)
            {
                playerAnimator.Jump(false);
                FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Land");
            }
            isGrounded =  true;
        }
        else if(isGrounded == true)
        {
            isGrounded =  false;
            
        }
    }

    private void Flip(float movement)
    {
        if (movement > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (movement < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
