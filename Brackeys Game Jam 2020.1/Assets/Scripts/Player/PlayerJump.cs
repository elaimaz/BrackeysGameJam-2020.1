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
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }
    }
    
    //Copied from Assets/Scripts/PlayerScript.cs as temporary fix.
    private bool IsGrounded()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.GetChild(0).position, 0.2f, groundLayer);
        if (coll != null)
        {
            return true;
        }
        else
            return false;
    }
}
