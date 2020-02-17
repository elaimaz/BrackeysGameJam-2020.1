using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public float moveSpeed = 3f;
    
    Vector3 movement;
    Vector2 axis;
    // Update is called once per frame
    void Update()
    {
        movement.x = axis.x = Input.GetAxisRaw("Horizontal");
        
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}
