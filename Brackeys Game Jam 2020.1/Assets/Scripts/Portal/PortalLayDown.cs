using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLayDown : MonoBehaviour
{
    public Vector2 Pos;
    public Vector2 SizeOfBox;
    
    public Vector2 center;
    public Vector2 position;
    public float maxDistance = 0.1f;
    float actualDistance = 0;
    
    public GameObject player;
    public GameObject sprite;
    
    public float moveSpeed = 0.4f;
    public LayerMask GroundLayer;
    
    private Vector2 mousePosition;
    
    private Vector2 prevPos;
    
    public bool raycastDetectHorizontal;
    public bool raycastDownRayHit;
    void Update(){
        
        //Get mouse position.
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //Calculate center of player.
        center = player.transform.position;
//        actualDistance = Vector2.Distance(center, position);
        
//        if (actualDistance > maxDistance)
//        {
//            Vector2 centerToPosition = position - center;
//             centerToPosition.Normalize();
//             position = center + centerToPosition * maxDistance;
//        }
        
        
        //Get x component direction.
        Vector2 horizontalDirection = mousePosition - center;
        horizontalDirection.y = 0;
        
        Vector2 Rightmost = center + horizontalDirection;
        
        float horizontalDistance = Vector2.Distance(center, Rightmost);
        
        //If wall detected, deactivate portal.
        if(Physics2D.Raycast(center, horizontalDirection, horizontalDistance, GroundLayer)){
//            sprite.SetActive(false);
            raycastDetectHorizontal = true;
//            return;
//            position = transform.position = prevPos;
        }else raycastDetectHorizontal = false;
        
        float meanDistance;
        
        //Now form triangle and check if the hypotenuse is greater than range.
        RaycastHit hit;
        Ray downRay = new Ray(Rightmost, -Vector3.up);
        if (Physics.Raycast(downRay, out hit))
        {
            raycastDownRayHit = false;
            meanDistance = Mathf.Sqrt(horizontalDistance*horizontalDistance+(hit.distance)*(hit.distance));
            if (meanDistance<maxDistance){
                //now portal can be placed on ground.
                Vector2 verticalDirection;
                verticalDirection.x = 0;
                verticalDirection.y = hit.distance;
                
                
                gameObject.transform.position = center + horizontalDirection + verticalDirection;
                gameObject.transform.position = player.transform.position - gameObject.transform.position;
                
                sprite.SetActive(false);
            }
        }
        
        
//        transform.position = position;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)transform.position + Pos, SizeOfBox);
    }
}
