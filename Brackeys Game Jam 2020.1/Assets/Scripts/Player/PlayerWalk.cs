using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public float moveSpeed = 3f;
    
    public Rigidbody2D rb;
    
    Vector3 movement;
    Vector2 axis;
    // Update is called once per frame
    void Update()
    {
        movement.x = axis.x = Input.GetAxisRaw("Horizontal");
        
        transform.position += movement * moveSpeed * Time.deltaTime;
        
        
        //comment next line and uncomment lines in fixed update to move movement to fixedUpdate.
////        if (movement.x != 0)
////            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }
    
    void FixedUpdate()
    {
//        if (!movement.x == 0) rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);      
    }
}
