using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float playerSpeed = 5;
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
        rb.velocity = new Vector2(horizontalInput * playerSpeed, rb.velocity.y);
    }
}
