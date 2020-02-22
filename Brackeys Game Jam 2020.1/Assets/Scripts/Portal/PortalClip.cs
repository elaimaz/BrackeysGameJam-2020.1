using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalClip : MonoBehaviour
{
    public Vector2 center;
    public Vector2 position;
    public float maxDistance = 2;
    float actualDistance = 0;
    
    public GameObject player;
    
    private Vector3 mousePosition;
    public float moveSpeed = 0.4f;
    
    void Update(){
        //These two codes move the portal depending on mousePosition.
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        
        //These codes constrains portal to circle.
        center = player.transform.position;
        position = transform.position;
        actualDistance = Vector2.Distance(center, position);
        if (actualDistance > maxDistance)
        {
            Vector2 centerToPosition = position - center;
             centerToPosition.Normalize();
             transform.position = center + centerToPosition * maxDistance;
        }
    }
    
}
