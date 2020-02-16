using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOptimisedJump : MonoBehaviour
{
    [Range(1, 10)]
    public float jumpVelocity = 6f;
    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }
    }
}
