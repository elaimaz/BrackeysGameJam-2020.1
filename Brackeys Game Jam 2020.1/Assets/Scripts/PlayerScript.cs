using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float playerSpeed = 5.0f;
    [SerializeField]
    private float jumpForce = 5.0f;
    [SerializeField]
    private bool resetJump = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            StartCoroutine(ResetJumpRoutine());
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

    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }
}
