using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalClip : MonoBehaviour
{
    public Vector2 Pos;
    public Vector2 SizeOfBox;
    
    public Vector2 center;
    public Vector2 position;
    public float maxDistance = 0.1f;
    float actualDistance = 0;
    
    public GameObject player;
    
    private Vector3 mousePosition;
    public float moveSpeed = 0.4f;
    
    public LayerMask GroundLayer;
//    public LayerMask WallLayer;
    
    private Vector2 prevPos;
    
    void Update(){
        prevPos = position;
        
        //These two codes move the portal depending on mousePosition.
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        
        //These codes constrains portal to circle.
        center = player.transform.position;
        actualDistance = Vector2.Distance(center, position);
        if (actualDistance > maxDistance)
        {
            Vector2 centerToPosition = position - center;
             centerToPosition.Normalize();
             position = center + centerToPosition * maxDistance;
        }
        
        transform.position = position;
        
        //Use ray cast. Reset to previous pos if detected.
        if(Physics2D.Raycast(center, position - center, actualDistance, GroundLayer)){
            position = transform.position = prevPos;
        }
//        Collider2D coll = Physics2D.OverlapBox((Vector2)transform.position + Pos, SizeOfBox, 0, GroundLayer);
//        if(coll != null){
//            position = transform.position = prevPos;
//        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)transform.position + Pos, SizeOfBox);
    }
}
