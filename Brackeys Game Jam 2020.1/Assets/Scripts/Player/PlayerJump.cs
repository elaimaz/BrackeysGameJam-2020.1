using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Range(1, 10)]
    public float jumpVelocity = 6f;
    public LayerMask groundLayer;
    
    bool prevGrounded = false;
    
    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded() && prevGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }
        else
        {
            prevGrounded = false;
        }
    }
    
    //Copied from Assets/Scripts/PlayerScript.cs as temporary fix.
    private bool IsGrounded()
    {
        prevGrounded = true;
        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, groundLayer);
        if (hitInfo.collider != null)
        {
            return true;
        }
        return false;
    }
}
