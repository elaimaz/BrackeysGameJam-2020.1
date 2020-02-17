using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Components
    private Rigidbody2D rb;

    //Horizontal move variables
    private float horizontalInput;
    [SerializeField]
    private float playerSpeed = 5.0f;

    //Jump Variables
    private bool resetJump = false;
    [SerializeField]
    private float jumpForce = 5.0f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ResetJumpRoutine());
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (resetJump == true && IsGrounded() == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
        rb.velocity = new Vector2(horizontalInput * playerSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        //The Raycast Distance probabily will need to change when we change the player sprite. 
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, 1 << 8);
        //Debug.DrawRay(transform.position, Vector2.down * 1.3f, Color.green); Will be needed to test with the real player character
        if (hitInfo.collider != null)
        {
            return true;
        }
        return false;
    }

    private IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }
}
