using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }
    }

    private void FixedUpdate()
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
    }

    private void CheckGround()
    {
        if (Physics2D.OverlapCircle(transform.GetChild(0).position, 0.2f, groundLayer) != null)
            isGrounded =  true;
        else
            isGrounded =  false;
    }
}
